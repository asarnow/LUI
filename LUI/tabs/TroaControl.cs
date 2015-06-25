using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using lasercom;
using lasercom.camera;
using lasercom.control;
using lasercom.ddg;
using lasercom.io;
using LUI.config;
using LUI.controls;

namespace LUI.tabs
{
    public partial class TroaControl : LuiTab
    {

        public class TimesRow : INotifyPropertyChanged
        {
            private double _Value;
            public double Value
            {
                get
                {
                    return _Value;
                }
                set
                {
                    _Value = value;
                    NotifyPropertyChanged();
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;

            private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
            {
                var handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            public static explicit operator TimesRow(DataRow dr)
            {
                TimesRow p = new TimesRow();
                p.Value = (double)dr.ItemArray[0];
                return p;
            }
            public static explicit operator TimesRow(DataGridViewRow row)
            {
                return new TimesRow()
                {
                    Value = (double)row.Cells["Value"].Value,
                };
            }
        }

        struct WorkArgs
        {
            public WorkArgs(int N, IList<double> Times, string PrimaryDelayName, string PrimaryDelayTrigger, PumpMode Pump, bool DiscardFirst)
            {
                this.N = N;
                this.Times = new List<double>(Times);
                this.PrimaryDelayName = PrimaryDelayName;
                this.TriggerName = PrimaryDelayTrigger;
                //this.GateName = new Tuple<char, char>(Gate.Delay[0], Gate.Delay[1]);
                //this.GateTriggerName = Gate.Trigger[0];
                //this.Gate = Gate.DelayValue;
                //this.GateDelay = Gate.DelayValue;
                this.GateName = null;
                this.GateTriggerName = '\0';
                this.Gate = double.NaN;
                this.GateDelay = double.NaN;
                this.Pump = Pump;
                this.DiscardFirst = DiscardFirst;
            }
            public readonly int N;
            public readonly IList<double> Times;
            public readonly string PrimaryDelayName;
            public readonly string TriggerName;
            public readonly Tuple<char,char> GateName;
            public readonly char GateTriggerName;
            public readonly double GateDelay;
            public readonly double Gate;
            public readonly PumpMode Pump;
            public readonly bool DiscardFirst;
        }

        struct ProgressObject
        {
            public ProgressObject(double[] Data, string CameraStatus, double Delay, Dialog Status)
            {
                this.Data = Data;
                this.CameraStatus = CameraStatus;
                this.Delay = Delay;
                this.Status = Status;
            }
            public readonly double[] Data;
            public readonly string CameraStatus;
            public readonly double Delay;
            public readonly Dialog Status;
        }

        IList<double> Times
        {
            get
            {
                return TimesList.Select(x => x.Value).ToList();
            }
            set
            {
                TimesList.Clear();
                foreach (double d in value)
                    TimesList.Add(new TimesRow() { Value = d });
            }
        }

        public enum Dialog
        {
            INITIALIZE, PROGRESS, PROGRESS_DARK, PROGRESS_TIME, 
            PROGRESS_TIME_COMPLETE, PROGRESS_FLASH, PROGRESS_TRANS,
            CALCULATE
        }

        public enum PumpMode
        {
            NEVER, TRANS, ALWAYS
        }

        private BindingList<TimesRow> TimesList = new BindingList<TimesRow>();
        MatFile DataFile;
        MatVar<int> RawData;
        MatVar<double> LuiData;

        public TroaControl(LuiConfig Config) : base(Config)
        {
            InitializeComponent();
            Init();

            TimesList.AllowEdit = true;
            TimesView.DefaultValuesNeeded += (sender, e) => { e.Row.Cells["Value"].Value = 0; };
            TimesView.DataSource = new BindingSource(TimesList, null);
            TimesView.CellValidating += TimesView_CellValidating;
            TimesView.CellEndEdit += TimesView_CellEndEdit;
            
            SaveData.Click += (sender, e) => SaveOutput();

            DdgConfigBox.Config = Config;
            DdgConfigBox.Commander = Commander;
            DdgConfigBox.AllowZero = false;
            DdgConfigBox.HandleParametersChanged(this, EventArgs.Empty);
        }

        private void Init()
        {
            SuspendLayout();
            
            SaveData.Enabled = false;

            ResumeLayout();
        }

        protected override void OnLoad(EventArgs e)
        {
            PumpBox.ObjectChanged += HandlePumpChanged;
            base.OnLoad(e);
        }

        public override void HandleParametersChanged(object sender, EventArgs e)
        {
            base.HandleParametersChanged(sender, e); // Takes care of ObjectSelectPanel.
            DdgConfigBox.HandleParametersChanged(sender, e);

            var PumpsAvailable = Config.GetParameters(typeof(PumpParameters));
            if (PumpsAvailable.Count() > 0)
            {
                var selectedPump = PumpBox.SelectedObject;
                PumpBox.Objects.Items.Clear();
                foreach (var p in PumpsAvailable)
                    PumpBox.Objects.Items.Add(p);
                // One of next two lines will trigger CameraChanged event.
                PumpBox.SelectedObject = selectedPump;
                if (PumpBox.Objects.SelectedItem == null) PumpBox.Objects.SelectedIndex = 0;
                PumpBox.Enabled = true;
            }
            else
            {
                PumpBox.Enabled = false;
            }
            
        }

        public override void HandleContainingTabSelected(object sender, EventArgs e)
        {
            base.HandleContainingTabSelected(sender, e);
            DdgConfigBox.UpdatePrimaryDelayValue();
        }

        public virtual void HandlePumpChanged(object sender, EventArgs e)
        {
            if (Commander.Pump != null) Commander.Pump.SetClosed();
            Commander.Pump = (IPump)Config.GetObject(PumpBox.SelectedObject);
        }

        protected override void LoadSettings()
        {
            base.LoadSettings();
            var Settings = Config.TabSettings[this.GetType().Name];
            string value;
            if (Settings.TryGetValue("PrimaryDelayDdg", out value) && value != null && value != "")
                DdgConfigBox.PrimaryDelayDdg = (DelayGeneratorParameters)Config.GetFirstParameters(
                    typeof(DelayGeneratorParameters), value);
            if (Settings.TryGetValue("PrimaryDelayDelay", out value) && value != null && value != "")
                DdgConfigBox.PrimaryDelayDelay = value;
        }

        protected override void SaveSettings()
        {
            base.SaveSettings();
            var Settings = Config.TabSettings[this.GetType().Name];
            Settings["PrimaryDelayDdg"] = DdgConfigBox.PrimaryDelayDdg != null ? DdgConfigBox.PrimaryDelayDdg.Name : null;
            Settings["PrimaryDelayDelay"] = DdgConfigBox.PrimaryDelayDelay != null ? DdgConfigBox.PrimaryDelayDelay : null;
        }

        /// <summary>
        /// Create temporary MAT file and initialize variables.
        /// </summary>
        /// <param name="NumChannels"></param>
        /// <param name="NumScans"></param>
        /// <param name="NumTimes"></param>
        private void InitDataFile(int NumChannels, int NumScans, int NumTimes)
        {
            string TempFileName = Path.GetTempFileName();
            TempFileName = TempFileName.Replace(".tmp", ".mat");
            DataFile = new MatFile(TempFileName);
            RawData = DataFile.CreateVariable<int>("rawdata", NumScans, NumChannels);
            LuiData = DataFile.CreateVariable<double>("luidata", NumTimes + 1, NumChannels + 1);
        }

        protected override void Graph_Click(object sender, MouseEventArgs e)
        {
            var NormalizedCoords = Graph.AxesToNormalized(Graph.ScreenToAxes(new Point(e.X, e.Y)));
            int SelectedChannel = Commander.Camera.CalibrationAscending ?
                (int)Math.Round(NormalizedCoords.X * (Commander.Camera.Width - 1))
                :
                (int)Math.Round((1 - NormalizedCoords.X) * (Commander.Camera.Width - 1));
            var X = Commander.Camera.Calibration[SelectedChannel];
            float Y = NormalizedCoords.Y;
            Graph.ClearAnnotation();
            Graph.Annotate(GraphControl.Annotation.VERTLINE, Graph.MarkerColor, X);
            Graph.Annotate(GraphControl.Annotation.HORZLINE, Graph.MarkerColor, Y);
            Graph.Invalidate();
        }

        protected override void Collect_Click(object sender, EventArgs e)
        {
            if (DdgConfigBox.PrimaryDelayDdg == null || DdgConfigBox.PrimaryDelayDelay == null)
            {
                MessageBox.Show("Primary delay must be configured.", "Error", MessageBoxButtons.OK);
                return;
            }

            CameraStatus.Text = "";

            Graph.ClearData();
            Graph.Invalidate();

            int N = (int)NScan.Value;
            PumpMode Mode;
            if (PumpNever.Checked) Mode = PumpMode.NEVER;
            else if (PumpTs.Checked) Mode = PumpMode.TRANS;
            else Mode = PumpMode.ALWAYS;

            Commander.BeamFlag.CloseLaserAndFlash();

            worker = new BackgroundWorker();
            worker.DoWork += new System.ComponentModel.DoWorkEventHandler(DoWork);
            worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(WorkProgress);
            worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(WorkComplete);
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync(new WorkArgs(N, Times, DdgConfigBox.PrimaryDelayDelay, DdgConfigBox.PrimaryDelayTrigger, Mode, Discard.Checked));
            OnTaskStarted(EventArgs.Empty);
        }

        public override void OnTaskStarted(EventArgs e)
        {
            base.OnTaskStarted(e);
            DdgConfigBox.Enabled = false;
        }

        public override void OnTaskFinished(EventArgs e)
        {
            base.OnTaskFinished(e);
            DdgConfigBox.Enabled = true;
            SaveData.Enabled = true;
        }

        protected override void DoWork(object sender, DoWorkEventArgs e)
        {
            var progress = new ProgressObject(null, null, 0, Dialog.INITIALIZE);
            if (PauseCancelProgress(e, 0, progress)) return; // Show zero progress.

            // Set camera for external gate and full vertical binning.
            if (Commander.Camera is AndorCamera)
            {
                Commander.Camera.AcquisitionMode = AndorCamera.AcquisitionModeSingle;
                Commander.Camera.TriggerMode = AndorCamera.TriggerModeExternalExposure;
                Commander.Camera.DDGTriggerMode = AndorCamera.DDGTriggerModeExternal;
                Commander.Camera.ReadMode = AndorCamera.ReadModeFVB;
            }

            var args = (WorkArgs)e.Argument;
            int N = args.N; // Save typing for later.
            int half = N / 2; // Integer division rounds down.
            IList<double> Times = args.Times;

            // Total scans = dark scans + ground state scans + plus time series scans.
            int TotalScans = 2*N + Times.Count * N;

            // Create the data store.
            InitDataFile((int)Commander.Camera.AcqSize, TotalScans, Times.Count);

            // Measure dark current.
            progress = new ProgressObject(null, null, 0, Dialog.PROGRESS_DARK);
            if (PauseCancelProgress(e, 0, progress)) return;
            
            // Dark buffers.
            int[] DarkBuffer = new int[Commander.Camera.AcqSize];
            int[] Dark = new int[Commander.Camera.AcqSize];

            // Dark scans.
            Commander.BeamFlag.CloseLaserAndFlash();
            for (int i = 0; i < N; i++)
            {
                uint ret = Commander.Camera.Acquire(DarkBuffer);
                RawData.WriteNext(DarkBuffer, 0);
                Data.Accumulate(Dark, DarkBuffer);

                progress = new ProgressObject(null, Commander.Camera.DecodeStatus(ret), 0, Dialog.PROGRESS_DARK);
                if (PauseCancelProgress(e, (i + 1) * 99 / TotalScans, progress)) return;
            }
            Data.DivideArray(Dark, N); // Average dark current.

            // Set delays for GS.
            Commander.DDG.SetDelay(args.PrimaryDelayName, args.TriggerName, 3.2E-8); // Set delay time.

            // Buffer for acuisition data.
            int[] DataBuffer = new int[Commander.Camera.AcqSize];
            double[] Ground = new double[Commander.Camera.AcqSize];

            // Flow-flash.
            if (args.Pump == PumpMode.ALWAYS)
            {
                Commander.Pump.SetOpen();
                if (args.DiscardFirst)
                {
                    var ret = Commander.Camera.Acquire(DataBuffer);
                }
            }

            // Ground state scans - first half.
            Commander.BeamFlag.OpenFlash();
            for (int i = 0; i < half; i++)
            {
                uint ret = Commander.Camera.Acquire(DataBuffer);
                RawData.WriteNext(DataBuffer, 0);
                Data.Accumulate(Ground, DataBuffer);

                progress = new ProgressObject(null, Commander.Camera.DecodeStatus(ret), 0, Dialog.PROGRESS_FLASH);
                if (PauseCancelProgress(e, (N + (i + 1)) * 99 / TotalScans, progress)) return; // Handle new data.
            }

            Commander.BeamFlag.CloseLaserAndFlash();

            Data.DivideArray(Ground, half); // Average GS for first half.
            Data.Dissipate(Ground, Dark); // Subtract average dark from average GS.


            // Excited state buffer.
            double[] Excited = new double[Commander.Camera.AcqSize];

            // Flow-flash.
            if (args.Pump == PumpMode.TRANS)
            {
                Commander.Pump.SetOpen();
                if (args.DiscardFirst)
                {
                    var ret = Commander.Camera.Acquire(DataBuffer);
                }
            }

            // Excited state scans.
            Commander.BeamFlag.OpenLaserAndFlash();
            for (int i = 0; i < Times.Count; i++)
            {
                double Delay = Times[i];
                progress = new ProgressObject(null, null, Delay, Dialog.PROGRESS_TIME);
                if (PauseCancelProgress(e, (half + i * N) * 99 / TotalScans, progress)) return; // Display current delay.
                
                for (int j = 0; j < N; j++)
                {

                    Commander.DDG.SetDelay(args.PrimaryDelayName, args.TriggerName, Delay); // Set delay time.
                    
                    uint ret = Commander.Camera.Acquire(DataBuffer);
                    RawData.WriteNext(DataBuffer, 0);
                    Data.Accumulate(Excited, DataBuffer);

                    progress = new ProgressObject(null, Commander.Camera.DecodeStatus(ret), Delay, Dialog.PROGRESS_TRANS);
                    if (PauseCancelProgress(e, (N + half + (i + 1) * (j + 1)) * 99 / TotalScans, progress)) return; // Handle new data.
                }
                
                Data.DivideArray(Excited, N); // Average ES for time point.
                Data.Dissipate(Excited, Dark); // Subtract average dark from time point average.
                double[] Difference = Data.DeltaOD(Ground, Excited); // Time point diff. spec. w/ current GS average.
                progress = new ProgressObject(Difference, null, Delay, Dialog.PROGRESS_TIME_COMPLETE);
                if (PauseCancelProgress(e, (N + half + N * Times.Count) * 99 / TotalScans, progress)) return;
            }
            
            Commander.BeamFlag.CloseLaserAndFlash();

            // Set delays for GS.
            Commander.DDG.SetDelay(args.PrimaryDelayName, args.TriggerName, 3.2E-8); // Set delay time.

            // Flow-flash.
            if (args.Pump == PumpMode.TRANS) // Could close pump before last collect.
            {
                Commander.Pump.SetClosed();
            }

            // Ground state scans - second half.
            Commander.BeamFlag.OpenFlash();
            int half2 = N % 2 == 0 ? half : half + 1; // If N is odd, need 1 more GS scan in the second half.
            for (int i = 0; i < half2; i++)
            {
                uint ret = Commander.Camera.Acquire(DataBuffer);
                RawData.WriteNext(DataBuffer, 0);

                progress = new ProgressObject(null, Commander.Camera.DecodeStatus(ret), 0, Dialog.PROGRESS_FLASH);
                if (PauseCancelProgress(e, (N + half + (N * Times.Count) + (i + 1)) * 99 / TotalScans, progress)) return;
            }
            Commander.BeamFlag.CloseLaserAndFlash();

            // Flow-flash.
            if (args.Pump == PumpMode.ALWAYS)
            {
                Commander.Pump.SetClosed(); // Could close pump before last collect.
            }

            // Calculate LuiData matrix
            progress = new ProgressObject(null, null, 0, Dialog.CALCULATE);
            if (PauseCancelProgress(e, 99, progress)) return;
            // Write dummy value (number of scans).
            LuiData.Write((double)args.N, new long[] { 0, 0 });
            // Write wavelengths.
            long[] RowSize = { 1, Commander.Camera.AcqSize };
            LuiData.Write(Commander.Camera.Calibration, new long[] { 0, 1 }, RowSize);
            // Write times.
            long[] ColSize = { Times.Count, 1 };
            LuiData.Write(Times.ToArray(), new long[] { 1, 0 }, ColSize);
            
            // Read ground state values and average.
            Ground.Initialize(); // Zero ground state buffer.
            // Read 1st half
            for (int i = 0; i < half; i++)
            {
                RawData.Read(DataBuffer, new long[] { i, 0 }, RowSize);
                Data.Accumulate(Ground, DataBuffer);
            }
            // Read 2nd half
            for (int i = (N + half + N * Times.Count); i < TotalScans; i++)
            {   
                RawData.Read(DataBuffer, new long[] { i, 0 }, RowSize);
                Data.Accumulate(Ground, DataBuffer);
            }
            Data.DivideArray(Ground, N); // Average ground state.
            Data.Dissipate(Ground, Dark); // Subtract average dark.

            // Read excited state values, average and compute delta OD.
            Excited.Initialize(); // Zero excited state buffer.
            for (int i = 0; i < Times.Count; i++ )
            {
                for (int j = 0; j < N; j++)
                {
                    // Read time point j
                    int idx = N + half + (i * N) + j;
                    RawData.Read(DataBuffer, new long[] { idx, 0 }, RowSize);
                    Data.Accumulate(Excited, DataBuffer);
                }
                Data.DivideArray(Excited, N); // Average excited state for time point.
                Data.Dissipate(Excited, Dark); // Subtract average dark.
                // Write the final difference spectrum for time point.
                LuiData.Write(Data.DeltaOD(Ground, Excited),
                    new long[] { i + 1, 1 }, RowSize);
            }

            // Done with everything.
        }

        protected override void WorkProgress(object sender, ProgressChangedEventArgs e)
        {
            var progress = (ProgressObject)e.UserState;
            StatusProgress.Value = e.ProgressPercentage;
            switch (progress.Status)
            {
                case Dialog.INITIALIZE:
                    ProgressLabel.Text = "Initializing";
                    break;
                case Dialog.PROGRESS:
                    break;
                case Dialog.PROGRESS_DARK:
                    ProgressLabel.Text = "Collecting dark";
                    CameraStatus.Text = progress.CameraStatus;
                    break;
                case Dialog.PROGRESS_TIME:
                    DdgConfigBox.PrimaryDelayValue = progress.Delay;
                    break;
                case Dialog.PROGRESS_TIME_COMPLETE:
                    Display(progress.Data);
                    break;
                case Dialog.PROGRESS_FLASH:
                    ProgressLabel.Text = "Collecting ground";
                    CameraStatus.Text = progress.CameraStatus;
                    break;
                case Dialog.PROGRESS_TRANS:
                    ProgressLabel.Text = "Collecting transient";
                    CameraStatus.Text = progress.CameraStatus;
                    break;
                case Dialog.CALCULATE:
                    ProgressLabel.Text = "Calculating...";
                    break;
            }
        }

        protected override void WorkComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            Commander.BeamFlag.CloseLaserAndFlash();
            Commander.Pump.SetClosed();
            if (e.Error != null)
            {
                // Handle the exception thrown in the worker thread.
                MessageBox.Show(e.Error.ToString());
            }
            else if (e.Cancelled)
            {
                ProgressLabel.Text = "Aborted";
            }
            else
            {
                SaveOutput();
                ProgressLabel.Text = "Complete";
            }

            // Ensure the temp file is always closed.
            if (DataFile != null) DataFile.Close();

            OnTaskFinished(EventArgs.Empty);
        }

        private void Display(double[] Y)
        {
            Graph.DrawPoints(Commander.Camera.Calibration, Y);
            Graph.Invalidate();
            Graph.MarkerColor = Graph.NextColor;
        }

        /// <summary>
        /// Clears row error if user presses ESC while editing.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TimesView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            TimesView.Rows[e.RowIndex].ErrorText = String.Empty;
        }

        /// <summary>
        /// Validate that times entered by the user are legal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void TimesView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (TimesView.Columns[e.ColumnIndex].Name == "Value")
            {
                double value;
                if (!double.TryParse(e.FormattedValue.ToString(), out value))
                {
                    TimesView.Rows[e.RowIndex].ErrorText = "Time must be a number";
                    e.Cancel = true;
                }
                if (value <= 0)
                {
                    TimesView.Rows[e.RowIndex].ErrorText = "Time must be positive";
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Load file containing time delays (one per line).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoadTimes_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Text File|*.txt|All Files|*.*";
            openFile.Title = "Load Time Series File";
            openFile.ShowDialog();

            if (openFile.FileName == "") return;

            try
            {
                Times = FileIO.ReadTimesFile(openFile.FileName);
            }
            catch (IOException ex)
            {
                Log.Error(ex);
                MessageBox.Show("Couldn't load times file at " + openFile.FileName);
            }
            catch (FormatException ex)
            {
                Log.Error(ex);
                MessageBox.Show("Couldn't parse file: " + ex.Message);
            }
                
        }

        private void SaveOutput()
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "MAT File|*.mat|CSV File|*.csv";
            saveFile.Title = "Save Data File";
            saveFile.ShowDialog();

            if (saveFile.FileName == "") return;

            switch (saveFile.FilterIndex)
            {
                case 1: // MAT file; just move temporary MAT file.
                    if (DataFile != null && !DataFile.Closed) DataFile.Close();
                    try
                    {
                        File.Copy(DataFile.FileName, saveFile.FileName);
                    }
                    catch (IOException ex)
                    {
                        Log.Error(ex);
                        MessageBox.Show(ex.Message);
                    }
                    break;
                case 2: // CSV file; read LuiData to CSV file.
                    if (DataFile != null)
                    {
                        if (DataFile.Closed) DataFile.Reopen();
                        //MatVar<double> luiData = (MatVar<double>)DataFile["LuiData"];

                        if (!LuiData.Closed)
                        {
                            double[,] Matrix = new double[LuiData.Dims[0], LuiData.Dims[1]];
                            LuiData.Read(Matrix, new long[] { 0, 0 }, LuiData.Dims);
                            FileIO.WriteMatrix(saveFile.FileName, Matrix);
                        }

                        DataFile.Close();
                    }
                    break;
            }
        }

    }
}

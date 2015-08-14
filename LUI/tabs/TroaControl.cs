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
            public WorkArgs(int N, IList<double> Times, string PrimaryDelayName, string PrimaryDelayTrigger, PumpMode Pump, int Discard, double GsDelay)
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
                this.Discard = Discard;
                this.GsDelay = GsDelay;
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
            public readonly int Discard;
            public readonly double GsDelay;
        }

        struct ProgressObject
        {
            public ProgressObject(double[] Data, double Delay, Dialog Status)
            {
                this.Data = Data;
                this.Delay = Delay;
                this.Status = Status;
            }
            public readonly double[] Data;
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
            CALCULATE, TEMPERATURE
        }

        public enum PumpMode
        {
            NEVER, TRANS, ALWAYS
        }

        private BindingList<TimesRow> TimesList = new BindingList<TimesRow>();
        MatFile DataFile;
        MatVar<int> RawData;
        MatVar<double> LuiData;

        public const double DefaultGsDelay = 3.2E-8;

        public TroaControl(LuiConfig Config) : base(Config)
        {
            InitializeComponent();
            Init();

            TimesList.AllowEdit = false;
            TimesView.DefaultValuesNeeded += (sender, e) => { e.Row.Cells["Value"].Value = 0; };
            TimesView.DataSource = new BindingSource(TimesList, null);
            TimesView.CellValidating += TimesView_CellValidating;
            TimesView.CellEndEdit += TimesView_CellEndEdit;
            
            SaveData.Click += (sender, e) => SaveOutput();

            DdgConfigBox.Config = Config;
            DdgConfigBox.Commander = Commander;
            DdgConfigBox.AllowZero = false;
            DdgConfigBox.HandleParametersChanged(this, EventArgs.Empty);

            GsDelay.TextChanged += GsDelay_TextChanged;
            GsDelay.LostFocus += GsDelay_LostFocus;
            GsDelay.KeyPress += GsDelay_KeyPress;
        }

        private void Init()
        {
            SuspendLayout();

            ScanProgress.Text = "0";
            TimeProgress.Text = "0";

            NScan.Minimum = 2;
            NScan.Increment = 2;
            NScan.ValueChanged += (sender, e) =>
            {
                if (NScan.Value % 2 != 0) NScan.Value += 1;
            };

            SaveData.Enabled = false;

            GsDelay.Text = DefaultGsDelay.ToString("E3");

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
            if (Settings.TryGetValue("GsDelay", out value) && value != null && value != "")
                GsDelay.Text = value;
        }

        protected override void SaveSettings()
        {
            base.SaveSettings();
            var Settings = Config.TabSettings[this.GetType().Name];
            Settings["PrimaryDelayDdg"] = DdgConfigBox.PrimaryDelayDdg != null ? DdgConfigBox.PrimaryDelayDdg.Name : null;
            Settings["PrimaryDelayDelay"] = DdgConfigBox.PrimaryDelayDelay != null ? DdgConfigBox.PrimaryDelayDelay : null;
            Settings["GsDelay"] = GsDelay.Text;
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
            if (Times.Count < 1)
            {
                MessageBox.Show("Time delay series must be configured.", "Error", MessageBoxButtons.OK);
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

            SetupWorker();
            worker.RunWorkerAsync(new WorkArgs(N, Times, DdgConfigBox.PrimaryDelayDelay, DdgConfigBox.PrimaryDelayTrigger, 
                Mode, (int)Discard.Value, double.Parse(GsDelay.Text)));
            OnTaskStarted(EventArgs.Empty);
        }

        public override void OnTaskStarted(EventArgs e)
        {
            base.OnTaskStarted(e);
            DdgConfigBox.Enabled = false;
            PumpBox.Enabled = false;
            ExperimentConfigBox.Enabled = false;
            LoadTimes.Enabled = SaveData.Enabled = false;
            ScanProgress.Text = "0";
            TimeProgress.Text = "0";
        }

        public override void OnTaskFinished(EventArgs e)
        {
            base.OnTaskFinished(e);
            DdgConfigBox.Enabled = true;
            PumpBox.Enabled = true;
            ExperimentConfigBox.Enabled = true;
            LoadTimes.Enabled = SaveData.Enabled = true;
        }

        private void DoTempCheck(Func<bool> Breakout)
        {
            if (Commander.Camera is CameraTempControlled)
            {
                var camct = (CameraTempControlled)Commander.Camera;
                if (camct.TemperatureStatus != CameraTempControlled.TemperatureStabilized)
                {
                    bool equil = (bool)Invoke(new Func<bool>(TemperatureStabilizedDialog));
                    if (equil)
                    {
                        if (camct.EquilibrateUntil(Breakout)) return;
                    }
                }
            }
        }

        /// <summary>
        /// Acquire in loop, exit early if breakout function returns true.
        /// </summary>
        /// <param name="AcqBuffer">Array for raw camera data.</param>
        /// <param name="DataBuffer">Array for binned camera data.</param>
        /// <param name="SumBuffer">Array for accumulated binned data.</param>
        /// <param name="N"></param>
        /// <param name="Breakout"></param>
        private void DoAcq(int[] AcqBuffer, int[] DataBuffer, int[] SumBuffer, int N, Func<int,bool> Breakout)
        {
            Array.Clear(SumBuffer, 0, SumBuffer.Length);
            for (int i = 0; i < N; i++)
            {
                Array.Clear(DataBuffer, 0, DataBuffer.Length);
                TryAcquire(AcqBuffer);
                Data.ColumnSum(DataBuffer, AcqBuffer);
                Data.Accumulate(SumBuffer, DataBuffer);
                RawData.WriteNext(DataBuffer, 0);
                if (Breakout(i)) return;
            }
        }

        protected override void DoWork(object sender, DoWorkEventArgs e)
        {
            DoTempCheck(() => PauseCancelProgress(e, 0, new ProgressObject(null, 0, Dialog.TEMPERATURE)));

            if (PauseCancelProgress(e, 0, new ProgressObject(null, 0, Dialog.INITIALIZE))) return; // Show zero progress.

            var args = (WorkArgs)e.Argument;
            int N = args.N; // Save typing for later.
            int half = N / 2; // N is always even.
            IList<double> Times = args.Times;
            int AcqSize = Commander.Camera.AcqSize;
            int AcqWidth = Commander.Camera.AcqWidth;

            // Total scans = dark scans + ground state scans + plus time series scans.
            int TotalScans = N + half + Times.Count * (half + N);

            // Create the data store.
            InitDataFile(AcqWidth, TotalScans, Times.Count);

            // Write dummy value (number of scans).
            LuiData.Write((double)args.N, new long[] { 0, 0 });
            // Write wavelengths.
            long[] RowSize = { 1, AcqWidth };
            LuiData.Write(Commander.Camera.Calibration, new long[] { 0, 1 }, RowSize);
            // Write times.
            long[] ColSize = { Times.Count, 1 };
            LuiData.Write(Times.ToArray(), new long[] { 1, 0 }, ColSize);

            // Buffers for acuisition data.
            int[] AcqBuffer = new int[AcqSize];
            int[] AcqRow = new int[AcqWidth];
            int[] Drk = new int[AcqWidth];
            int[] Gnd1 = new int[AcqWidth];
            int[] Gnd2 = new int[AcqWidth];
            int[] Exc = new int[AcqWidth];
            double[] Ground = new double[AcqWidth];
            double[] Excited = new double[AcqWidth];
            double[] Dark = new double[AcqWidth];

            // Set delay for GS before dark in case old delay still on DG.
            Commander.DDG.SetDelay(args.PrimaryDelayName, args.TriggerName, args.GsDelay);

            // Collect dark.
            if (PauseCancelProgress(e, -1, new ProgressObject(null, 0, Dialog.PROGRESS_DARK))) return;
            Commander.BeamFlag.CloseLaserAndFlash();
            DoAcq(AcqBuffer, AcqRow, Drk, N, (p) => PauseCancelProgress(e, p, new ProgressObject(null, 0, Dialog.PROGRESS_DARK)));
            if (PauseCancelProgress(e, -1, new ProgressObject(null, 0, Dialog.PROGRESS))) return;
            Data.Accumulate(Dark, Drk);
            Data.DivideArray(Dark, N);

            // Run data collection scheme.
            if (PauseCancelProgress(e, -1, new ProgressObject(null, 0, Dialog.PROGRESS_FLASH))) return;
            Commander.DDG.SetDelay(args.PrimaryDelayName, args.TriggerName, args.GsDelay); // Set delay for GS.
            if (args.Pump == PumpMode.ALWAYS) OpenPump(args.Discard);
            Commander.BeamFlag.OpenFlash();
            DoAcq(AcqBuffer, AcqRow, Gnd1, half, (p) => PauseCancelProgress(e, p, new ProgressObject(null, 0, Dialog.PROGRESS_FLASH)));
            if (PauseCancelProgress(e, -1, new ProgressObject(null, 0, Dialog.PROGRESS))) return;

            for (int i = 0; i < Times.Count; i++)
            {
                double Delay = Times[i];
                Commander.DDG.SetDelay(args.PrimaryDelayName, args.TriggerName, Delay); // Set delay time.
                if (args.Pump == PumpMode.TRANS) OpenPump(args.Discard);
                Commander.BeamFlag.OpenLaserAndFlash();
                if (PauseCancelProgress(e, -1, new ProgressObject(null, Delay, Dialog.PROGRESS_TIME))) return;
                DoAcq(AcqBuffer, AcqRow, Exc, N, (p) => PauseCancelProgress(e, p, new ProgressObject(null, Delay, Dialog.PROGRESS_TRANS)));
                if (PauseCancelProgress(e, -1, new ProgressObject(null, 0, Dialog.PROGRESS))) return;
                if (args.Pump == PumpMode.TRANS) Commander.Pump.SetClosed();
                if (PauseCancelProgress(e, -1, new ProgressObject(null, 0, Dialog.PROGRESS_FLASH))) return;
                Commander.BeamFlag.OpenFlash();
                Commander.DDG.SetDelay(args.PrimaryDelayName, args.TriggerName, args.GsDelay); // Set delay for GS.
                if (i % 2 == 0) // Alternate between Gnd1 and Gnd2.
                {
                    DoAcq(AcqBuffer, AcqRow, Gnd2, half, (p) => PauseCancelProgress(e, p % half + half, new ProgressObject(null, 0, Dialog.PROGRESS_FLASH)));
                    if (PauseCancelProgress(e, -1, new ProgressObject(null, 0, Dialog.PROGRESS))) return;
                }
                else
                {
                    DoAcq(AcqBuffer, AcqRow, Gnd1, half, (p) => PauseCancelProgress(e, p, new ProgressObject(null, 0, Dialog.PROGRESS_FLASH)));
                    if (PauseCancelProgress(e, -1, new ProgressObject(null, 0, Dialog.PROGRESS))) return;
                }
                Data.Accumulate(Ground, Gnd1);
                Data.Accumulate(Ground, Gnd2);
                Data.DivideArray(Ground, N);
                Data.Accumulate(Excited, Exc);
                Data.DivideArray(Excited, N);
                double[] deltaOD = Data.DeltaOD(Ground, Excited, Dark);
                LuiData.Write(deltaOD, new long[] { i + 1, 1 }, RowSize);
                if (PauseCancelProgress(e, i, new ProgressObject(deltaOD, Delay, Dialog.PROGRESS_TIME_COMPLETE))) return;
                Array.Clear(Ground, 0, Ground.Length);
                Array.Clear(Excited, 0, Excited.Length);
            }
            if (args.Pump != PumpMode.NEVER) Commander.Pump.SetClosed();
            Commander.BeamFlag.CloseLaserAndFlash();
            Commander.DDG.SetDelay(args.PrimaryDelayName, args.TriggerName, args.GsDelay); // Don't leave last delay on DG.
        }

        protected override void WorkProgress(object sender, ProgressChangedEventArgs e)
        {
            var progress = (ProgressObject)e.UserState;
            //StatusProgress.Value = e.ProgressPercentage;
            var progressValue = (e.ProgressPercentage + 1).ToString();
            switch (progress.Status)
            {
                case Dialog.INITIALIZE:
                    ProgressLabel.Text = "Initializing";
                    break;
                case Dialog.PROGRESS:
                    break;
                case Dialog.PROGRESS_DARK:
                    ProgressLabel.Text = "Collecting dark";
                    ScanProgress.Text = progressValue + "/" + NScan.Value.ToString();
                    break;
                case Dialog.PROGRESS_TIME:
                    DdgConfigBox.PrimaryDelayValue = progress.Delay;
                    break;
                case Dialog.PROGRESS_TIME_COMPLETE:
                    TimeProgress.Text = progressValue + "/" + Times.Count.ToString();
                    Display(progress.Data);
                    break;
                case Dialog.PROGRESS_FLASH:
                    ProgressLabel.Text = "Collecting ground";
                    ScanProgress.Text = progressValue + "/" + NScan.Value.ToString();
                    break;
                case Dialog.PROGRESS_TRANS:
                    ProgressLabel.Text = "Collecting transient";
                    ScanProgress.Text = progressValue + "/" + NScan.Value.ToString();
                    break;
                case Dialog.CALCULATE:
                    ProgressLabel.Text = "Calculating...";
                    break;
                case Dialog.TEMPERATURE:
                    ProgressLabel.Text = "Waiting for temperature...";
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
            saveFile.Title = "Save As";
            var result = saveFile.ShowDialog();

            if (result != DialogResult.OK || saveFile.FileName == "") return;

            if (File.Exists(saveFile.FileName)) File.Delete(saveFile.FileName);

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

        private bool TemperatureStabilizedDialog()
        {
            var result = MessageBox.Show("Camera temperature has not stabilized. Wait before running?", "Error", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Cancel)
            {
                worker.CancelAsync();
            }
            else if (result == DialogResult.Yes)
            {
                return true;
            }
            return false;
        }

        void GsDelay_LostFocus(object sender, EventArgs e)
        {
            double value;
            if (!double.TryParse(GsDelay.Text, out value))
            {
                GsDelay.Text = GsDelay.Tag != null ? (string)GsDelay.Tag : DefaultGsDelay.ToString("E3");
            }
        }

        void GsDelay_TextChanged(object sender, EventArgs e)
        {
            double value;
            if (!double.TryParse(GsDelay.Text, out value))
            {
                GsDelay.ForeColor = Color.Red;
            }
            else
            {
                GsDelay.ForeColor = Color.Black;
                GsDelay.Tag = value.ToString("E3");
            }
        }

         void GsDelay_KeyPress(object sender, KeyPressEventArgs e)
        {
            Keys key = (Keys)e.KeyChar;
            if (key == Keys.Enter)
            {
                GsDelay.Text = GsDelay.Tag != null ? (string)GsDelay.Tag : DefaultGsDelay.ToString("E3");
                e.Handled = true;
            }
            if (key == Keys.Escape)
            {
                GsDelay.Text = GsDelay.Tag != null ? (string)GsDelay.Tag : DefaultGsDelay.ToString("E3");
                e.Handled = true;
            }
        }

    }
}

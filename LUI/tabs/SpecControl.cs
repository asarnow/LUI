using lasercom;
using lasercom.camera;
using lasercom.control;
using lasercom.io;
using LUI.config;
using LUI.controls;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace LUI.tabs
{
    public partial class SpecControl : LuiTab
    {

        MatFile DataFile;
        MatVar<int> RawData;

        double[] OD = null;

        int _SelectedChannel = -1;
        int SelectedChannel
        {
            get
            {
                return _SelectedChannel;
            }
            set
            {
                _SelectedChannel = Math.Max(Math.Min(value, (int)Commander.Camera.Width - 1), 0);
                if (OD != null) CountsDisplay.Text = OD[_SelectedChannel].ToString("n");
            }
        }

        struct WorkArgs
        {
            public WorkArgs(int N, PumpMode Pump, bool DiscardFirst)
            {
                this.N = N;
                this.Pump = Pump;
                this.DiscardFirst = DiscardFirst;
            }
            public readonly int N;
            public readonly PumpMode Pump;
            public readonly bool DiscardFirst;
        }

        public enum PumpMode
        {
            ALWAYS, NEVER
        }

        public enum Dialog
        {
            BLANK, SAMPLE, PROGRESS, PROGRESS_BLANK, PROGRESS_DARK, PROGRESS_DATA,
            PROGRESS_CALC
        }

        public SpecControl(LuiConfig Config) : base(Config)
        {
            InitializeComponent();

            SaveData.Click += (sender, e) => SaveOutput();
            SaveData.Enabled = false; //TODO Implement save data.

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            PumpBox.ObjectChanged += HandlePumpChanged;
        }

        public override void HandleParametersChanged(object sender, EventArgs e)
        {
            base.HandleParametersChanged(sender, e); // Takes care of ObjectSelectPanel.

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

        public virtual void HandlePumpChanged(object sender, EventArgs e)
        {
            if (Commander.Pump != null) Commander.Pump.SetClosed();
            Commander.Pump = (IPump)Config.GetObject(PumpBox.SelectedObject);
        }

        protected override void Collect_Click(object sender, EventArgs e)
        {
            Collect.Enabled = NScan.Enabled = CameraGain.Enabled = false;
            Abort.Enabled = true;

            //CameraStatus.Text = "";

            Graph.ClearData();
            Graph.Invalidate();

            int N = (int)NScan.Value;
            PumpMode Mode;
            if (PumpNever.Checked) Mode = PumpMode.NEVER;
            else Mode = PumpMode.ALWAYS;

            Commander.BeamFlag.CloseLaserAndFlash();

            worker = new BackgroundWorker();
            worker.DoWork += new System.ComponentModel.DoWorkEventHandler(DoWork);
            worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(WorkProgress);
            worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(WorkComplete);
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync(new WorkArgs(N, Mode, Discard.Checked));
            OnTaskStarted(EventArgs.Empty);
        }

        protected override void DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            Commander.Camera.AcquisitionMode = AndorCamera.AcquisitionModeSingle;
            Commander.Camera.TriggerMode = AndorCamera.TriggerModeExternalExposure;
            Commander.Camera.DDGTriggerMode = AndorCamera.DDGTriggerModeExternal;
            Commander.Camera.ReadMode = AndorCamera.ReadModeFVB;
            WorkArgs args = (WorkArgs)e.Argument;
            int N = args.N;

            worker.ReportProgress(0, Dialog.PROGRESS_DARK.ToString());

            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            Commander.BeamFlag.CloseLaserAndFlash();

            int[] DarkBuffer = Commander.Camera.Acquire();
            for (int i = 0; i < N - 1; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                Data.Accumulate(DarkBuffer, Commander.Camera.Acquire());
                worker.ReportProgress(33 + (i / N) * 33, Dialog.PROGRESS_DARK.ToString());
            }

            worker.ReportProgress(33, Dialog.BLANK.ToString());

            wait = true;
            bool[] waitparam = { wait };
            //args.UI.Invoke(new Action(BlockingBlankDialog), waitparam);
            Invoke(new Action(BlockingBlankDialog));
            //while (wait) ;

            Commander.BeamFlag.OpenFlash();

            int[] BlankBuffer = Commander.Camera.Acquire();
            for (int i = 0; i < N - 1; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                Data.Accumulate(BlankBuffer, Commander.Camera.Acquire());
                worker.ReportProgress((i / N) * 33, Dialog.PROGRESS_BLANK.ToString());
            }

            Commander.BeamFlag.CloseLaserAndFlash();

            worker.ReportProgress(66, Dialog.SAMPLE.ToString());

            wait = true;
            Invoke(new Action(BlockingSampleDialog));
            //while (wait) ;

            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            worker.ReportProgress(66, Dialog.PROGRESS_DATA.ToString());

            Commander.BeamFlag.OpenFlash();

            if (args.Pump == PumpMode.ALWAYS)
            {
                Commander.Pump.SetOpen();
                if (args.DiscardFirst)
                {
                    var ret = Commander.Camera.Acquire();
                }
            }

            int[] DataBuffer = Commander.Camera.Acquire();
            for (int i = 0; i < N - 1; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                Data.Accumulate(DataBuffer, Commander.Camera.Acquire());
                worker.ReportProgress(66 + (i / N) * 33, Dialog.PROGRESS_DATA.ToString());
            }
            Commander.BeamFlag.CloseLaserAndFlash();

            if (args.Pump == PumpMode.ALWAYS)
            {
                Commander.Pump.SetClosed();
            }

            worker.ReportProgress(99, Dialog.PROGRESS_CALC.ToString());
            e.Result = Data.OpticalDensity(DataBuffer, BlankBuffer, DarkBuffer);
        }

        protected override void WorkProgress(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            Dialog operation = (Dialog)Enum.Parse(typeof(Dialog), (string)e.UserState);
            switch (operation)
            {
                case Dialog.BLANK:
                    StatusProgress.Value = e.ProgressPercentage;
                    ProgressLabel.Text = "Waiting";
                    break;
                case Dialog.SAMPLE:
                    StatusProgress.Value = e.ProgressPercentage;
                    ProgressLabel.Text = "Waiting";
                    break;
                case Dialog.PROGRESS:
                    StatusProgress.Value = e.ProgressPercentage;
                    ProgressLabel.Text = "Busy";
                    break;
                case Dialog.PROGRESS_BLANK:
                    StatusProgress.Value = e.ProgressPercentage;
                    ProgressLabel.Text = "Collecting blank";
                    break;
                case Dialog.PROGRESS_DARK:
                    StatusProgress.Value = e.ProgressPercentage;
                    ProgressLabel.Text = "Collecting dark";
                    break;
                case Dialog.PROGRESS_DATA:
                    StatusProgress.Value = e.ProgressPercentage;
                    ProgressLabel.Text = "Collecting data";
                    break;
                case Dialog.PROGRESS_CALC:
                    StatusProgress.Value = e.ProgressPercentage;
                    ProgressLabel.Text = "Calculating";
                    break;
            }
        }

        protected override void WorkComplete(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            Commander.BeamFlag.CloseLaserAndFlash();
            if (!e.Cancelled)
            {
                OD = (double[])e.Result;
                for (int i = 0; i < OD.Length; i++) if (Double.IsNaN(OD[i]) || Double.IsInfinity(OD[i])) OD[i] = 0;
                Graph.DrawPoints(Commander.Camera.Calibration, OD);
                Graph.Invalidate();
                ProgressLabel.Text = "Complete";
            }
            else
            {
                ProgressLabel.Text = "Aborted";
            }
            StatusProgress.Value = 100;
            Collect.Enabled = NScan.Enabled = CameraGain.Enabled = true;
            Abort.Enabled = false;
            OnTaskFinished(EventArgs.Empty);
        }

        protected override void Graph_Click(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            // Selects a *physical channel* on the camera.
            SelectedChannel = Commander.Camera.CalibrationAscending ?
                (int)Math.Round(Graph.AxesToNormalized(Graph.ScreenToAxes(new Point(e.X, e.Y))).X * (Commander.Camera.Width - 1))
                :
                (int)Math.Round((1 - Graph.AxesToNormalized(Graph.ScreenToAxes(new Point(e.X, e.Y))).X) * (Commander.Camera.Width - 1));
            RedrawLines();
        }

        private void RedrawLines()
        {
            Graph.ClearAnnotation();
            Graph.Annotate(GraphControl.Annotation.VERTLINE, Graph.ColorOrder[0], Commander.Camera.Calibration[SelectedChannel]);
            Graph.Invalidate();
        }

        private void SaveOutput()
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            //saveFile.Filter = "MAT File|*.mat|CSV File|*.csv";
            saveFile.Filter = "MAT File|*.mat";
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
                    throw new NotImplementedException();
                    //if (DataFile != null)
                    //{
                    //    if (DataFile.Closed) DataFile.Reopen();
                    //    //MatVar<double> luiData = (MatVar<double>)DataFile["LuiData"];

                    //    if (!LuiData.Closed)
                    //    {
                    //        double[,] Matrix = new double[LuiData.Dims[0], LuiData.Dims[1]];
                    //        LuiData.Read(Matrix, new long[] { 0, 0 }, LuiData.Dims);
                    //        FileIO.WriteMatrix(saveFile.FileName, Matrix);
                    //    }

                    //    DataFile.Close();
                    //}
                    break;
            }
        }
    }
}

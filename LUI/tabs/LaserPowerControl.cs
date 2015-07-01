using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using lasercom;
using lasercom.camera;
using lasercom.control;
using LUI.config;
using LUI.controls;

namespace LUI.tabs
{
    public partial class LaserPowerControl : LuiTab
    {
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

        double[] Light = null;

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
                if (Light != null) CountsDisplay.Text = Light[_SelectedChannel].ToString("n");
            }
        }

        public enum PumpMode
        {
            NEVER, TRANS, ALWAYS
        }

        public enum Dialog
        {
            INITIALIZE, PROGRESS, PROGRESS_DARK,
            PROGRESS_FLASH, PROGRESS_TRANS, CALCULATE
        }
        public LaserPowerControl(LuiConfig Config) : base(Config)
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            PumpBox.ObjectChanged += HandlePumpChanged;
            base.OnLoad(e);
        }

        public override void HandleParametersChanged(object sender, EventArgs e)
        {
            base.HandleParametersChanged(sender, e);
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
            int N = (int)NScan.Value;
            PumpMode Pump;
            if (PumpNever.Checked) Pump = PumpMode.NEVER;
            else if (PumpTs.Checked) Pump = PumpMode.TRANS;
            else Pump = PumpMode.ALWAYS;
            Commander.BeamFlag.CloseLaserAndFlash();
            worker = new BackgroundWorker();
            worker.DoWork += new System.ComponentModel.DoWorkEventHandler(DoWork);
            worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(WorkProgress);
            worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(WorkComplete);
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync(new WorkArgs(N, Pump, Discard.Checked));
            OnTaskStarted(EventArgs.Empty);
        }

        protected override void DoWork(object sender, DoWorkEventArgs e)
        {
            if (PauseCancelProgress(e, 0, Dialog.INITIALIZE)) return;

            var args = (WorkArgs)e.Argument;
            int N = args.N;

            int TotalScans = 3 * N;

            int AcqSize = (int)Commander.Camera.AcqSize;
            int finalSize = Commander.Camera.ReadMode == AndorCamera.ReadModeImage ?
                AcqSize / Commander.Camera.Image.Height : AcqSize;

            if (PauseCancelProgress(e, 0, Dialog.PROGRESS_DARK)) return;

            Commander.BeamFlag.CloseLaserAndFlash();

            int[] DataBuffer = new int[AcqSize];
            double[] Dark = new double[finalSize];
            for (int i = 0; i < N; i++)
            {
                uint ret = Commander.Camera.Acquire(DataBuffer);

                Data.ColumnSum(Dark, DataBuffer);

                if (PauseCancelProgress(e, i * 99 / TotalScans, Dialog.PROGRESS_DARK)) return;
            }
            Data.DivideArray(Dark, N);

            if (PauseCancelProgress(e, 33, Dialog.PROGRESS_FLASH)) return;

            // Flow-flash.
            if (args.Pump == PumpMode.ALWAYS)
            {
                Commander.Pump.SetOpen();
                if (args.DiscardFirst)
                {
                    var ret = Commander.Camera.Acquire(DataBuffer);
                }
            }

            Commander.BeamFlag.OpenFlash();

            double[] Ground = new double[finalSize];
            for (int i = 0; i < N; i++)
            {
                uint ret = Commander.Camera.Acquire(DataBuffer);
                
                Data.ColumnSum(Ground, DataBuffer);

                if (PauseCancelProgress(e, (N + i) * 99 / TotalScans, Dialog.PROGRESS_FLASH)) return;
            }
            Data.DivideArray(Ground, N);
            Data.Dissipate(Ground, Dark);

            if (PauseCancelProgress(e, 66, Dialog.PROGRESS_TRANS)) return;

            // Flow-flash.
            if (args.Pump == PumpMode.TRANS)
            {
                Commander.Pump.SetOpen();
                if (args.DiscardFirst)
                {
                    var ret = Commander.Camera.Acquire(DataBuffer);
                }
            }

            Commander.BeamFlag.OpenLaserAndFlash();

            double[] Excited = new double[finalSize];
            for (int i = 0; i < N; i++)
            {
                uint ret = Commander.Camera.Acquire(DataBuffer);

                Data.ColumnSum(Excited, DataBuffer);

                if (PauseCancelProgress(e, (2 * N + i) * 99 / TotalScans, Dialog.PROGRESS_TRANS)) return;
            }

            Commander.BeamFlag.CloseLaserAndFlash();

            // Flow-flash.
            if (args.Pump == PumpMode.TRANS || args.Pump == PumpMode.ALWAYS) // Could close pump before last collect.
            {
                Commander.Pump.SetClosed();
            }

            Data.DivideArray(Excited, N);
            Data.Dissipate(Excited, Dark);

            if (PauseCancelProgress(e, 99, Dialog.CALCULATE)) return;
            
            e.Result = Data.DeltaOD(Ground, Excited);
        }

        protected override void WorkProgress(object sender, ProgressChangedEventArgs e)
        {
            Dialog operation = (Dialog)Enum.Parse(typeof(Dialog), e.UserState.ToString());
            StatusProgress.Value = e.ProgressPercentage;
            switch (operation)
            {
                case Dialog.INITIALIZE:
                    ProgressLabel.Text = "Initializing";
                    break;
                case Dialog.PROGRESS:
                    break;
                case Dialog.PROGRESS_DARK:
                    ProgressLabel.Text = "Collecting dark";
                    break;
                case Dialog.PROGRESS_FLASH:
                    ProgressLabel.Text = "Collecting ground";
                    break;
                case Dialog.PROGRESS_TRANS:
                    ProgressLabel.Text = "Collecting excited";
                    break;
                case Dialog.CALCULATE:
                    ProgressLabel.Text = "Calculating";
                    break;
            }
        }

        protected override void WorkComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            Commander.BeamFlag.CloseLaserAndFlash();
            Commander.Pump.SetClosed();
            if (!e.Cancelled)
            {
                Light = (double[])e.Result;
                Display();
                SelectedChannel = SelectedChannel;
                ProgressLabel.Text = "Complete";
            }
            else
            {
                ProgressLabel.Text = "Aborted";
            }
            OnTaskFinished(EventArgs.Empty);
        }

        protected override void Graph_Click(object sender, MouseEventArgs e)
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

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                    if (SelectedChannel > -1)
                    {
                        if (Commander.Camera.CalibrationAscending) SelectedChannel--;
                        else SelectedChannel++;
                    }
                    RedrawLines();
                    break;
                case Keys.Right:
                    if (SelectedChannel > -1)
                    {
                        if (Commander.Camera.CalibrationAscending) SelectedChannel--;
                        else SelectedChannel++;
                    }
                    RedrawLines();
                    break;
                case Keys.Enter:
                    //TODO add calibration point
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void TBKeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(char.IsDigit(e.KeyChar)))
            {
                Keys key = (Keys)e.KeyChar;

                if (!(key == Keys.Back || key == Keys.Delete))
                {
                    e.Handled = true;
                }
            }
        }

        private void Display()
        {
            Graph.ClearData();
            Graph.Invalidate();

            if (Light != null)
            {
                Graph.DrawPoints(Commander.Camera.Calibration, Light);
            }

            Graph.Invalidate();
        }
    }
}

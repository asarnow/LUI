using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Threading;
using lasercom;
using lasercom.camera;
using LUI.config;
using LUI.controls;

namespace LUI.tabs
{
    public partial class LaserPowerControl : LuiTab
    {
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

        public enum Dialog
        {
            INITIALIZE, PROGRESS, PROGRESS_DARK,
            PROGRESS_FLASH, PROGRESS_TRANS, CALCULATE
        }
        public LaserPowerControl(LuiConfig Config) : base(Config)
        {
            InitializeComponent();
        }

        protected override void DoWork(object sender, DoWorkEventArgs e)
        {
            worker.ReportProgress(0, Dialog.INITIALIZE);

            Commander.Camera.AcquisitionMode = AndorCamera.AcquisitionModeSingle;
            Commander.Camera.TriggerMode = AndorCamera.TriggerModeExternalExposure;
            Commander.Camera.ReadMode = AndorCamera.ReadModeFVB;
            int N = (int)e.Argument;

            int TotalScans = 3 * N;

            worker.ReportProgress(0, Dialog.PROGRESS_DARK);

            int[] DataBuffer = new int[Commander.Camera.AcqSize];
            double[] Dark = new double[Commander.Camera.AcqSize];
            for (int i = 0; i < N; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                uint ret = Commander.Dark(DataBuffer);

                Data.Accumulate(Dark, DataBuffer);

                worker.ReportProgress(i * 99 / TotalScans, Dialog.PROGRESS_DARK);
            }
            Data.DivideArray(Dark, N);

            worker.ReportProgress(33, Dialog.PROGRESS_FLASH);

            double[] Ground = new double[Commander.Camera.AcqSize];
            for (int i = 0; i < N; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                uint ret = Commander.Flash(DataBuffer);
                
                Data.Accumulate(Ground, DataBuffer);

                worker.ReportProgress((N + i) * 99 / TotalScans, Dialog.PROGRESS_FLASH);
            }
            Data.DivideArray(Ground, N);
            Data.Dissipate(Ground, Dark);

            worker.ReportProgress(66, Dialog.PROGRESS_TRANS);

            double[] Excited = new double[Commander.Camera.AcqSize];
            for (int i = 0; i < N; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                uint ret = Commander.Trans(DataBuffer);

                Data.Accumulate(Excited, DataBuffer);

                worker.ReportProgress((2*N + i) * 99 / TotalScans, Dialog.PROGRESS_TRANS);
            }
            Data.DivideArray(Excited, N);
            Data.Dissipate(Excited, Dark);


            worker.ReportProgress(99, Dialog.CALCULATE);
            
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
            StatusProgress.Value = 100;
            Collect.Enabled = true;
        }

        protected override void Graph_Click(object sender, MouseEventArgs e)
        {
            // Selects a *physical channel* on the camera.
            SelectedChannel = (int)Math.Round(Graph.AxesToNormalized(Graph.ScreenToAxes(new Point(e.X, e.Y))).X * (Commander.Camera.Width - 1));
            RedrawLines();
        }

        private void RedrawLines()
        {
            Graph.ClearAnnotation();
            Graph.Annotate(GraphControl.Annotation.VERTLINE, Graph.ColorOrder[0], SelectedChannel);
            Graph.Invalidate();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                    if (SelectedChannel > -1)
                    {
                        SelectedChannel--;
                    }
                    RedrawLines();
                    break;
                case Keys.Right:
                    if (SelectedChannel > -1)
                    {
                        SelectedChannel++;
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

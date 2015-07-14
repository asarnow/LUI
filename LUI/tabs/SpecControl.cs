using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CsvHelper;
using lasercom;
using lasercom.camera;
using lasercom.control;
using lasercom.io;
using LUI.config;
using LUI.controls;

namespace LUI.tabs
{
    public partial class SpecControl : LuiTab
    {
        double[] OD = null;
        int[] BlankBuffer = null;

        int _SelectedChannel = -1;
        int SelectedChannel
        {
            get
            {
                return _SelectedChannel;
            }
            set
            {
                _SelectedChannel = Math.Max(Math.Min(value, Commander.Camera.Width - 1), 0);
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

            CurvesView.Graph = Graph;

            SaveData.Click += (sender, e) => SaveOutput();
            SaveData.Enabled = false;

            ClearBlank.Click += ClearBlank_Click;
            ClearBlank.Enabled = false;
        }

        protected override void OnLoad(EventArgs e)
        {
            PumpBox.ObjectChanged += HandlePumpChanged;
            base.OnLoad(e);
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

        public override void OnTaskStarted(EventArgs e)
        {
            base.OnTaskStarted(e);
            ClearBlank.Enabled = false;
            PumpBox.Enabled = false;
            SaveData.Enabled = false;
        }

        public override void OnTaskFinished(EventArgs e)
        {
            base.OnTaskFinished(e);
            ClearBlank.Enabled = true;
            PumpBox.Enabled = true;
            SaveData.Enabled = true;
        }

        protected override void Collect_Click(object sender, EventArgs e)
        {
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
            WorkArgs args = (WorkArgs)e.Argument;
            int N = args.N;

            int AcqSize = Commander.Camera.AcqSize;
            int finalSize = Commander.Camera.ReadMode == AndorCamera.ReadModeImage ? 
                AcqSize / Commander.Camera.Image.Height : AcqSize;

            if (PauseCancelProgress(e, 0, Dialog.PROGRESS_DARK.ToString())) return;

            int[] DataBuffer = new int[AcqSize];
            int[] DarkBuffer = new int[finalSize];

            Commander.BeamFlag.CloseLaserAndFlash();
            for (int i = 0; i < N; i++)
            {
                TryAcquire(DataBuffer);
                Data.ColumnSum(DarkBuffer, DataBuffer);
                if (PauseCancelProgress(e, i+1, Dialog.PROGRESS_DARK.ToString())) return;
            }

            if (PauseCancelProgress(e, 0, Dialog.BLANK.ToString())) return;

            if (BlankBuffer == null || BlankBuffer.Length != Commander.Camera.AcqSize)
            {
                Invoke(new Action(BlockingBlankDialog));

                Commander.BeamFlag.OpenFlash();

                BlankBuffer = new int[finalSize];
                for (int i = 0; i < N; i++)
                {
                    TryAcquire(DataBuffer);
                    Data.ColumnSum(BlankBuffer, DataBuffer);
                    if (PauseCancelProgress(e, i+1, Dialog.PROGRESS_BLANK.ToString())) return;
                }

                Commander.BeamFlag.CloseLaserAndFlash();
                
                if (PauseCancelProgress(e, 0, Dialog.SAMPLE.ToString())) return;

                Invoke(new Action(BlockingSampleDialog));
            }
            else
            {
                if (PauseCancelProgress(e, 0, Dialog.SAMPLE.ToString())) return;
            }
            
            if (PauseCancelProgress(e, 0, Dialog.PROGRESS_DATA.ToString())) return;

            Commander.BeamFlag.OpenFlash();

            if (args.Pump == PumpMode.ALWAYS)
            {
                OpenPump(args.DiscardFirst);
            }

            int[] SampleBuffer = new int[finalSize];
            for (int i = 0; i < N; i++)
            {
                TryAcquire(DataBuffer);
                Data.ColumnSum(SampleBuffer, DataBuffer);
                if (PauseCancelProgress(e, i+1, Dialog.PROGRESS_DATA.ToString())) return;
            }
            Commander.BeamFlag.CloseLaserAndFlash();

            if (args.Pump == PumpMode.ALWAYS)
            {
                Commander.Pump.SetClosed();
            }

            if (PauseCancelProgress(e, -1, Dialog.PROGRESS_CALC.ToString())) return;
            e.Result = Data.OpticalDensity(SampleBuffer, BlankBuffer, DarkBuffer);
        }

        protected override void WorkProgress(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            Dialog operation = (Dialog)Enum.Parse(typeof(Dialog), (string)e.UserState);
            if (e.ProgressPercentage != -1)
                ScanProgress.Text = e.ProgressPercentage.ToString() + "/" + NScan.Value.ToString();
            switch (operation)
            {
                case Dialog.BLANK:
                    ProgressLabel.Text = "Waiting";
                    break;
                case Dialog.SAMPLE:
                    ProgressLabel.Text = "Waiting";
                    break;
                case Dialog.PROGRESS:
                    ProgressLabel.Text = "Busy";
                    break;
                case Dialog.PROGRESS_BLANK:
                    ProgressLabel.Text = "Collecting blank";
                    break;
                case Dialog.PROGRESS_DARK:
                    ProgressLabel.Text = "Collecting dark";
                    break;
                case Dialog.PROGRESS_DATA:
                    ProgressLabel.Text = "Collecting data";
                    break;
                case Dialog.PROGRESS_CALC:
                    ProgressLabel.Text = "Calculating";
                    break;
            }
        }

        protected override void WorkComplete(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            Commander.BeamFlag.CloseLaserAndFlash();
            Commander.Pump.SetClosed();
            if (!e.Cancelled)
            {
                OD = (double[])e.Result;
                CurvesView.Add(OD);
                for (int i = 0; i < OD.Length; i++) if (Double.IsNaN(OD[i]) || Double.IsInfinity(OD[i])) OD[i] = 0;
                Graph.DrawPoints(Commander.Camera.Calibration, OD);
                Graph.Invalidate();
                ProgressLabel.Text = "Complete";
                SaveData.Enabled = true;
                SelectedChannel = Commander.Camera.Width / 2;
            }
            else
            {
                ProgressLabel.Text = "Aborted";
            }
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

        void ClearBlank_Click(object sender, EventArgs e)
        {
            BlankBuffer = null;
            ClearBlank.Enabled = false;
        }

        void ExportCurvesToMat(string FileName)
        {
            MatFile DataFile = new MatFile(FileName);
            foreach (string CurveName in CurvesView.SaveCurveNames)
            {
                var curve = CurvesView.FindCurveByName(CurveName);
                var V = DataFile.CreateVariable<double>(CurveName, 1, curve.Length);
                V.WriteNext(curve, 0);
            }
            DataFile.Dispose();
        }

        void ExportCurvesToCsv(string FileName)
        {
            TextWriter writer = new StreamWriter(FileName);
            CsvWriter csv = new CsvWriter(writer);
            var headers = CurvesView.SaveCurveNames.ToList();
            var curves = CurvesView.SaveCurves.ToList();
            foreach(string header in headers) csv.WriteField(header);
            csv.NextRecord();
            for (int i = 0; i < curves[0].Count; i++)
            {
                for (int j = 0; j < curves.Count; j++)
                {
                    csv.WriteField(curves[j][i]);
                }
                csv.NextRecord();
            }
            writer.Close();
        }

        private void SaveOutput()
        {
            if (OD == null || OD.Length == 0)
            {
                MessageBox.Show("No data available.", "Error", MessageBoxButtons.OK);
                return;
            }

            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "MAT File|*.mat|CSV File|*.csv";
            saveFile.Title = "Save As";
            var result = saveFile.ShowDialog();

            if (result != DialogResult.OK || saveFile.FileName == "") return;

            if (File.Exists(saveFile.FileName)) File.Delete(saveFile.FileName);

            switch (saveFile.FilterIndex)
            {
                case 1: // MAT file.
                    ExportCurvesToMat(saveFile.FileName);
                    break;
                case 2: // CSV file.
                    ExportCurvesToCsv(saveFile.FileName);
                    break;
            }
        }
    }
}

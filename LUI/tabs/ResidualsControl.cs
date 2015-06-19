using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using lasercom;
using lasercom.camera;
using lasercom.ddg;
using lasercom.io;
using LUI.config;
using LUI.controls;

namespace LUI.tabs
{
    public partial class ResidualsControl : LuiTab
    {
        private double[] Light = null;
        private double[] LastLight = null;
        private double[] CumulativeLight = null;
        private double[] _DiffLight = null;
        private double[] DiffLight
        {
            get
            {
                return _DiffLight;
            }
            set
            {
                _DiffLight = value;
                DiffSum.Text = _DiffLight.Sum().ToString("n");
            }
        }

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
            }
        }

        int LowerBound { get; set; }
        int UpperBound { get; set; }

        public enum Dialog
        {
            PROGRESS_CAMERA, PROGRESS_DATA
        }

        struct WorkArgs
        {
            public WorkArgs(int NScans, int NAverage, bool CollectLaser)
            {
                this.NScans = NScans;
                this.NAvg = NAverage;
                this.CollectLaser = CollectLaser;
            }
            public readonly int NScans;
            public readonly int NAvg;
            public readonly bool CollectLaser;
        }

        struct ProgressObject
        {
            public ProgressObject(object Data, int Counts, double VarCounts, int Peak, double VarPeak, 
                int CountsN, double VarCountsN, int PeakN, double VarPeakN, Dialog Status)
            {
                this.Data = Data;
                this.Counts = Counts;
                this.VarCounts = VarCounts;
                this.Peak = Peak;
                this.VarPeak = VarPeak;
                this.CountsN = CountsN;
                this.VarCountsN = VarCountsN;
                this.PeakN = PeakN;
                this.VarPeakN = VarPeakN;
                this.Status = Status;
            }                             
            public readonly object Data;
            public readonly int Counts;
            public readonly int Peak;
            public readonly double VarCounts;
            public readonly double VarPeak;
            public readonly int CountsN;
            public readonly int PeakN;
            public readonly double VarCountsN;
            public readonly double VarPeakN;
            public readonly Dialog Status;
        }

        public ResidualsControl(LuiConfig config) : base(config)
        {
            InitializeComponent();
            Graph.YLabelFormat = "g";

            CollectLaser.CheckedChanged += CollectLaser_CheckedChanged;

            DdgConfigBox.Config = Config;
            DdgConfigBox.Commander = Commander;
            DdgConfigBox.AllowZero = true;
            DdgConfigBox.Enabled = false;
            DdgConfigBox.HandleParametersChanged(this, EventArgs.Empty);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            RedrawLines();
        }

        public override void HandleParametersChanged(object sender, EventArgs e)
        {
            base.HandleParametersChanged(sender, e);
            DdgConfigBox.HandleParametersChanged(sender, e);
        }

        public override void HandleCameraChanged(object sender, EventArgs e)
        {
            base.HandleCameraChanged(sender, e);
            LowerBound = (int)Commander.Camera.Width / 6;
            UpperBound = (int)Commander.Camera.Width * 5 / 6;
        }

        public override void HandleContainingTabSelected(object sender, EventArgs e)
        {
            base.HandleContainingTabSelected(sender, e);
            DdgConfigBox.UpdatePrimaryDelayValue();
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

        protected override void Collect_Click(object sender, EventArgs e)
        {
            Graph.ClearData();
            CumulativeLight = null;

            Commander.BeamFlag.CloseLaserAndFlash();

            if (CollectLaser.Checked)
                DdgConfigBox.ApplyPrimaryDelayValue();

            worker = new BackgroundWorker();
            worker.DoWork += new System.ComponentModel.DoWorkEventHandler(DoWork);
            worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(WorkProgress);
            worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(WorkComplete);
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync(new WorkArgs((int)NScan.Value, (int)NAverage.Value, CollectLaser.Checked));
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
            DdgConfigBox.Enabled = CollectLaser.Checked;
        }

        /// <summary>
        /// Alignment / Residuals background task logic.
        /// For both functions, we need to poll the camera continuously while
        /// updating the graph with the acquired data.
        /// A blank isn't required since we're already looking at the blank
        /// for these functions. Subtracting dark current is also not required
        /// as it would only subtract an equal, fixed amount from every scan.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void DoWork(object sender, DoWorkEventArgs e)
        {
            Commander.Camera.AcquisitionMode = AndorCamera.AcquisitionModeSingle;
            Commander.Camera.TriggerMode = AndorCamera.TriggerModeExternalExposure;
            Commander.Camera.DDGTriggerMode = AndorCamera.DDGTriggerModeExternal;
            Commander.Camera.ReadMode = AndorCamera.ReadModeFVB;
            WorkArgs args = (WorkArgs)e.Argument;

            int cmasum = 0; // Cumulative moving average over scans
            double varsum = 0;
            int cmapeak = 0;
            double varpeak = 0;
            int nsum = 0; // CMA over last NAvg scans only
            double nvarsum = 0;
            int npeak = 0;
            double nvarpeak = 0;
            int[] DataBuffer = new int[Commander.Camera.AcqSize];
            int[] CumulativeDataBuffer = new int[DataBuffer.Length];

            if (args.CollectLaser)
            {
                Commander.BeamFlag.OpenLaserAndFlash();
            }
            else
            {
                Commander.BeamFlag.OpenFlash();
            }

            for (int i = 0; i < args.NScans; i++)
            {
                uint ret = Commander.Camera.Acquire(DataBuffer);

                Data.Accumulate(CumulativeDataBuffer, DataBuffer);

                int sum = 0;
                int peak = int.MinValue;
                for (int j = LowerBound; j <= UpperBound; j++)
                {
                    sum += DataBuffer[j];
                    if (DataBuffer[j] > peak) peak = DataBuffer[j];
                }

                double vartemp;
                int delta = sum - cmasum;
                cmasum += delta / (i + 1);
                vartemp = delta * (sum - cmasum);
                varsum += Math.Sqrt(Math.Abs(vartemp));

                delta = peak - cmapeak;
                cmapeak += delta / (i + 1);
                vartemp = delta * (peak - cmapeak);
                varpeak += Math.Sqrt(Math.Abs(vartemp));
                //cmasum = (sum + i * cmasum) / (i + 1);
                //cmapeak = (peak + i * cmapeak) / (i + 1);

                int n = i % args.NAvg; // Reset NAvg CMA
                if (n == 0) nvarpeak = nvarsum = npeak = nsum = 0;
                //nsum = (sum + n * nsum) / (n + 1);
                //npeak = (peak + n * npeak) / (n + 1);
                delta = sum - nsum;
                nsum += delta / (i + 1);
                vartemp = delta * (sum - nsum);
                nvarsum += Math.Sqrt(Math.Abs(vartemp));

                delta = peak - npeak;
                npeak += delta / (i + 1);
                vartemp = delta * (peak - npeak);
                nvarpeak += Math.Sqrt(Math.Abs(vartemp));

                ProgressObject progress = new ProgressObject(Array.ConvertAll((int[])DataBuffer, x => (double)x), 
                    cmasum, varsum / i, cmapeak, varpeak / i, nsum, nvarsum / i, npeak, nvarpeak / i, Dialog.PROGRESS_DATA);
                if (PauseCancelProgress(e, i * 100 / args.NScans, progress)) return;
            }

            Commander.BeamFlag.CloseLaserAndFlash();

            Data.DivideArray(CumulativeDataBuffer, args.NScans);
            e.Result = Array.ConvertAll((int[])CumulativeDataBuffer, x=> (double)x);
        }

        /// <summary>
        /// Runs in UI thread to report background task progress.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void WorkProgress(object sender, ProgressChangedEventArgs e)
        {
            ProgressObject Progress = (ProgressObject)e.UserState;

            switch (Progress.Status)
            {
                case Dialog.PROGRESS_DATA:
                    Light = (double[])Progress.Data;
                    if (LastLight != null)
                    {
                        DiffLight = (double[])Light.Clone(); // Deep copy for value types only
                        Data.Dissipate(DiffLight, LastLight);
                    }

                    DisplayProgress();

                    Peak.Text = Progress.Peak.ToString("n0") + " \u00B1 " + Progress.VarPeak.ToString("n3");
                    Counts.Text = Progress.Counts.ToString("n0") + " \u00B1 " + Progress.VarCounts.ToString("n0");
                    PeakN.Text = Progress.PeakN.ToString("n0") + " \u00B1 " + Progress.VarPeakN.ToString("n3");
                    CountsN.Text = Progress.CountsN.ToString("n0") + " \u00B1 " + Progress.VarCountsN.ToString("n0");

                    StatusProgress.Value = e.ProgressPercentage;
                    ProgressLabel.Text = "Collecting data";
                    break;
            }
        }

        /// <summary>
        /// Runs in UI thread after background task completed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void WorkComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            Commander.BeamFlag.CloseLaserAndFlash();
            if (e.Error != null)
            {
                // Handle the exception thrown in the worker thread
                MessageBox.Show(e.Error.ToString());
            }
            else if (e.Cancelled)
            {
                ProgressLabel.Text = "Aborted";
            }
            else
            {
                ProgressLabel.Text = "Complete";
                CumulativeLight = (double[])e.Result;
                DisplayComplete();
            }
            OnTaskFinished(EventArgs.Empty);
        }

        protected override void Graph_Click(object sender, MouseEventArgs e)
        {
            SelectedChannel = Commander.Camera.CalibrationAscending ?
                (int)Math.Round(Graph.AxesToNormalized(Graph.ScreenToAxes(new Point(e.X, e.Y))).X * (Commander.Camera.Width - 1))
                :
                (int)Math.Round((1 - Graph.AxesToNormalized(Graph.ScreenToAxes(new Point(e.X, e.Y))).X) * (Commander.Camera.Width - 1));

            // If the click is closer to the LB, update LB. Else (equidistant or closer to UB), update UB.
            if (Math.Abs(SelectedChannel - LowerBound) < Math.Abs(SelectedChannel - UpperBound))
            {
                LowerBound = SelectedChannel;
            }
            else
            {
                UpperBound = SelectedChannel;
            }

            RedrawLines();
        }

        private void RedrawLines()
        {
            Graph.ClearAnnotation();
            Graph.Annotate(GraphControl.Annotation.VERTLINE, Graph.ColorOrder[0], Commander.Camera.Calibration[LowerBound]);
            Graph.Annotate(GraphControl.Annotation.VERTLINE, Graph.ColorOrder[0], Commander.Camera.Calibration[UpperBound]);
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

        private void LoadProfile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Alignment File|*.aln|Text File|*.txt|All Files|*.*";
            openFile.Title = "Load Alignment Profile";
            openFile.ShowDialog();

            if (openFile.FileName == "") return;

            switch (openFile.FilterIndex)
            {
                case 1:
                case 2:
                case 3:
                    try
                    {
                        Light = FileIO.ReadVector<double>(openFile.FileName);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    break;
            }
        }

        /// <summary>
        /// (Re-)graph most current data.
        /// Used when ShowLast/ShowDiff is changed or calibration is updated
        /// while the background task is stopped.
        /// Also if in "alignment mode" (PeristentGraphing NOT checked) while
        /// the background task is running.
        /// </summary>
        private void Display()
        {

            Graph.ClearData();
            
            if (ShowLast.Checked && LastLight != null)
            {
                Graph.MarkerColor = Graph.ColorOrder[1];
                Graph.DrawPoints(Commander.Camera.Calibration, LastLight);
            }

            if (ShowDifference.Checked && DiffLight != null)
            {
                Graph.MarkerColor = Graph.ColorOrder[2];
                Graph.DrawPoints(Commander.Camera.Calibration, DiffLight);
            }

            if (Light != null)
            {
                Graph.MarkerColor = Graph.ColorOrder[0];
                Graph.DrawPoints(Commander.Camera.Calibration, Light);
            }

            if (CumulativeLight != null) // Always false while background task running.
            {
                Graph.MarkerColor = Graph.ColorOrder[3];
                Graph.DrawPoints(Commander.Camera.Calibration, CumulativeLight);
            }

            Graph.Invalidate();
        }

        /// <summary>
        /// Used to update graph as background task runs.
        /// If PersistentGraphing isn't checked, forwards to Display().
        /// Otherwise, we re-graph the new DiffLight and Light.
        /// DiffLight should rarely appear on top of other curves.
        /// </summary>
        private void DisplayProgress()
        {
            if (PersistentGraphing.Checked)
            {
                if (ShowDifference.Checked && DiffLight != null)
                {
                    Graph.MarkerColor = Graph.ColorOrder[2];
                    Graph.DrawPoints(Commander.Camera.Calibration, DiffLight);
                }

                if (Light != null)
                {
                    Graph.MarkerColor = Graph.ColorOrder[0];
                    Graph.DrawPoints(Commander.Camera.Calibration, Light);
                }
                Graph.Invalidate();
            }
            else
            {
                Display();
            }
        }

        /// <summary>
        /// Updates graph after background task complete.
        /// Reduces to a no-op if PersistentGraphing is NOT checked.
        /// Note in this case, the final scan has already been displayed.
        /// </summary>
        private void DisplayComplete()
        {
            // No point if PersistentGraphing isn't checked, as we would only
            // see the final scan with the average. Graphing the average is
            // most useful if it's graphed on top of all previous scans.
            if (CumulativeLight != null && PersistentGraphing.Checked)
            {
                Graph.MarkerColor = Graph.ColorOrder[3];
                Graph.DrawPoints(Commander.Camera.Calibration, CumulativeLight);
            }
            Graph.Invalidate();
        }

        private void ShowLast_CheckedChanged(object sender, EventArgs e)
        {
            Display();
        }

        private void ShowDifference_CheckedChanged(object sender, EventArgs e)
        {
            Display();
        }

        //private void NAverage_ValueChanged(object sender, EventArgs e)
        //{
        //    PeakNLabel.Text = NAverage.Value.ToString("n") + " Point Average";
        //}

        private void SaveProfile_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "ALN File|*.aln|MAT File|*.mat|All Files|*.*";
            saveFile.Title = "Save Light Profile";
            saveFile.ShowDialog();

            if (saveFile.FileName == "") return;

            switch (saveFile.FilterIndex)
            {
                case 3:
                // All files, fall through to ALN.
                case 1:
                    // ALN
                    try
                    {
                        FileIO.WriteVector<double>(saveFile.FileName, Light);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    break;
                case 2:
                    // MAT
                    try
                    {
                        MatFile mat = new MatFile(saveFile.FileName);
                        MatVar<double> V = mat.CreateVariable<double>("aln", Light.Length, 1);
                        V.WriteNext(Light,1);
                        mat.Dispose();
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    break;
            }
        }

        void CollectLaser_CheckedChanged(object sender, EventArgs e)
        {
            if (CollectLaser.Checked)
            {
                if (Commander.Camera.HasIntensifier) CameraGain.Value = Commander.Camera.MinIntensifierGain;
                string Msg = "Warning: camera will be exposed to scattered laser light!\r\n" +
                (Commander.Camera.HasIntensifier ? "Gain has been set to minimum as a precaution.\r\n" : "") +
                         "Remember to configure DDG and set delay manually before proceeding.";
                MessageBox.Show(Msg, "Warning!", MessageBoxButtons.OK);
            }
            PersistentGraphing.Checked = !CollectLaser.Checked;
            DdgConfigBox.Enabled = CollectLaser.Checked;
        }

    }
}

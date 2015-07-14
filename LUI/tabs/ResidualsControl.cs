using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
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
        MatVar<int> RawData;
        MatFile DataFile;
        CancellationTokenSource TemperatureCts = null;
        private double[] Light = null;
        private double[] LastLight = null;
        private double[] CumulativeLight = null;

        int LastAcqWidth;
        int LastVBin;

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
                _SelectedChannel = Math.Max(Math.Min(value, Commander.Camera.Width - 1), 0);
            }
        }

        int LowerBound { get; set; }
        int UpperBound { get; set; }

        int SelectedRow { get; set; }

        public enum Dialog
        {
            INIT, PROGRESS_DATA
        }

        struct WorkArgs
        {
            public WorkArgs(int NScans, int NAverage, bool CollectLaser, bool SoftwareBinning)
            {
                this.NScans = NScans;
                this.NAvg = NAverage;
                this.CollectLaser = CollectLaser;
                this.SoftwareBinning = SoftwareBinning;
            }
            public readonly int NScans;
            public readonly int NAvg;
            public readonly bool CollectLaser;
            public readonly bool SoftwareBinning;
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

            SaveData.Click += SaveData_Click;
            SaveData.Enabled = false;

            CollectLaser.CheckedChanged += CollectLaser_CheckedChanged;
            ImageMode.CheckedChanged += ImageMode_CheckedChanged;
            FvbMode.CheckedChanged += FvbMode_CheckedChanged;
            SoftFvbMode.CheckedChanged += SoftFvbMode_CheckedChanged;

            GraphScroll.Scroll += GraphScroll_Scroll;
            GraphScroll.ValueChanged += GraphScroll_ValueChanged;
            GraphScroll.Enabled = false;
            GraphScroll.LargeChange = 1;
            SelectedRow = 0;

            CameraTemperature.Enabled = false;

            VBin.Minimum = 1;
            VBin.Value = 1;
            VBin.ValueChanged += CameraImage_ValueChanged;
            VStart.Minimum = 1;
            VStart.ValueChanged += CameraImage_ValueChanged;
            VEnd.Minimum = 1;
            VEnd.ValueChanged += CameraImage_ValueChanged;

            DdgConfigBox.Config = Config;
            DdgConfigBox.Commander = Commander;
            DdgConfigBox.AllowZero = true;
            DdgConfigBox.Enabled = false;
            DdgConfigBox.HandleParametersChanged(this, EventArgs.Empty);
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (DataFile != null)
                {
                    var FileName = DataFile.FileName;
                    DataFile.Dispose();
                    if (File.Exists(FileName)) File.Delete(FileName);
                }
                if (components != null) components.Dispose();
            }
            base.Dispose(disposing);
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
            LowerBound = Commander.Camera.Image.Width / 6;
            UpperBound = Commander.Camera.Image.Width * 5 / 6;
            UpdateReadMode();
            UpdateCameraImage();
            if (Commander.Camera is CameraTempControlled)
            {
                CameraTemperature.Enabled = true;
                var camct = (CameraTempControlled)Commander.Camera;
                CameraTemperature.Minimum = camct.MinTemp;
                CameraTemperature.Maximum = camct.MaxTemp;
                CameraTemperature.Increment = (int)CameraTempControlled.TemperatureEps;
                UpdateCameraTemperature(); // Subscribes ValueChanged.
            }
            else
            {
                CameraTemperature.Enabled = false;
                CameraTemperature.ValueChanged -= CameraTemperature_ValueChanged;
            }
        }

        public override void HandleContainingTabSelected(object sender, EventArgs e)
        {
            base.HandleContainingTabSelected(sender, e);
            DdgConfigBox.UpdatePrimaryDelayValue();
            UpdateReadMode();
            UpdateCameraTemperature();
            UpdateCameraImage();
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
            if (CollectLaser.Checked && 
                (DdgConfigBox.PrimaryDelayDdg == null || DdgConfigBox.PrimaryDelayDelay == null))
            {
                MessageBox.Show("Primary delay must be configured.", "Error", MessageBoxButtons.OK);
                return;
            }

            CameraStatus.Text = "";

            Graph.ClearData();
            CumulativeLight = null;

            Commander.BeamFlag.CloseLaserAndFlash();

            if (CollectLaser.Checked)
                DdgConfigBox.ApplyPrimaryDelayValue();

            LastAcqWidth = Commander.Camera.AcqWidth;
            LastVBin = Commander.Camera.Image.vbin;

            GraphScroll.Enabled = ImageMode.Checked;
            UpdateGraphScroll();
            
            CameraImage_ValueChanged(sender, e);
            ImageMode_CheckedChanged(sender, e);
            FvbMode_CheckedChanged(sender, e);
            SoftFvbMode_CheckedChanged(sender, e);

            SetupWorker();
            worker.RunWorkerAsync(new WorkArgs((int)NScan.Value, (int)NAverage.Value, CollectLaser.Checked, SoftFvbMode.Checked));
            OnTaskStarted(EventArgs.Empty);
        }

        public override void OnTaskStarted(EventArgs e)
        {
            base.OnTaskStarted(e);
            DdgConfigBox.Enabled = false;
            OptionsBox.Enabled = CameraExtras.Enabled = false;
            LoadProfile.Enabled = SaveProfile.Enabled = SaveData.Enabled = false;
        }

        public override void OnTaskFinished(EventArgs e)
        {
            base.OnTaskFinished(e);
            DdgConfigBox.Enabled = CollectLaser.Checked;
            OptionsBox.Enabled = CameraExtras.Enabled = true;
            LoadProfile.Enabled = SaveProfile.Enabled = SaveData.Enabled = true;
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
            if (PauseCancelProgress(e, -1, 
                new ProgressObject(null, 0, 0, 0, 0, 0, 0 ,0, 0, Dialog.INIT))) return;
            
            WorkArgs args = (WorkArgs)e.Argument;
            
            int cmasum = 0; // Cumulative moving average over scans
            double varsum = 0;
            int cmapeak = 0;
            double varpeak = 0;
            int nsum = 0; // CMA over last NAvg scans only
            double nvarsum = 0;
            int npeak = 0;
            double nvarpeak = 0;
            int[] pastsums = new int[args.NAvg];
            int[] pastpeaks = new int[args.NAvg];

            int finalSize = args.SoftwareBinning ?
                Commander.Camera.AcqSize / Commander.Camera.Image.Height : 
                Commander.Camera.AcqSize;
            double nrows = (double)Commander.Camera.AcqSize / finalSize;
            int[] DataBuffer = new int[Commander.Camera.AcqSize];
            int[] BinnedDataBuffer = new int[finalSize];
            int[] CumulativeDataBuffer = new int[finalSize];

            InitDataFile(finalSize, args.NScans);

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
                TryAcquire(DataBuffer);
                RawData.WriteNext(DataBuffer, 0);

                int sum = 0;
                int peak = int.MinValue;
                for (int k = 0; k < Commander.Camera.AcqSize / Commander.Camera.AcqWidth; k++)
                {
                    for (int j = LowerBound; j <= UpperBound; j++)
                    {
                        int idx = k * Commander.Camera.Image.Width + j;
                        sum += DataBuffer[idx];
                        if (DataBuffer[idx] > peak) peak = DataBuffer[idx];
                    }
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

                int n = i % args.NAvg;
                pastsums[n] = sum;
                pastpeaks[n] = peak;

                nvarpeak = nvarsum = npeak = nsum = 0;
                int localN = i < args.NAvg ? n : args.NAvg;
                for (int j = 0; j < localN; j++)
                {
                    //nsum = (sum + n * nsum) / (n + 1);
                    //npeak = (peak + n * npeak) / (n + 1);
                    delta = pastsums[j] - nsum;
                    nsum += delta / (j + 1);
                    vartemp = delta * (pastsums[j] - nsum);
                    nvarsum += Math.Sqrt(Math.Abs(vartemp));

                    delta = pastpeaks[j] - npeak;
                    npeak += delta / (j + 1);
                    vartemp = delta * (pastpeaks[j] - npeak);
                    nvarpeak += Math.Sqrt(Math.Abs(vartemp));
                }

                Array.Clear(BinnedDataBuffer, 0, BinnedDataBuffer.Length);
                Data.ColumnSum(BinnedDataBuffer, DataBuffer);
                Data.Accumulate(CumulativeDataBuffer, BinnedDataBuffer);

                var progress = new ProgressObject(
                    Array.ConvertAll((int[])BinnedDataBuffer, x => (double)x / nrows), 
                    cmasum, varsum / i, cmapeak, varpeak / i, nsum, nvarsum / i, 
                    npeak, nvarpeak / i, Dialog.PROGRESS_DATA);
                if (PauseCancelProgress(e, i+1, progress)) return;
            }

            Commander.BeamFlag.CloseLaserAndFlash();

            double[] CumulativeData = Array.ConvertAll((int[])CumulativeDataBuffer, x => (double)x / (args.NScans * nrows));
            e.Result = CumulativeData;
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
                case Dialog.INIT:
                    ProgressLabel.Text = "Initializing";
                    break;
                case Dialog.PROGRESS_DATA:
                    Light = (double[])Progress.Data;
                    if (LastLight != null)
                    {
                        DiffLight = (double[])Light.Clone(); // Deep copy for value types only.
                        Data.Dissipate(DiffLight, LastLight);
                    }

                    DisplayProgress();

                    Peak.Text = Progress.Peak.ToString("n0") + " \u00B1 " + Progress.VarPeak.ToString("n3");
                    Counts.Text = Progress.Counts.ToString("n0") + " \u00B1 " + Progress.VarCounts.ToString("n0");
                    PeakN.Text = Progress.PeakN.ToString("n0") + " \u00B1 " + Progress.VarPeakN.ToString("n3");
                    CountsN.Text = Progress.CountsN.ToString("n0") + " \u00B1 " + Progress.VarCountsN.ToString("n0");

                    ScanProgress.Text = e.ProgressPercentage.ToString() + "/" + NScan.Value.ToString();
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

            if (DataFile != null) DataFile.Close();

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
                        LastLight = FileIO.ReadVector<double>(openFile.FileName);
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
            int start = LastAcqWidth * SelectedRow;
            int count = LastAcqWidth;

            Graph.ClearData();
            
            if (ShowLast.Checked && LastLight != null)
            {
                Graph.MarkerColor = Graph.ColorOrder[1];
                Graph.DrawPoints(Commander.Camera.Calibration, new ArraySegment<double>(LastLight, start, count));
            }

            if (ShowDifference.Checked && DiffLight != null)
            {
                Graph.MarkerColor = Graph.ColorOrder[2];
                Graph.DrawPoints(Commander.Camera.Calibration, new ArraySegment<double>(DiffLight, start, count));
            }

            if (Light != null)
            {
                Graph.MarkerColor = Graph.ColorOrder[0];
                Graph.DrawPoints(Commander.Camera.Calibration, new ArraySegment<double>(Light, start, count));
            }

            if (CumulativeLight != null && PersistentGraphing.Checked) // Always false while background task running.
            {
                Graph.MarkerColor = Graph.ColorOrder[3];
                Graph.DrawPoints(Commander.Camera.Calibration, new ArraySegment<double>(CumulativeLight, start, count));
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
            int start = LastAcqWidth * SelectedRow;
            int count = LastAcqWidth;
            if (PersistentGraphing.Checked)
            {
                if (ShowDifference.Checked && DiffLight != null)
                {
                    Graph.MarkerColor = Graph.ColorOrder[2];
                    Graph.DrawPoints(Commander.Camera.Calibration, new ArraySegment<double>(DiffLight, start, count));
                }

                if (Light != null)
                {
                    Graph.MarkerColor = Graph.ColorOrder[0];
                    Graph.DrawPoints(Commander.Camera.Calibration, new ArraySegment<double>(Light, start, count));
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
                int start = LastAcqWidth * SelectedRow;
                int count = LastAcqWidth;
                Graph.MarkerColor = Graph.ColorOrder[3];
                Graph.DrawPoints(Commander.Camera.Calibration, new ArraySegment<double>(CumulativeLight, start, count));
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
            saveFile.Title = "Save As";
            
            var result = saveFile.ShowDialog();

            if (result != DialogResult.OK || saveFile.FileName == "") return;

            if (File.Exists(saveFile.FileName)) File.Delete(saveFile.FileName);

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

        void GraphScroll_ValueChanged(object sender, EventArgs e)
        {
            UpdateSelectedRow();
            if (worker == null || !worker.IsBusy) Display();
        }

        void UpdateSelectedRow()
        {
            SelectedRow = (int)(GraphScroll.Value - GraphScroll.Minimum - 0.5) / LastVBin;
        }

        void GraphScroll_Scroll(object sender, ScrollEventArgs e)
        {
            ScrollTip.SetToolTip(GraphScroll, SelectedRow.ToString() + " (" + GraphScroll.Value.ToString() + ")");
            //ScrollTip.Show(GraphScroll.Value.ToString(), GraphScroll, 10000);
        }

        async void CameraTemperature_ValueChanged(object sender, EventArgs e)
        {
            var camct = Commander.Camera as CameraTempControlled;
            if (camct != null)
            {
                if (TemperatureCts != null) TemperatureCts.Cancel();
                TemperatureCts = new CancellationTokenSource();

                CameraTemperature.ForeColor = Color.Red;
                await camct.EquilibrateTemperatureAsync((int)CameraTemperature.Value, TemperatureCts.Token); // Wait for 3 deg. threshold.
                CameraTemperature.ForeColor = Color.Goldenrod;
                await camct.EquilibrateTemperatureAsync(TemperatureCts.Token); // Wait for driver signal.
                UpdateCameraTemperature();
                CameraTemperature.ForeColor = Color.Black;

                TemperatureCts = null;
            }
        }

        private void UpdateCameraTemperature()
        {
            var camct = Commander.Camera as CameraTempControlled;
            if (camct != null)
            {
                CameraTemperature.ValueChanged -= CameraTemperature_ValueChanged;
                CameraTemperature.Value = camct.Temperature;
                CameraTemperature.ValueChanged += CameraTemperature_ValueChanged;
            }
        }

        private void UpdateCameraImage()
        {
            VStart.ValueChanged -= CameraImage_ValueChanged;
            VBin.ValueChanged -= CameraImage_ValueChanged;
            VEnd.ValueChanged -= CameraImage_ValueChanged;

            VStart.Minimum = 0;
            VStart.Maximum = Commander.Camera.Height - 1;
            VStart.Value = Commander.Camera.Image.vstart;
            VEnd.Minimum = 0;
            VEnd.Maximum = Commander.Camera.Height - 1;
            VEnd.Value = Commander.Camera.Image.vstart + Commander.Camera.Image.vcount - 1;
            VBin.Minimum = 1;
            VBin.Maximum = Commander.Camera.Image.Height;
            VBin.Value = Commander.Camera.Image.vbin;

            VStart.ValueChanged += CameraImage_ValueChanged;
            VBin.ValueChanged += CameraImage_ValueChanged;
            VEnd.ValueChanged += CameraImage_ValueChanged;
        }

        private void UpdateGraphScroll()
        {
            if (GraphScroll.Enabled)
            {
                GraphScroll.ValueChanged -= GraphScroll_ValueChanged;
                GraphScroll.Minimum = Commander.Camera.Image.vstart;
                GraphScroll.Maximum = (Commander.Camera.Image.vstart + Commander.Camera.Image.vcount - 1);
                GraphScroll.LargeChange = Commander.Camera.Image.vbin;
                GraphScroll.Value = GraphScroll.Value > GraphScroll.Maximum ||
                    GraphScroll.Value <= GraphScroll.Minimum
                    ?
                    GraphScroll.Minimum + (GraphScroll.Maximum - GraphScroll.Minimum) / 2
                    :
                    GraphScroll.Value;
                UpdateSelectedRow();
                GraphScroll.ValueChanged += GraphScroll_ValueChanged;
            }
            else
            {
                SelectedRow = 0;
            }
        }

        private void UpdateReadMode()
        {
            if (Commander.Camera.ReadMode == AndorCamera.ReadModeImage)
            {
                ImageMode.Checked = true;
            }
            else
            {
                FvbMode.Checked = true;
            }
        }

        private void CameraImage_ValueChanged(object sender, EventArgs e)
        {
            Commander.Camera.Image = new ImageArea(Commander.Camera.Image.hbin, (int)VBin.Value,
                Commander.Camera.Image.hstart, Commander.Camera.Image.hcount,
                (int)VStart.Value, (int)(VEnd.Value - VStart.Value + 1));
            UpdateCameraImage();
        }

        void SoftFvbMode_CheckedChanged(object sender, EventArgs e)
        {
            if (SoftFvbMode.Checked)
                Commander.Camera.ReadMode = AndorCamera.ReadModeImage;
        }

        void FvbMode_CheckedChanged(object sender, EventArgs e)
        {
            if (FvbMode.Checked)
            {
                Commander.Camera.ReadMode = AndorCamera.ReadModeFVB;
            }
        }

        void ImageMode_CheckedChanged(object sender, EventArgs e)
        {
            if (ImageMode.Checked)
                Commander.Camera.ReadMode = AndorCamera.ReadModeImage;
        }

        /// <summary>
        /// Create temporary MAT file and initialize variables.
        /// </summary>
        /// <param name="NumChannels"></param>
        /// <param name="NumScans"></param>
        /// <param name="NumTimes"></param>
        void InitDataFile(int NumChannels, int NumScans)
        {
            string TempFileName;
            if (DataFile != null)
            {
                if (!DataFile.Closed) DataFile.Close();
                File.Delete(DataFile.FileName);
                TempFileName = DataFile.FileName;
            }
            else
            {
                TempFileName = Path.GetTempFileName();
            }
            DataFile = new MatFile(TempFileName);
            RawData = DataFile.CreateVariable<int>("rawdata", NumScans, NumChannels);
        }

        private void SaveData_Click(object sender, EventArgs e)
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
                case 2: // CSV file; copy data to CSV file.
                    if (DataFile != null)
                    {
                        if (DataFile.Closed) DataFile.Reopen();

                        if (!RawData.Closed)
                        {
                            int[,] Matrix = new int[RawData.Dims[0], RawData.Dims[1]];
                            RawData.Read(Matrix, new long[] { 0, 0 }, RawData.Dims);
                            Matrix = Data.Transpose(Matrix);
                            FileIO.WriteMatrix(saveFile.FileName, Matrix);
                        }

                        DataFile.Close();
                    }
                    break;
            }
        }
    }
}

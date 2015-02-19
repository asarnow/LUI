using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Threading;
using lasercom;
using lasercom.io;

namespace LUI.controls
{
    public partial class ResidualsControl : LUIControl
    {
        private BackgroundWorker ioWorker;
        private Dispatcher Dispatcher;

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
            public WorkArgs(int NScans, int NAverage)
            {
                this.NScans = NScans;
                this.NAvg = NAverage;
            }
            public readonly int NScans;
            public readonly int NAvg;
        }

        struct WorkProgress
        {
            public WorkProgress(object Data, int Counts, int Peak, int CountsN, int PeakN, Dialog Status)
            {
                this.Data = Data;
                this.Counts = Counts;
                this.Peak = Peak;
                this.CountsN = CountsN;
                this.PeakN = PeakN;
                this.Status = Status;
            }                             
            public readonly object Data;  
            public readonly int Counts;      
            public readonly int Peak;     
            public readonly int CountsN;     
            public readonly int PeakN;    
            public readonly Dialog Status;
        }

        public ResidualsControl(Commander commander)
        {
            InitializeComponent();
            Commander = commander;
            Graph.MouseClick += new MouseEventHandler(Graph_Click);
            Graph.XMin = (float)Commander.Calibration[0];
            Graph.XMax = (float)Commander.Calibration[Commander.Calibration.Length - 1];
            LowerBound = (int)Commander.Camera.Width / 6;
            UpperBound = (int)Commander.Camera.Width * 5 / 6;

            CameraGain.Minimum = Commander.Camera.MinMCPGain;
            CameraGain.Maximum = Commander.Camera.MaxMCPGain;
            CameraGain.Value = Commander.Camera.MCPGain;
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
        public void AlignmentWork(object sender, DoWorkEventArgs e)
        {
            Commander.Camera.AcquisitionMode = AndorCamera.AcquisitionModeSingle;
            Commander.Camera.TriggerMode = AndorCamera.TriggerModeExternalExposure;
            Commander.Camera.ReadMode = AndorCamera.ReadModeFVB;
            //TODO Need local sample size and no. scans
            WorkArgs args = (WorkArgs)e.Argument;

            int cmasum = 0; // Cumulative moving average over scans
            int cmapeak = 0;
            int nsum = 0; // CMA over last NAvg scans only
            int npeak = 0;
            int[] DataBuffer = new int[Commander.Camera.AcqSize];
            int[] CumulativeDataBuffer = new int[DataBuffer.Length];
            for (int i = 0; i < args.NScans; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                uint ret = Commander.Flash(DataBuffer); //TODO If flag, dump it in a MAT to be renamed later
                Data.Accumulate(CumulativeDataBuffer, DataBuffer);

                int sum = 0;
                int peak = int.MinValue;
                for (int j = LowerBound; j <= UpperBound; j++)
                {
                    sum += DataBuffer[j];
                    if (DataBuffer[j] > peak) peak = DataBuffer[j];
                }

                cmasum = (sum + i * cmasum) / (i + 1);
                cmapeak = (peak + i * cmapeak) / (i + 1);

                int n = i % args.NAvg; // Reset NAvg CMA
                if (n == 0) npeak = nsum = 0;
                nsum = (nsum + n * nsum) / (n + 1);
                npeak = (npeak + n * npeak) / (n + 1);

                WorkProgress progress = new WorkProgress(Array.ConvertAll((int[])DataBuffer, x => (double)x), cmasum, cmapeak, nsum, npeak, Dialog.PROGRESS_DATA);
                worker.ReportProgress(i / args.NScans, progress);
            }
            Data.DivideArray(CumulativeDataBuffer, args.NScans);
            e.Result = Array.ConvertAll((int[])CumulativeDataBuffer, x=> (double)x);
        }

        private void Collect_Click(object sender, EventArgs e)
        {
            Collect.Enabled = false;
            worker = new BackgroundWorker();
            worker.DoWork += new System.ComponentModel.DoWorkEventHandler(AlignmentWork);
            worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(AlignmentProgress);
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync(new WorkArgs((int)NScan.Value, (int)NAverage.Value));
            Graph.ClearData();
            CumulativeLight = null;
        }

        /// <summary>
        /// Runs in UI thread to report background task progress.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AlignmentProgress(object sender, ProgressChangedEventArgs e)
        {
            WorkProgress Progress = (WorkProgress)e.UserState;

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

                    Peak.Text = Progress.Peak.ToString("n");
                    Counts.Text = Progress.Counts.ToString("n");
                    PeakN.Text = Progress.PeakN.ToString("n");
                    CountsN.Text = Progress.CountsN.ToString("n");

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
        public void AlignmentComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                // Handle the exception thrown in the worker thread
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
            StatusProgress.Value = 100;
            Collect.Enabled = true;
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            Graph.Clear();
            Graph.Invalidate();
        }

        private void Graph_Click(object sender, MouseEventArgs e)
        {
            SelectedChannel = (int)Math.Round(Graph.AxesToNormalized(Graph.ScreenToAxes(new Point(e.X, e.Y))).X * (Commander.Camera.Width - 1));

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
            Graph.Annotate(GraphControl.Annotation.VERTLINE, Graph.ColorOrder[0], LowerBound);
            Graph.Annotate(GraphControl.Annotation.VERTLINE, Graph.ColorOrder[0], UpperBound);
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
                    try
                    {
                        Light = FileIO.ReadVector<double>(openFile.FileName);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    break;
                case 2:
                    try
                    {
                        Light = FileIO.ReadVector<double>(openFile.FileName);
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    break;
                case 3:
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
                Graph.DrawPoints(Commander.Calibration, LastLight);
            }

            if (ShowDifference.Checked && DiffLight != null)
            {
                Graph.MarkerColor = Graph.ColorOrder[2];
                Graph.DrawPoints(Commander.Calibration, DiffLight);
            }

            if (Light != null)
            {
                Graph.MarkerColor = Graph.ColorOrder[0];
                Graph.DrawPoints(Commander.Calibration, Light);
            }

            if (CumulativeLight != null) // Always false while background task running.
            {
                Graph.MarkerColor = Graph.ColorOrder[3];
                Graph.DrawPoints(Commander.Calibration, CumulativeLight);
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
                    Graph.DrawPoints(Commander.Calibration, DiffLight);
                }

                if (Light != null)
                {
                    Graph.MarkerColor = Graph.ColorOrder[0];
                    Graph.DrawPoints(Commander.Calibration, Light);
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
                Graph.DrawPoints(Commander.Calibration, CumulativeLight);
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

        public void HandleCalibrationChanged(object sender, EventArgs args)
        {
            Graph.XMin = (float)Commander.Calibration[0];
            Graph.XMax = (float)Commander.Calibration[Commander.Calibration.Length - 1];
            Graph.Clear();
            Display();
        }

        private void NAverage_ValueChanged(object sender, EventArgs e)
        {
            PeakNLabel.Text = NAverage.Value.ToString("n") + " Point Average";
        }

        private void CameraGain_ValueChanged(object sender, EventArgs e)
        {
            //TODO Safety check
            Commander.Camera.MCPGain = (int)CameraGain.Value;
        }

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
                        MatFile mat = new MatFile(saveFile.FileName, "aln",
                            Light.Length, 1, "double");
                        mat.WriteColumn(Light);
                        mat.Dispose();
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    break;
            }
        }

        private void OpenLaser_Click(object sender, EventArgs e)
        {
            Commander.BeamFlags.OpenLaser();
        }

        private void CloseLaser_Click(object sender, EventArgs e)
        {
            Commander.BeamFlags.CloseLaser();
        }

        private void OpenLamp_Click(object sender, EventArgs e)
        {
            Commander.BeamFlags.OpenFlash();
        }

        private void CloseLamp_Click(object sender, EventArgs e)
        {
            Commander.BeamFlags.CloseFlash();
        }

    }
}

using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using lasercom;
using lasercom.camera;
using lasercom.control;
using lasercom.io;
using lasercom.objects;

namespace DetectorTester
{
    public partial class DetectorTestForm : Form
    {
        double[] Light;

        CancellationTokenSource TemperatureCts = null;
        CancellationTokenSource WorkCts = null;
        ManualResetEvent Paused;
        Task Work;

        private MatFile DataFile;
        MatVar<int> RawData;

        int LastAcqWidth;
        int LastVBin;
        int SelectedRow;
        ImageArea WorkImage;

        ICamera Camera;
        IBeamFlags Flags;

        private uint _LastStatus;
        uint LastStatus { 
            get
            {
                return _LastStatus; 
            }
            set
            {
                _LastStatus = value;
                BeginInvoke(new Action(() =>
                {
                    CameraStatus.Text = Camera.DecodeStatus(_LastStatus);
                }));
            }
        }

        struct WorkParameters
        {
            public string ReadMethod { get; set; }
            public int NScans { get; set; }
            public bool Excite { get; set; }
        }

        struct ProgressObject
        {
            public int CurrentScan;
            public int[] Data;
        }

        public DetectorTestForm()
        {
            Paused = new ManualResetEvent(true);

            var bfp = new BeamFlagsParameters(typeof(BeamFlags));
            bfp.Name = "BF";
            bfp.PortName = "COM1";
            bfp.Delay = 300; // ms.
            Flags = new BeamFlags(bfp);
            //Flags = new DummyBeamFlags(bfp);

            var cp = new CameraParameters(typeof(CameraTempControlled));
            cp.Name = "Camera";
            cp.Dir = ".";
            cp.InitialGain = 10;
            cp.Temperature = 20;
            cp.ReadMode = AndorCamera.ReadModeFVB;
            Camera = new CameraTempControlled(cp);
            //Camera = new DummyAndorCamera(cp);

            Camera.AcquisitionMode = Constants.AcquisitionModeSingle;
            Camera.TriggerMode = Constants.TriggerModeExternalExposure;
            Camera.DDGTriggerMode = Constants.DDGTriggerModeExternal;

            InitializeComponent();
            Init();
        }

        private void Init()
        {
            CameraGain.Minimum = 0;
            CameraGain.Maximum = Camera.MaxIntensifierGain;
            CameraGain.Value = AndorCamera.DefaultMCPGain;

            CameraTemp.Minimum = CameraAs<CameraTempControlled>().MinTemp;
            CameraTemp.Maximum = CameraAs<CameraTempControlled>().MaxTemp;
            UpdateCameraTemperature(); // Subscribes ValueChanged.

            fvbButton.Checked = true;

            GraphScroll.Minimum = 0;
            VBin.Minimum = 1;
            FirstRow.Value = FirstRow.Minimum = 0;
            LastRow.Value = LastRow.Maximum = VBin.Maximum = GraphScroll.Maximum = Camera.Height - 1;
            GraphScroll.LargeChange = 1;
            GraphScroll.Enabled = false;
            SelectedRow = 0;

            GraphScroll.ValueChanged += GraphScroll_ValueChanged;
            GraphScroll.Scroll += GraphScroll_Scroll;
            VBin.ValueChanged += CameraImage_ValueChanged;
            FirstRow.ValueChanged += CameraImage_ValueChanged;
            LastRow.ValueChanged += CameraImage_ValueChanged;

            Graph.YLabelFormat = "g";
            Graph.XLeft = (float)Math.Min(Camera.Calibration[0], 
                Camera.Calibration[Camera.Calibration.Length - 1]);
            Graph.XRight = (float)Math.Max(Camera.Calibration[0], 
                Camera.Calibration[Camera.Calibration.Length - 1]);

            Graph.YMax = 1;
            Graph.YMin = 0;

            Graph.Clear();
            Graph.Invalidate();

            Graph.Click += Graph_Click;

            AcqMethods.SelectedIndex = 0;
            
            saveAsToolStripMenuItem.Enabled = false;
        }

        private P CameraAs<P>() where P : class
        {
            return Camera as P;
        }

        protected bool WillPause()
        {
            return !Paused.WaitOne(0);
        }

        protected bool WaitForResume()
        {
            return !Paused.WaitOne(Timeout.Infinite);
        }

        private void ShowImage(int[] image)
        {
            Data.NormalizeArray(image, 255);
            Bitmap picture = new Bitmap(1024, 256);
            for (int x = 0; x < picture.Width; x++)
            {
                for (int y = 0; y < picture.Height; y++)
                {
                    Color c = Color.FromArgb(image[y * picture.Height + x], image[y * picture.Height + x], image[y * picture.Height + x]);
                    picture.SetPixel(x, y, c);
                }
            }
            imageBox.Image = picture;
        }

        private void TaskStart()
        {
            CameraConfigBox.Enabled = false;
            specButton.Enabled = saveButton.Enabled = darkButton.Enabled = false;
            Abort.Enabled = Pause.Enabled = true;
            WorkImage = Camera.Image;
            GraphScroll.Enabled = imageAcqButton.Checked;
            UpdateGraphScroll();
            Graph.ClearData();
            Graph.MarkerColor = Graph.ColorOrder[0];
            Graph.Invalidate();
            WorkCts = new CancellationTokenSource();
            Paused.Set();
        }

        private void TaskFinish()
        {
            CameraConfigBox.Enabled = true;
            specButton.Enabled = saveButton.Enabled = darkButton.Enabled = true;
            Abort.Enabled = Pause.Enabled = false;
            saveAsToolStripMenuItem.Enabled = true;
        }

        private async void darkButton_Click(object sender, EventArgs e)
        {
            TaskStart();
            var progress = new Progress<ProgressObject>(ReportProgress);
            var args = new WorkParameters()
            {
                ReadMethod = (string)AcqMethods.SelectedItem,
                NScans = (int)NScans.Value
            };
            Work = DoWorkAsync(DoDark, args, progress, WorkCts.Token);
            await Work;
            TaskFinish();
        }

        private async void captureButton_Click(object sender, EventArgs e)
        {
            TaskStart();
            var progress = new Progress<ProgressObject>(ReportProgress);
            var args = new WorkParameters()
            {
                ReadMethod = (string)AcqMethods.SelectedItem,
                NScans = (int)NScans.Value,
                Excite = exciteCheck.Checked
            };
            Work = DoWorkAsync(DoCapture, args, progress, WorkCts.Token);
            await Work;
            TaskFinish();
        }

        private async Task DoWorkAsync(Action<WorkParameters, IProgress<ProgressObject>, CancellationToken> func, WorkParameters args, IProgress<ProgressObject> progress, CancellationToken cancel)
        {
            await Task.Run(() => func(args, progress, cancel));
        }

        private void DoCapture(WorkParameters args, IProgress<ProgressObject> progress, CancellationToken cancel)
        {
            InitDataFile(Camera.AcqSize, args.NScans);
            if (args.Excite)
                Flags.OpenLaserAndFlash();
            else
                Flags.OpenFlash();
            DoAcq(args, progress, cancel);
            Flags.CloseLaserAndFlash();
        }

        private void DoDark(WorkParameters args, IProgress<ProgressObject> progress, CancellationToken cancel)
        {
            InitDataFile(Camera.AcqSize, args.NScans);
            Flags.CloseLaserAndFlash();
            DoAcq(args, progress, cancel);
        }

        private void DoAcq(WorkParameters args, IProgress<ProgressObject> progress, CancellationToken cancel)
        {
            LastAcqWidth = Camera.AcqWidth;
            LastVBin = Camera.Image.vbin;
            int[] DataBuffer = new int[Camera.AcqSize];
            for (int i = 0; i < args.NScans; i++)
            {
                if (args.ReadMethod == "GetAcquiredData")
                    LastStatus = Camera.Acquire(DataBuffer);
                else
                    LastStatus = CameraAs<AndorCamera>().AcquireImage(DataBuffer);
                RawData.WriteNext(DataBuffer, 0);
                progress.Report(new ProgressObject()
                {
                    CurrentScan = i, Data = (int[])DataBuffer.Clone()
                });
                DoPause();
                if (cancel.IsCancellationRequested) return;
            }
        }

        private void DoPause()
        {
            if (WillPause())
            {
                // Going to pause.
                var OldFlashState = Flags.FlashState;
                var OldLaserState = Flags.LaserState;
                Flags.CloseLaserAndFlash();
                WaitForResume();
                if (OldFlashState == BeamFlagState.Open && OldLaserState == BeamFlagState.Open)
                    Flags.OpenLaserAndFlash();
                else if (OldFlashState == BeamFlagState.Open)
                    Flags.OpenFlash();
                else if (OldLaserState == BeamFlagState.Open)
                    Flags.OpenLaser();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (DataFile != null)
            {
                DataFile.Dispose();
                File.Delete(DataFile.FileName);
            }
            ((ILuiObject)Flags).Dispose();
            ((ILuiObject)Camera).Dispose();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            Graph.Clear();
            Graph.Invalidate();
        }

        /// <summary>
        /// Create temporary MAT file and initialize variables.
        /// </summary>
        /// <param name="NumChannels"></param>
        /// <param name="NumScans"></param>
        /// <param name="NumTimes"></param>
        private void InitDataFile(int NumChannels, int NumScans)
        {
            string TempFileName;
            if (DataFile != null)
            {
                DataFile.Dispose();
                TempFileName = DataFile.FileName;
            }
            else
            {
                TempFileName = Path.GetTempFileName();
            }
            DataFile = new MatFile(TempFileName);
            RawData = DataFile.CreateVariable<int>("rawdata", NumScans, NumChannels);
        }

        private void ReportProgress(ProgressObject progress)
        {
            ScanProgress.Text =
                (progress.CurrentScan + 1).ToString() + "/" + NScans.Value.ToString();
            if (progress.Data != null)
            {
                int start = LastAcqWidth * SelectedRow;
                int count = LastAcqWidth;
                Light = progress.Data.Select((x) => (double)x).ToArray();
                Graph.DrawPoints(Camera.Calibration, new ArraySegment<double>(Light, start, count));
                Graph.MarkerColor = Graph.NextColor;
                Graph.Invalidate();
            }
        }

        private void fvbButton_CheckedChanged(object sender, EventArgs e)
        {
            if (fvbButton.Checked)
            {
                Camera.ReadMode = AndorCamera.ReadModeFVB;
            }
        }

        private void imageAcqButton_CheckedChanged(object sender, EventArgs e)
        {
            if (imageAcqButton.Checked)
            {
                Camera.ReadMode = AndorCamera.ReadModeImage;
            }
        }

        private void CameraGain_ValueChanged(object sender, EventArgs e)
        {
            Camera.IntensifierGain = (int)CameraGain.Value;
        }

        private void CameraImage_ValueChanged(object sender, EventArgs e)
        {
            Camera.Image = new ImageArea(Camera.Image.hbin, (int)VBin.Value,
                Camera.Image.hstart, Camera.Image.hcount,
                (int)FirstRow.Value, (int)(LastRow.Value - FirstRow.Value + 1));
            UpdateCameraImage();
        }

        private void UpdateCameraImage()
        {
            FirstRow.ValueChanged -= CameraImage_ValueChanged;
            VBin.ValueChanged -= CameraImage_ValueChanged;
            LastRow.ValueChanged -= CameraImage_ValueChanged;

            FirstRow.Value = LastRow.Minimum = Camera.Image.vstart;
            LastRow.Value = FirstRow.Maximum = Camera.Image.vend;
            VBin.Maximum = Camera.Image.vcount;
            VBin.Value = Camera.Image.vbin;

            FirstRow.ValueChanged += CameraImage_ValueChanged;
            VBin.ValueChanged += CameraImage_ValueChanged;
            LastRow.ValueChanged += CameraImage_ValueChanged;
        }

        private async void CameraTemp_ValueChanged(object sender, EventArgs e)
        {
            var camct = CameraAs<CameraTempControlled>();
            if (camct != null)
            {
                if (TemperatureCts != null) TemperatureCts.Cancel();
                TemperatureCts = new CancellationTokenSource();

                CameraTemp.ForeColor = Color.Red;
                await camct.EquilibrateTemperatureAsync((int)CameraTemp.Value, TemperatureCts.Token); // Wait for 3 deg. threshold.
                CameraTemp.ForeColor = Color.DarkGoldenrod;
                await camct.EquilibrateTemperatureAsync(TemperatureCts.Token); // Wait for driver signal.
                UpdateCameraTemperature();
                CameraTemp.ForeColor = Color.Black;

                TemperatureCts = null;
            }
        }

        private void UpdateCameraTemperature()
        {
            var camct = CameraAs<CameraTempControlled>();
            if (camct != null)
            {
                CameraTemp.ValueChanged -= CameraTemp_ValueChanged;
                CameraTemp.Value = camct.Temperature;
                CameraTemp.ValueChanged += CameraTemp_ValueChanged;
            }
        }

        void UpdateSelectedRow()
        {
            SelectedRow = (int)(GraphScroll.Value - GraphScroll.Minimum - 0.5) / WorkImage.vbin;
        }

        private void GraphScroll_Scroll(object sender, ScrollEventArgs e)
        {
            ScrollTip.SetToolTip(GraphScroll, SelectedRow.ToString() + " (" + GraphScroll.Value.ToString() + ")");
        }

        void GraphScroll_ValueChanged(object sender, EventArgs e)
        {
            UpdateSelectedRow();
            if (Work == null || Work.Status != TaskStatus.Running)
            {
                int start = LastAcqWidth * SelectedRow;
                int count = LastAcqWidth;
                Graph.ClearData();
                Graph.MarkerColor = Graph.ColorOrder[0];
                Graph.DrawPoints(Camera.Calibration, new ArraySegment<double>(Light, start, count));
                Graph.Invalidate();
            }
        }

        private void Abort_Click(object sender, EventArgs e)
        {
            WorkCts.Cancel();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoSave();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            DoSave();
        }

        private void DoSave()
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

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Pause_Click(object sender, EventArgs e)
        {
            if (Paused.WaitOne(0)) // True if set (running/resumed).
            {
                Paused.Reset(); // Signal pause.
                Pause.Text = "Resume";
            }
            else
            {
                Paused.Set(); // Signal resume.
                Pause.Text = "Pause";
            }
        }

        private void Graph_Click(object sender, EventArgs e)
        {
            
        }

        private void UpdateGraphScroll()
        {
            if (GraphScroll.Enabled)
            {
                GraphScroll.ValueChanged -= GraphScroll_ValueChanged;
                GraphScroll.Minimum = Camera.Image.vstart;
                GraphScroll.Maximum = (Camera.Image.vstart + Camera.Image.vcount - 1);
                GraphScroll.LargeChange = Camera.Image.vbin;
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

        private void propertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var andor = CameraAs<AndorCamera>();
            var tempcontrol = CameraAs<CameraTempControlled>();
            var text = "Width: " + Camera.Width + ", " + "Height: " + Camera.Height + "\n" +
                "Min gain: " + Camera.MinIntensifierGain + ", " + "Max gain: " + Camera.MaxIntensifierGain + "\n";
            if (andor != null)
            {
                text += "Bit depth: " + andor.BitDepth + ", " + "AD channels: " + andor.NumberADChannels + "\n" +
                    "Max horizontal bin size: " + andor.MaxHorizontalBinSize + ", " + "Max vertical bin size: " + andor.MaxVerticalBinSize + "\n";
                if (tempcontrol != null)
                {
                    text += "Min temperature: " + tempcontrol.MinTemp + ", " + "Max temperature: " + tempcontrol.MaxTemp + "\n";
                }
            }
            
            MessageBox.Show(text, "Camera Detail");
        }

    }
}

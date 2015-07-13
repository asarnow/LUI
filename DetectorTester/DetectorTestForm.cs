using lasercom;
using lasercom.camera;
using lasercom.control;
using lasercom.io;
using lasercom.objects;
using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace DetectorTester
{
    public partial class DetectorTestForm : Form
    {   
        private int[] blank;
        private int[] dark;
        private int[] counts;
        private int[] image;

        CancellationTokenSource TemperatureCts = null;
        CancellationTokenSource WorkCts = null;

        private MatFile DataFile;
        MatVar<int> RawData;
        MatVar<double> LuiData;

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

        int SelectedRow;
        ImageArea WorkImage;

        struct WorkParameters
        {
            public int ReadMode { get; set; }
            public int NScans { get; set; }
            public bool Excite { get; set; }
        }

        public DetectorTestForm()
        {
            var bfp = new BeamFlagsParameters(typeof(BeamFlags));
            bfp.Name = "BF";
            bfp.PortName = "COM1";
            bfp.Delay = 300; // ms.
            Flags = new BeamFlags(bfp);

            var cp = new CameraParameters(typeof(CameraTempControlled));
            cp.Name = "Camera";
            cp.Dir = ".";
            cp.InitialGain = 10;
            cp.Temperature = 20;
            cp.ReadMode = AndorCamera.ReadModeFVB;
            Camera = new CameraTempControlled(cp);

            Camera.AcquisitionMode = Constants.AcquisitionModeSingle;
            Camera.TriggerMode = Constants.TriggerModeExternalExposure;
            Camera.DDGTriggerMode = Constants.DDGTriggerModeExternal;

            Init();
            InitializeComponent();
        }

        private void Init()
        {
            CameraGain.Minimum = 0;
            CameraGain.Maximum = Camera.MaxIntensifierGain;

            CameraTemp.Minimum = CameraAs<CameraTempControlled>().MinTemp;
            CameraTemp.Maximum = CameraAs<CameraTempControlled>().MaxTemp;

            GraphScroll.Minimum = 0;
            VBin.Minimum = 1;
            FirstRow.Value = FirstRow.Minimum = 0;
            LastRow.Value = LastRow.Maximum = VBin.Maximum = GraphScroll.Maximum = Camera.Height - 1;

            GraphScroll.ValueChanged += GraphScroll_ValueChanged;
            VBin.ValueChanged += CameraImage_ValueChanged;
            FirstRow.ValueChanged += CameraImage_ValueChanged;
            LastRow.ValueChanged += CameraImage_ValueChanged;
        }

        private P CameraAs<P>() where P : class
        {
            return Camera as P;
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

        private void darkButton_Click(object sender, EventArgs e)
        {
            Flags.CloseLaserAndFlash();
            dark = new int[Camera.AcqSize];
            LastStatus = Camera.Acquire(dark);
        }

        private void blankButton_Click(object sender, EventArgs e)
        {
            Flags.CloseLaserAndFlash();
            Flags.OpenFlash();
            blank = new int[Camera.AcqSize];
            LastStatus = Camera.Acquire(blank);
            ApplyDark(blank);
        }

        private void ApplyBlank(int[] data)
        {
            if (blank != null)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = blank[i] - data[i];
                }
            }
        }

        private void ApplyDark(int[] data)
        {
            if (dark != null)
            {
                Data.Dissipate(data, dark);
            }
        }

        private void AddSpec(int[] data)
        {
            for (int i = 0; i < data.Length; i++)
                specGraph.Series["spec"].Points.AddXY(i, data[i]);

            specGraph.Series["spec"].ChartArea = "specArea";

            RescaleChart();
        }

        private void RescaleChart()
        {
            double max = Double.MinValue;
            //double min = Double.MaxValue;
            foreach (Series s in specGraph.Series)
            {
                foreach (DataPoint p in s.Points)
                {
                    max = p.YValues[0] > max ? p.YValues[0] : max;
                    //min = p.YValues[0] < min ? p.YValues[0] : min;
                }
            }
            ChartArea MainChart = specGraph.ChartAreas.FindByName("specArea");
            //MainChart.AxisY.Minimum = min;
            MainChart.AxisY.Maximum = max;
        }

        private void TaskStart()
        {
            CameraConfigBox.Enabled = false;
            specButton.Enabled = blankButton.Enabled = darkButton.Enabled = false;
            Abort.Enabled = Pause.Enabled = true;
            WorkImage = Camera.Image;
            WorkCts = new CancellationTokenSource();
        }

        private void TaskFinish()
        {
            CameraConfigBox.Enabled = true;
            specButton.Enabled = blankButton.Enabled = darkButton.Enabled = true;
            Abort.Enabled = Pause.Enabled = false;
        }

        private async void specButton_Click(object sender, EventArgs e)
        {
            TaskStart();
            var progress = new Progress<int>(ReportProgress);
            await DoWorkAsync((int)NScans.Value, exciteCheck.Checked, progress, WorkCts.Token);
            TaskFinish();
        }

        private async Task DoWorkAsync(int NScans, bool ExciteSample, IProgress<int> progress, CancellationToken cancel)
        {
            await Task.Run(() => DoWork(NScans, ExciteSample, progress), cancel);
        }

        private void DoWork(int n, bool excite, IProgress<int> progress)
        {
            if (dark == null) { }
            if (blank == null) { }

            if (excite)
            {
                Flags.OpenLaserAndFlash();
            }
            else
            {
                Flags.OpenFlash();
            }

            int[] DataBuffer = new int[Camera.AcqSize];
            for (int i = 0; i < n; i++)
            {
                LastStatus = Camera.Acquire(DataBuffer);
                progress.Report(0);
            }

            Flags.CloseLaserAndFlash();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            ((ILuiObject)Flags).Dispose();
            ((ILuiObject)Camera).Dispose();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            specGraph.Series["spec"].Points.Clear();
        }

        private bool BlankDialog(int readMode)
        {
            DialogResult result = MessageBox.Show("Please insert blank",
                "Blank",
                MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                Flags.CloseLaserAndFlash();
                Flags.OpenFlash();
                blank = new int[Camera.AcqSize];
                LastStatus = Camera.Acquire(blank);
            }
            else
            {
                return false;
            }

            result = MessageBox.Show("Continue when ready",
                "Continue",
                MessageBoxButtons.OKCancel);

            if (result == DialogResult.Cancel) return false;

            return true;
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

        private void ReportProgress(int value)
        {

        }

        private void fvbButton_CheckedChanged(object sender, EventArgs e)
        {
            if (fvbButton.Checked)
            {
                Camera.ReadMode = AndorCamera.ReadModeFVB;
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
            
        }

        void GraphScroll_ValueChanged(object sender, EventArgs e)
        {
            UpdateSelectedRow();
        }

        private void Abort_Click(object sender, EventArgs e)
        {
            WorkCts.Cancel();
        }
    }
}

using lasercom;
using lasercom.io;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using lasercom.objects;

namespace DetectorTester
{
    public partial class DetectorTestForm : Form
    {
        private Commander Commander;
        
        private int[] blank;
        private int[] dark;
        private int[] counts;
        private int[] image;

        private BackgroundWorker worker;

        private MatFile DataFile;
        MatVar<int> RawData;
        MatVar<double> LuiData;

        struct WorkParameters
        {
            public int NSteps { get; set; }
            public int AcqMode { get; set; }
            public int ReadMode { get; set; }
            public int TriggerMode{ get; set; }
            public bool Excite { get; set; }
        }

        public DetectorTestForm()
        {
            Commander = new Commander();

            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
            worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.KineticSeriesAsync);
            worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ReportProgress);
            worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.WorkComplete);
            InitializeComponent();
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

        private void imageButton_Click(object sender, EventArgs e)
        {
            ShowImage( Commander.Camera.FullResolutionImage() );
        }

        private void darkButton_Click(object sender, EventArgs e)
        {
            Commander.Camera.AcquisitionMode = Constants.AcquisitionModeSingle;
            Commander.Camera.TriggerMode = Constants.TriggerModeExternalExposure;
            Commander.Camera.DDGTriggerMode = Constants.DDGTriggerModeExternal;
            Commander.Camera.ReadMode = Constants.ReadModeFVB;
            dark = Commander.Dark();
        }

        private void blankButton_Click(object sender, EventArgs e)
        {
            Commander.Camera.AcquisitionMode = Constants.AcquisitionModeSingle;
            Commander.Camera.TriggerMode = Constants.TriggerModeExternalExposure;
            Commander.Camera.DDGTriggerMode = Constants.DDGTriggerModeExternal;
            Commander.Camera.ReadMode = Constants.ReadModeFVB;
            blank = Commander.Flash();
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
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] -= dark[i];
                }
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

        private void specButton_Click(object sender, EventArgs e)
        {
            Commander.Camera.AcquisitionMode = Constants.AcquisitionModeSingle;
            Commander.Camera.TriggerMode = Constants.TriggerModeExternal;
            Commander.Camera.DDGTriggerMode = Constants.DDGTriggerModeExternal;
            Commander.Camera.ReadMode = Constants.ReadModeFVB;
            counts = Commander.Flash();
            ApplyDark(counts);
            ApplyBlank(counts);
            AddSpec(counts);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            ((LuiObject)Commander.Camera).Dispose();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            specGraph.Series["spec"].Points.Clear();
        }

        private void abortButton_Click(object sender, EventArgs e)
        {
            worker.CancelAsync();
        }

        private bool BlankDialog(int readMode)
        {
            DialogResult result = MessageBox.Show("Please insert blank",
                "Blank",
                MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                blank = Commander.Flash();
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

        private void startButton_Click(object sender, EventArgs e)
        {

            WorkParameters parameters = new WorkParameters();
            parameters.AcqMode = Constants.AcquisitionModeSingle;
            parameters.TriggerMode = Constants.TriggerModeExternalExposure;

            Commander.Camera.AcquisitionMode = parameters.AcqMode;
            Commander.Camera.TriggerMode = parameters.TriggerMode;

            parameters.Excite = exciteCheck.Checked;

            if (fvbButton.Checked)
            {
                parameters.ReadMode = Constants.ReadModeFVB;
            }
            else if (imageAcqButton.Checked)
            {
                parameters.ReadMode = Constants.ReadModeImage;
            }

            Commander.Camera.ReadMode = parameters.ReadMode;

            parameters.NSteps = (int)seriesLength.Value;

            if (!BlankDialog(parameters.ReadMode)) return;

            worker.RunWorkerAsync(parameters);
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

        private void KineticSeriesAsync(object sender, DoWorkEventArgs e)
        {
            WorkParameters parameters = (WorkParameters)e.Argument;

            int nsteps = parameters.NSteps;
            int cnt = 0;

            InitDataFile((int)Commander.Camera.AcqSize, nsteps, nsteps);

            dark = Commander.Dark();

            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            if (parameters.Excite)
            {
                int[] data = Commander.Trans();
                
                if (parameters.ReadMode == Constants.ReadModeFVB)
                {
                    ApplyDark(data);
                    ApplyBlank(data);
                    counts = data;
                    worker.ReportProgress(0, parameters.ReadMode);
                }
                else if (parameters.ReadMode == Constants.ReadModeImage)
                {
                    ApplyDark(data);
                    image = data;
                    worker.ReportProgress(0, parameters.ReadMode);
                    ApplyBlank(data);
                }
            }

            while (cnt <= nsteps)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                int[] data = Commander.Flash();
                
                if (parameters.ReadMode == Constants.ReadModeFVB)
                {
                    ApplyDark(data);
                    ApplyBlank(data);
                    counts = data;
                    worker.ReportProgress(0, parameters.ReadMode);
                }
                else if (parameters.ReadMode == Constants.ReadModeImage)
                {
                    ApplyDark(data);
                    image = data;
                    worker.ReportProgress(0, parameters.ReadMode);
                    ApplyBlank(data);
                }
                
                cnt++;
            }
            blank = null;
            dark = null;
            counts = null;
            image = null;
            DataFile.Close();
        }

        private void ReportProgress(object sender, ProgressChangedEventArgs e)
        {
            int readMode = (int)e.UserState;
            if (readMode == Constants.ReadModeFVB) AddSpec(counts);
            else if (readMode == Constants.ReadModeImage) ShowImage(image);
        }

        private void WorkComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            if (DataFile != null) DataFile.Close();
        }



    }
}

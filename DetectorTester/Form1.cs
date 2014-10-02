using LUI;
using LUI.io;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DetectorTester
{
    public partial class Form1 : Form
    {
        private Commander Commander;
        
        private int[] blank;
        private int[] dark;
        private int[] counts;
        private int[] image;

        private BackgroundWorker worker;
        private BackgroundWorker ioWorker;
        //private Queue<int[]> ioQueue;

        private MatFile OutFile;

        struct WorkParameters
        {
            public int NSteps { get; set; }
            public int AcqMode { get; set; }
            public int ReadMode { get; set; }
            public int TriggerMode{ get; set; }
            public bool Excite { get; set; }
        }

        public Form1(Commander commander)
        {
            Commander = commander;

            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.KineticSeriesAsync);
            worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ReportProgress);

            ioWorker = new BackgroundWorker();
            ioWorker.WorkerSupportsCancellation = true;
            ioWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.WriteDataAsync);
            
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
            Commander.Camera.ReadMode = Constants.ReadModeFVB;
            dark = Commander.Dark();
        }

        private void blankButton_Click(object sender, EventArgs e)
        {
            Commander.Camera.AcquisitionMode = Constants.AcquisitionModeSingle;
            Commander.Camera.TriggerMode = Constants.TriggerModeExternalExposure;
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
        }

        private void specButton_Click(object sender, EventArgs e)
        {
            Commander.Camera.AcquisitionMode = Constants.AcquisitionModeSingle;
            Commander.Camera.TriggerMode = Constants.TriggerModeExternalExposure;
            Commander.Camera.ReadMode = Constants.ReadModeFVB;
            counts = Commander.Flash();
            ApplyDark(counts);
            ApplyBlank(counts);
            AddSpec(counts);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            Commander.Camera.Close();
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

        private void ReportProgress(object sender, ProgressChangedEventArgs e)
        {
            int readMode = (int)sender;
            if (readMode == Constants.ReadModeFVB) AddSpec(counts);
            else if (readMode == Constants.ReadModeImage) ShowImage(image);
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

        private void KineticSeriesAsync(object sender, DoWorkEventArgs e)
        {
            WorkParameters parameters = (WorkParameters)e.Argument;

            string tempFile = Path.GetTempFileName();
            OutFile = new MatFile(tempFile, "int32", parameters.NSteps, (int)Commander.Camera.Width);

            int nsteps = parameters.NSteps;
            int cnt = 0;

            ioWorker.RunWorkerAsync(blank);

            dark = Commander.Dark();
            ioWorker.RunWorkerAsync(dark);

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
                    ioWorker.RunWorkerAsync(data);
                    ApplyDark(data);
                    ApplyBlank(data);
                    counts = data;
                    worker.ReportProgress(parameters.ReadMode);
                }
                else if (parameters.ReadMode == Constants.ReadModeImage)
                {
                    ApplyDark(data);
                    image = data;
                    worker.ReportProgress(parameters.ReadMode);
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
                    ioWorker.RunWorkerAsync(data);
                    ApplyDark(data);
                    ApplyBlank(data);
                    counts = data;
                    worker.ReportProgress(parameters.ReadMode);
                }
                else if (parameters.ReadMode == Constants.ReadModeImage)
                {
                    ApplyDark(data);
                    image = data;
                    worker.ReportProgress(parameters.ReadMode);
                    ApplyBlank(data);
                }
                
                cnt++;
            }
            blank = null;
            dark = null;
            counts = null;
            image = null;
            OutFile.Close();
        }

        private void WriteDataAsync(object data, DoWorkEventArgs e)
        {
            OutFile.WriteRow((int[])data);
        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Threading;

using LUI.ddg; //TODO use command transient delay for DDG classes & remove this line.

namespace LUI
{
    public partial class TROSForm : Form
    {
        private Commander Commander;
        private BackgroundWorker worker;
        private BackgroundWorker ioWorker;
        private Dispatcher Dispatcher;
        bool wait;

        private enum Dialog { BLANK, SAMPLE, PROGRESS, PROGRESS_BLANK, PROGRESS_DARK, PROGRESS_DATA, 
            PROGRESS_CALC, PROGRESS_TRANS, PROGRESS_GROUND };
        private enum TROAPattern { SEPARATED }

        public TROSForm(Commander commander)
        {
            Commander = commander;
            InitializeComponent();
            //Dispatcher = new Dispatcher();
            InitChart();
        }

        private void InitChart()
        {
            ChartArea MainChart = SpecGraph.ChartAreas.FindByName("MainChart");
            MainChart.AxisX.Minimum = 0;
            MainChart.AxisX.Maximum = Commander.Camera.Width - 1;
            Series series = SpecGraph.Series.Add("dummySeries");
            series.MarkerStyle = MarkerStyle.None;
            series.Points.AddXY(0, 0);
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            Commander.Camera.Close();
        }

        private void loadTimesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a times file";
            openFileDialog.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Commander.SetDelays(openFileDialog.FileName);
            }
        }

        private void AddSeries(double[] arr)
        {
            string name = "series" + SpecGraph.Series.Count;
            Series series = SpecGraph.Series.Add(name);
            series.ChartType = SeriesChartType.FastLine;
            for (int i = 0; i < arr.Length; i++)
            {
                series.Points.AddXY(i, arr[i]);
            }
            series.ChartArea = "MainChart";
        }

        private void RescaleChart()
        {
            double max = Double.MinValue;
            double min = Double.MaxValue;
            foreach (Series s in SpecGraph.Series)
            {
                foreach(DataPoint p in s.Points)
                {
                    max = p.YValues[0] > max ? p.YValues[0] : max;
                    min = p.YValues[0] < min ? p.YValues[0] : min;
                }
            }
            ChartArea MainChart = SpecGraph.ChartAreas.FindByName("MainChart");
            MainChart.AxisY.Minimum = min;
            MainChart.AxisY.Maximum = max;
        }

        private void Clear_Click(object sender, EventArgs e)
        {
            for (int i = SpecGraph.Series.Count - 1; i >= 0; i--)
            {
                if (SpecGraph.Series[i].Name != "dummySeries")
                {
                    SpecGraph.Series.RemoveAt(i);
                }
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

        private bool BlankDialog()
        {
            DialogResult result = MessageBox.Show("Please insert blank",
                "Blank",
                MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel) return false;
            return true;
        }

        private void BlockingBlankDialog()
        {
            DialogResult result = MessageBox.Show("Please insert blank",
                "Blank",
                MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel)
            {
                worker.CancelAsync();
            }
            wait = false;
        }

        private bool SampleDialog()
        {
            DialogResult result = MessageBox.Show("Please insert sample",
                    "Continue",
                    MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel) return false;
            return true;
        }

        private void BlockingSampleDialog()
        {
            DialogResult result = MessageBox.Show("Please insert sample",
                    "Continue",
                    MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel)
            {
                worker.CancelAsync();
            }
            wait = false;
        }

        private void Abort_Click(object sender, EventArgs e)
        {
            worker.CancelAsync();
        }

        private void Collect_Click(object sender, EventArgs e)
        {
            Collect.Enabled = false;
            int N = (int)Averages.Value;
            worker = new BackgroundWorker();
            worker.DoWork += new System.ComponentModel.DoWorkEventHandler(AbsorbanceSpectrumWork);
            worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(AbsorbanceSpectrumProgress);
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync(N);
        }

        public void AbsorbanceSpectrumWork(object sender, DoWorkEventArgs e)
        {
            Commander.Camera.AcquisitionMode = Constants.AcquisitionModeSingle;
            Commander.Camera.TriggerMode = Constants.TriggerModeExternalExposure;
            Commander.Camera.ReadMode = Constants.ReadModeFVB;
            int N = (int)e.Argument;

            worker.ReportProgress(0, Dialog.BLANK.ToString());

            int[] BlankBuffer = Commander.Flash();
            for (int i = 0; i < N-1; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                Data.Accumulate(BlankBuffer, Commander.Flash());
                worker.ReportProgress((i / N) * 33, Dialog.PROGRESS_BLANK.ToString());
            }

            worker.ReportProgress(33, Dialog.SAMPLE.ToString());

            wait = true;
            bool[] waitparam = { wait };
            Dispatcher.BeginInvoke(new Action(BlockingBlankDialog), waitparam);
            while (wait);

            worker.ReportProgress(33, Dialog.PROGRESS_DARK.ToString());

            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            int[] DarkBuffer = Commander.Dark();
            for (int i = 0; i < N-1; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                Data.Accumulate(DarkBuffer, Commander.Dark());
                worker.ReportProgress(33 + (i / N) * 33, Dialog.PROGRESS_DARK.ToString());
            }

            worker.ReportProgress(66, Dialog.PROGRESS.ToString());
            wait = true;
            Dispatcher.BeginInvoke(new Action(BlockingSampleDialog), new bool[] { wait });
            while (wait);

            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            worker.ReportProgress(66, Dialog.PROGRESS_DATA.ToString());

            int[] DataBuffer = Commander.Flash();
            for (int i = 0; i < N-1; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                Data.Accumulate(DataBuffer, Commander.Flash());
                worker.ReportProgress(66 + (i / N) * 33, Dialog.PROGRESS_DATA.ToString());
            }
            worker.ReportProgress(99, Dialog.PROGRESS_CALC.ToString());
            e.Result = Data.OpticalDensity(DataBuffer, BlankBuffer, DarkBuffer);
        }

        public void AbsorbanceSpectrumProgress(object sender, ProgressChangedEventArgs e)
        {
            Dialog operation = (Dialog)Enum.Parse(typeof(Dialog), (string)e.UserState);
            switch (operation)
            {
                case Dialog.BLANK:
                    StatusProgress.Value = e.ProgressPercentage;
                    ProgressLabel.Text = "Waiting";
                    break;
                case Dialog.SAMPLE:
                    StatusProgress.Value = e.ProgressPercentage;
                    ProgressLabel.Text = "Waiting";
                    break;
                case Dialog.PROGRESS:
                    StatusProgress.Value = e.ProgressPercentage;
                    ProgressLabel.Text = "Busy";
                    break;
                case Dialog.PROGRESS_BLANK:
                    StatusProgress.Value = e.ProgressPercentage;
                    ProgressLabel.Text = "Collecting blank";
                    break;
                case Dialog.PROGRESS_DARK:
                    StatusProgress.Value = e.ProgressPercentage;
                    ProgressLabel.Text = "Collecting dark";
                    break;
                case Dialog.PROGRESS_DATA:
                    StatusProgress.Value = e.ProgressPercentage;
                    ProgressLabel.Text = "Collecting data";
                    break;
                case Dialog.PROGRESS_CALC:
                    StatusProgress.Value = e.ProgressPercentage;
                    ProgressLabel.Text = "Calculating";
                    break;
                case Dialog.PROGRESS_TRANS:
                    StatusProgress.Value = e.ProgressPercentage;
                    ProgressLabel.Text = "Collecting trans";
                    break;
                case Dialog.PROGRESS_GROUND:
                    StatusProgress.Value = e.ProgressPercentage;
                    ProgressLabel.Text = "Collecting ground";
                    break;
            }
        }

        public void AbsorbanceSpectrumComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                double[] OD = (double[])e.Result;
                AddSeries(OD);
                RescaleChart();
                ProgressLabel.Text = "Complete";
            }
            else
            {
                ProgressLabel.Text = "Aborted";
            }
            StatusProgress.Value = 100;
            Collect.Enabled = true;
        }

        private class TROAParameters
        {
            public TROAPattern TROAPattern { get; set; }
            public List<string> TROADelays { get; set; }
            public int N { get; set; }
        }

        private void TROA_Click(object sender, EventArgs e)
        {
            worker = new BackgroundWorker();
            worker.DoWork += new System.ComponentModel.DoWorkEventHandler(TROAWork);
            worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(TROAProgress);
            worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(TROAComplete);
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;

            TROAParameters parameters = new TROAParameters();
            parameters.N = (int)Averages.Value;
            parameters.TROAPattern = TROAPattern.SEPARATED;
            parameters.TROADelays = new List<string>();
            foreach (double d in Commander.Delays)
            {
                parameters.TROADelays.Add(Constants.DDG535.BOutput + "," + d.ToString());
            }

            worker.RunWorkerAsync(parameters);
        }

        public void TROAWork(object sender, DoWorkEventArgs e)
        {
            TROAParameters parameters = (TROAParameters)e.Argument;

            Commander.Camera.AcquisitionMode = Constants.AcquisitionModeSingle;
            Commander.Camera.TriggerMode = Constants.TriggerModeExternalExposure;
            Commander.Camera.ReadMode = Constants.ReadModeFVB;

            // Dark
            worker.ReportProgress(0, Dialog.PROGRESS_DARK.ToString());
            int[] DarkBuffer = Commander.Dark();
            for (int i = 0; i < parameters.N-1; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                Data.Accumulate(DarkBuffer, Commander.Dark());
                worker.ReportProgress((i / parameters.N) * 20, Dialog.PROGRESS_DARK.ToString());
            }

            // Ground part 1
            worker.ReportProgress(20, Dialog.PROGRESS_GROUND.ToString());
            int[] GroundBuffer = Commander.Flash();
            // If N is odd the extra scan is done in the second ground stage.
            int niter = (parameters.N - 1) / 2 + 1;
            for (int i = 0; i < niter; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                Data.Accumulate(GroundBuffer, Commander.Flash());
                worker.ReportProgress(20 + (i / niter) * 20, Dialog.PROGRESS_GROUND.ToString());
            }

            // Trans
            worker.ReportProgress(40, Dialog.PROGRESS_TRANS.ToString());

            for (int ti = 0; ti < parameters.TROADelays.Count; ti++)
            {
                ((DDG535)Commander.DDG).BDelay = parameters.TROADelays[ti];
                int[] TransBuffer = Commander.Trans();
                niter = parameters.N - 1;
                for (int i = 0; i < niter; i++)
                {
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                    Data.Accumulate(TransBuffer, Commander.Trans());
                    worker.ReportProgress(40 + (i / niter) * 40, Dialog.PROGRESS_TRANS.ToString());
                }
                double[] DifferenceBuffer = Data.DeltaOD(GroundBuffer, TransBuffer, DarkBuffer);
            }

            // Ground part 2
            worker.ReportProgress(80, Dialog.PROGRESS_GROUND.ToString());
            niter = (parameters.N - 1) / 2 + 1;
            for (int i = 0; i < niter; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                Data.Accumulate(GroundBuffer, Commander.Flash());
                worker.ReportProgress(80 + (i / niter) * 20, Dialog.PROGRESS_GROUND.ToString());
            }
            
        }

        public void TROAProgress(object sender, ProgressChangedEventArgs e)
        {

        }

        public void TROAComplete(object sender, RunWorkerCompletedEventArgs e)
        {

        }

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace LUI
{
    public partial class ControlForm : Form
    {
        private Commander Commander;
        private BackgroundWorker worker;
        private BackgroundWorker ioWorker;

        private enum Dialog { BLANK, SAMPLE, PROGRESS, PROGRESS_BLANK, PROGRESS_DARK, PROGRESS_DATA, 
            PROGRESS_CALC, PROGRESS_TRANS, PROGRESS_GROUND };
        private enum TROAPattern { SEPARATED }

        public ControlForm(Commander commander)
        {
            Commander = commander;
            InitializeComponent();
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

        private bool BlankDialog()
        {
            DialogResult result = MessageBox.Show("Please insert blank",
                "Blank",
                MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel) return false;
            return true;
        }

        private bool SampleDialog()
        {
            DialogResult result = MessageBox.Show("Please insert sample",
                    "Continue",
                    MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel) return false;
            return true;
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
            worker.RunWorkerAsync(N);
        }

        public void AbsorbanceSpectrumWork(object sender, DoWorkEventArgs e)
        {
            Commander.Camera.AcquisitionMode = Constants.AcquisitionModeSingle;
            Commander.Camera.TriggerMode = Constants.TriggerModeExternalExposure;
            Commander.Camera.ReadMode = Constants.ReadModeFVB;
            int N = (int)sender;

            worker.ReportProgress(0, Dialog.BLANK);

            int[] BlankBuffer = Commander.Flash();
            for (int i = 0; i < N; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                Data.Accumulate(BlankBuffer, Commander.Flash());
                worker.ReportProgress((i / N) * 33, Dialog.PROGRESS_BLANK);
            }

            worker.ReportProgress(33, Dialog.SAMPLE);

            int[] DarkBuffer = Commander.Dark();
            for (int i = 0; i < N; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                Data.Accumulate(DarkBuffer, Commander.Dark());
                worker.ReportProgress(33 + (i / N) * 33, Dialog.PROGRESS_DARK);
            }

            worker.ReportProgress(66, Dialog.PROGRESS);

            int[] DataBuffer = Commander.Flash();
            for (int i = 0; i < N; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                Data.Accumulate(DataBuffer, Commander.Flash());
                worker.ReportProgress(66 + (i / N) * 33, Dialog.PROGRESS_DATA);
            }
            e.Result = Data.OpticalDensity(DataBuffer, BlankBuffer, DarkBuffer);
            worker.ReportProgress(99, Dialog.PROGRESS_CALC);
        }

        public void AbsorbanceSpectrumProgress(object sender, ProgressChangedEventArgs e)
        {
            Dialog operation = (Dialog)sender;
            switch (operation)
            {
                case Dialog.BLANK:
                    if (!BlankDialog()) worker.CancelAsync();
                    break;
                case Dialog.SAMPLE:
                    if (!SampleDialog()) worker.CancelAsync();
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
            TROAParameters parameters = (TROAParameters)sender;

            Commander.Camera.AcquisitionMode = Constants.AcquisitionModeSingle;
            Commander.Camera.TriggerMode = Constants.TriggerModeExternalExposure;
            Commander.Camera.ReadMode = Constants.ReadModeFVB;

            worker.ReportProgress(0, Dialog.PROGRESS_DARK);

            int[] DarkBuffer = Commander.Dark();
            for (int i = 0; i < parameters.N; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                Data.Accumulate(DarkBuffer, Commander.Dark());
                worker.ReportProgress((i / parameters.N) * 33, Dialog.PROGRESS_DARK);
            }


            int[] GroundBuffer = Commander.Flash();

            int[] TransBuffer = Commander.Trans();

            double[] DifferenceBuffer = Data.DeltaOD(GroundBuffer, TransBuffer, DarkBuffer);
        }

    }
}

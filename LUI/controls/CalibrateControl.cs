using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Threading;
using System.Runtime.CompilerServices;
using lasercom;
using System.IO;
using lasercom.io;

namespace LUI.controls
{
    public partial class CalibrateControl : LUIControl
    {
        private BackgroundWorker ioWorker;
        private Dispatcher Dispatcher;

        double[] OD = null;

        private int _SelectedChannel = -1;
        /// <summary>
        /// Currently selected detector channel regardless of calibration.
        /// Min value zero, max value is camera width - 1.
        /// </summary>
        int SelectedChannel
        {
            get
            {
                return _SelectedChannel;
            }
            set
            {
                _SelectedChannel = value;
                _SelectedChannel = Math.Max(Math.Min(value, (int)Commander.Camera.Width - 1), 0);
            }
        }

        /// <summary>
        /// Stores channel & wavelength point in a class suitable for data
        /// binding to a DataGridView.
        /// </summary>
        public class CalibrationPoint : INotifyPropertyChanged
        {
            private int _Channel;
            public int Channel
            {
                get
                {
                    return _Channel;
                }
                set
                {
                    if (value != _Channel)
                    {
                        _Channel = value;
                        NotifyPropertyChanged();
                    }
                }
            }
            private double _Wavelength;
            public double Wavelength
            {
                get
                {
                    return _Wavelength;
                }
                set
                {
                    if (value != _Wavelength)
                    {
                        _Wavelength = value;
                        NotifyPropertyChanged();
                    }
                }
            }
            public event PropertyChangedEventHandler PropertyChanged;
            private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            public static explicit operator CalibrationPoint(DataRow dr)
            {
                CalibrationPoint p = new CalibrationPoint();
                p.Channel = (int)dr.ItemArray[0];
                p.Wavelength = (double)dr.ItemArray[1];
                return p;
            }
            public static explicit operator CalibrationPoint(DataGridViewRow row)
            {
                return new CalibrationPoint()
                {
                    Channel = (int)row.Cells["Channel"].Value,
                    Wavelength = (double)row.Cells["Channel"].Value
                };
            }
            public static explicit operator Tuple<int,double>(CalibrationPoint p)
            {
                return Tuple.Create<int, double>(p.Channel, p.Wavelength);
            }
        }

        /// <summary>
        /// List of current calibration points, bound to CalibrationListView.
        /// </summary>
        BindingList<CalibrationPoint> CalibrationList = new BindingList<CalibrationPoint>();

        public enum Dialog
        {
            BLANK, SAMPLE, PROGRESS, PROGRESS_BLANK, PROGRESS_DARK, PROGRESS_DATA,
            PROGRESS_CALC
        }

        public CalibrateControl(Commander commander)
        {
            InitializeComponent();
            Commander = commander;
            Graph.MouseClick += new MouseEventHandler(Graph_Click);
            Graph.XMin = (float)Commander.Calibration[0];
            Graph.XMax = (float)Commander.Calibration[Commander.Calibration.Length - 1];

            CalibrationList.AllowEdit = true;
            CalibrationListView.DefaultValuesNeeded += CalibrationListView_DefaultValuesNeeded;
            CalibrationListView.EditingControlShowing += CalibrationListView_EditingControlShowing;
            CalibrationListView.DataSource = new BindingSource(CalibrationList, null);

            // Superscript the "2" in "R2"
            RSquaredLabel.SelectionStart = 1;
            RSquaredLabel.SelectionLength = 1;
            RSquaredLabel.SelectionCharOffset = 5;
            RSquaredLabel.SelectionFont = new Font("Microsoft Sans Serif", 6, FontStyle.Regular);
            RSquaredLabel.SelectionLength = 0;
        }

        public void CalibrateWork(object sender, DoWorkEventArgs e)
        {
            Commander.Camera.AcquisitionMode = AndorCamera.AcquisitionModeSingle;
            Commander.Camera.TriggerMode = AndorCamera.TriggerModeExternalExposure;
            Commander.Camera.ReadMode = AndorCamera.ReadModeFVB;
            int N = (int)e.Argument;

            worker.ReportProgress(0, Dialog.BLANK.ToString());

            int[] BlankBuffer = Commander.Flash();
            for (int i = 0; i < N - 1; i++)
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
            while (wait) ;

            worker.ReportProgress(33, Dialog.PROGRESS_DARK.ToString());

            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            int[] DarkBuffer = Commander.Dark();
            for (int i = 0; i < N - 1; i++)
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
            while (wait) ;

            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            worker.ReportProgress(66, Dialog.PROGRESS_DATA.ToString());

            int[] DataBuffer = Commander.Flash();
            for (int i = 0; i < N - 1; i++)
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

        private void Collect_Click(object sender, EventArgs e)
        {
            Collect.Enabled = false;
            int N = (int)Averages.Value;
            worker = new BackgroundWorker();
            worker.DoWork += new System.ComponentModel.DoWorkEventHandler(CalibrateWork);
            worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(CalibrateProgress);
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync(N);
        }

        public void CalibrateProgress(object sender, ProgressChangedEventArgs e)
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
            }
        }

        public void CalibrateComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled)
            {
                OD = (double[])e.Result;
                Graph.ClearData();
                Graph.DrawPoints(Commander.Calibration, OD);
                Graph.Invalidate();
                ProgressLabel.Text = "Complete";
            }
            else
            {
                ProgressLabel.Text = "Aborted";
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
            //PointF p = Graph.ScreenToData(new Point(e.X, e.Y));
            //SelectedChannel = (int)Math.Round(p.X);
            SelectedChannel = (int)Math.Round(Graph.CanvasToNormalized(Graph.ScreenToCanvas(new Point(e.X, e.Y))).X * (Commander.Camera.Width - 1));
            DataGridViewSelectedRowCollection selection = CalibrationListView.SelectedRows;
            if (selection.Count == 0)
            {
                DataGridViewRow row = CalibrationListView.Rows[CalibrationListView.Rows.Count - 1];
                row.Cells["Channel"].Value = SelectedChannel;
            }
            else if (selection.Count == 1)
            {
                selection[0].Cells["Channel"].Value = SelectedChannel;
            }
            else
            {
                CalibrationListView.ClearSelection();
            }
            RedrawLines();
        }

        /// <summary>
        /// Vertical lines are drawn for each calibration point at the
        /// X-position of the channel wrt to the current calibration vector.
        /// </summary>
        private void RedrawLines()
        {
            Graph.ClearAnnotation();
            int i;
            for (i=0; i<CalibrationList.Count; i++)
            {
                CalibrationPoint p = CalibrationList[i];
                Graph.Annotate(GraphControl.Annotation.VERTLINE, Graph.ColorOrder[i % Graph.ColorOrder.Count], Commander.Calibration[p.Channel]);
            }
            int newRowChannel = (int)(CalibrationListView.Rows[CalibrationListView.NewRowIndex].Cells["Channel"].Value ?? 0);
            if (CalibrationListView.Rows.Count > CalibrationList.Count && newRowChannel != 0)
            {
                Graph.Annotate(GraphControl.Annotation.VERTLINE, Graph.ColorOrder[i % Graph.ColorOrder.Count], Commander.Calibration[newRowChannel]);
            }
            Graph.Invalidate();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Left:
                    if (!CalibrationListView.IsCurrentCellInEditMode && CalibrationListView.SelectedRows.Count == 1)
                    {
                        SelectedChannel--;
                        CalibrationListView.SelectedRows[0].Cells["Channel"].Value = SelectedChannel;
                    }
                    RedrawLines();
                    break;
                case Keys.Right:
                    if (!CalibrationListView.IsCurrentCellInEditMode && CalibrationListView.SelectedRows.Count == 1)
                    {
                        SelectedChannel++;
                        CalibrationListView.SelectedRows[0].Cells["Channel"].Value = SelectedChannel;
                    }
                    RedrawLines();
                    break;
                case Keys.Enter:
                    //TODO add calibration point
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Called when editing CalibrationListView to set accepted key press events.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CalibrationListView_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (this.CalibrationListView.CurrentCell.ColumnIndex == 1)
            {
                if (e.Control is TextBox)
                {
                    TextBox tb = e.Control as TextBox;
                    tb.KeyPress -= new KeyPressEventHandler(TBKeyPress);
                    tb.KeyPress += new KeyPressEventHandler(TBKeyPress);
                }
            }
        }

        /// <summary>
        /// Key press event firing for digits, backspace and delete only.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        private void CalibrationListView_DefaultValuesNeeded(object sender,
    System.Windows.Forms.DataGridViewRowEventArgs e)
        {
            if (e.Row.Cells["Channel"].Value != null)
            {
                e.Row.Cells["Channel"].Value = SelectedChannel;
            }
            else
            {
                e.Row.Cells["Channel"].Value = 0;
            }
                
        }

        private void RemoveCalItem_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in CalibrationListView.SelectedRows)
            {
                if (!row.IsNewRow) CalibrationListView.Rows.Remove(row);
            }
            RedrawLines();
        }

        private void RunCal_Click(object sender, EventArgs e)
        {
            Tuple<double, double, double> fitdata = Data.LinearLeastSquares(CalibrationList.Select(it => (double)it.Channel).ToArray(),
                CalibrationList.Select(it => (double)it.Wavelength).ToArray());
            Commander.Calibration = Data.Calibrate((int)Commander.Camera.Width, fitdata.Item1, fitdata.Item2);

            Slope.Text = fitdata.Item1.ToString("n4");
            Intercept.Text = fitdata.Item2.ToString("n4");
            RSquared.Text = fitdata.Item3.ToString("n6");
        }

        private void SaveCal_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "CAL File|*.cal|MAT File|*.mat|All Files|*.*";
            saveFile.Title = "Save Calibration Data";
            saveFile.ShowDialog();

            if (saveFile.FileName == "") return;

            switch (saveFile.FilterIndex)
            {
                case 3:
                    // All files, fall through to CAL.
                case 1:
                    // CAL
                    try {
                        FileIO.WriteVector<double>(saveFile.FileName, Commander.Calibration);
                    } catch (IOException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    break;
                case 2:
                    // MAT
                    try
                    {
                        MatFile mat = new MatFile(saveFile.FileName, "cal", 
                            Commander.Calibration.Length, 1, "double");
                        mat.WriteColumn(Commander.Calibration);
                        mat.Dispose();
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    break;
            }
        }

        public void HandleCalibrationChanged(object sender, EventArgs args)
        {
            Graph.XMin = (float)Commander.Calibration[0];
            Graph.XMax = (float)Commander.Calibration[Commander.Calibration.Length - 1];
            if (OD != null)
            {
                Graph.Clear();
                Graph.DrawPoints(Commander.Calibration, OD);
            }
            RedrawLines();
        }

    }
}

using Extensions;
using lasercom;
using lasercom.camera;
using lasercom.io;
using lasercom.objects;
using LUI.config;
using LUI.controls;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace LUI.tabs
{
    public partial class CalibrateControl : LuiTab
    {
        public event EventHandler<LuiObjectParametersEventArgs> CalibrationChanged;

        double[] OD = null;
        int[] BlankBuffer = null;

        private int _SelectedChannel = -1;
        /// <summary>
        /// Currently selected detector channel regardless of calibration.
        /// Min value is zero, max value is camera width - 1.
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
                _SelectedChannel = Math.Max(Math.Min(value, Commander.Camera.Width - 1), 0);
            }
        }

        bool Ascending { get; set; }

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

        struct WorkArgs
        {
            public WorkArgs(int N)
            {
                this.N = N;
            }
            public readonly int N;
        }

        public CalibrateControl(LuiConfig config) : base(config)
        {

            InitializeComponent();
            
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

            Ascending = true;

            ClearBlank.Click += ClearBlank_Click;
            ClearBlank.Enabled = false;
        }

        public override void HandleCalibrationChanged(object sender, LuiObjectParametersEventArgs args)
        {
            // If a different camera is selected, do nothing (until that camera is selected by the user).
            if (!CameraBox.SelectedObject.Equals(args.Argument)) return;

            if (Ascending)
            {
                Graph.XLeft = (float)Commander.Camera.Calibration[0];
                Graph.XRight = (float)Commander.Camera.Calibration[Commander.Camera.Calibration.Length - 1];
            }
            else
            {
                Graph.XLeft = (float)Commander.Camera.Calibration[Commander.Camera.Calibration.Length - 1];
                Graph.XRight = (float)Commander.Camera.Calibration[0];
            }

            Graph.ClearAxes();

            RedrawLines();
        }

        public override void OnTaskStarted(EventArgs e)
        {
            base.OnTaskStarted(e);
            ClearBlank.Enabled = false;
        }

        public override void OnTaskFinished(EventArgs e)
        {
            base.OnTaskFinished(e);
            ClearBlank.Enabled = true;
        }

        protected override void DoWork(object sender, DoWorkEventArgs e)
        {
            WorkArgs args = (WorkArgs)e.Argument;
            int N = args.N;

            int AcqSize = Commander.Camera.AcqSize;
            int finalSize = Commander.Camera.ReadMode == AndorCamera.ReadModeImage ?
                AcqSize / Commander.Camera.Image.Height : AcqSize;

            if (PauseCancelProgress(e, 0, Dialog.PROGRESS_DARK.ToString())) return;

            int[] DataBuffer = new int[AcqSize];
            int[] DarkBuffer = new int[finalSize];

            Commander.BeamFlag.CloseLaserAndFlash();
            for (int i = 0; i < N; i++)
            {
                TryAcquire(DataBuffer);
                Data.ColumnSum(DarkBuffer, DataBuffer);
                if (PauseCancelProgress(e, i+1, Dialog.PROGRESS_DARK.ToString())) return;
            }

            if (PauseCancelProgress(e, 0, Dialog.BLANK.ToString())) return;

            if (BlankBuffer == null || BlankBuffer.Length != Commander.Camera.AcqSize)
            {
                Invoke(new Action(BlockingBlankDialog));

                Commander.BeamFlag.OpenFlash();

                BlankBuffer = new int[finalSize];
                for (int i = 0; i < N; i++)
                {
                    TryAcquire(DataBuffer);
                    Data.ColumnSum(BlankBuffer, DataBuffer);
                    if (PauseCancelProgress(e, i+1, Dialog.PROGRESS_BLANK.ToString())) return;
                }

                Commander.BeamFlag.CloseLaserAndFlash();

                if (PauseCancelProgress(e, 0, Dialog.SAMPLE.ToString())) return;

                Invoke(new Action(BlockingSampleDialog));
            }
            else
            {
                if (PauseCancelProgress(e, 0, Dialog.SAMPLE.ToString())) return;
            }

            if (PauseCancelProgress(e, 0, Dialog.PROGRESS_DATA.ToString())) return;

            Commander.BeamFlag.OpenFlash();

            int[] SampleBuffer = new int[finalSize];
            for (int i = 0; i < N; i++)
            {
                TryAcquire(DataBuffer);
                Data.ColumnSum(SampleBuffer, DataBuffer);
                if (PauseCancelProgress(e, i+1, Dialog.PROGRESS_DATA.ToString())) return;
            }
            Commander.BeamFlag.CloseLaserAndFlash();
            if (PauseCancelProgress(e, -1, Dialog.PROGRESS_CALC.ToString())) return;
            e.Result = Data.OpticalDensity(SampleBuffer, BlankBuffer, DarkBuffer);
        }

        protected override void Collect_Click(object sender, EventArgs e)
        {
            //Dispatcher = Dispatcher.CurrentDispatcher;
            Graph.ClearData();
            Graph.Invalidate();
            int N = (int)NScan.Value;

            Commander.BeamFlag.CloseLaserAndFlash();

            worker = new BackgroundWorker();
            worker.DoWork += new System.ComponentModel.DoWorkEventHandler(DoWork);
            worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(WorkProgress);
            worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(WorkComplete);
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync(new WorkArgs(N));
            OnTaskStarted(EventArgs.Empty);
        }

        protected override void WorkProgress(object sender, ProgressChangedEventArgs e)
        {
            Dialog operation = (Dialog)Enum.Parse(typeof(Dialog), (string)e.UserState);
            if (e.ProgressPercentage != -1)
                ScanProgress.Text = e.ProgressPercentage.ToString() +"/" + NScan.Value.ToString();
            switch (operation)
            {
                case Dialog.BLANK:
                    ProgressLabel.Text = "Waiting";
                    break;
                case Dialog.SAMPLE:
                    ProgressLabel.Text = "Waiting";
                    break;
                case Dialog.PROGRESS:
                    ProgressLabel.Text = "Busy";
                    break;
                case Dialog.PROGRESS_BLANK:
                    ProgressLabel.Text = "Collecting blank";
                    break;
                case Dialog.PROGRESS_DARK:
                    ProgressLabel.Text = "Collecting dark";
                    break;
                case Dialog.PROGRESS_DATA:
                    ProgressLabel.Text = "Collecting data";
                    break;
                case Dialog.PROGRESS_CALC:
                    ProgressLabel.Text = "Calculating";
                    break;
            }
        }

        protected override void WorkComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            Commander.BeamFlag.CloseLaserAndFlash();
            if (!e.Cancelled)
            {
                OD = (double[])e.Result;
                for (int i = 0; i < OD.Length; i++) if (Double.IsNaN(OD[i]) || Double.IsInfinity(OD[i])) OD[i] = 0;
                Graph.DrawPoints(Commander.Camera.Calibration, OD);
                Graph.Invalidate();
                ProgressLabel.Text = "Complete";
            }
            else
            {
                ProgressLabel.Text = "Aborted";
            }
            OnTaskFinished(EventArgs.Empty);
        }

        protected override void  Graph_Click(object sender, MouseEventArgs e)
        {
            SelectedChannel = Ascending ?
                (int)Math.Round(Graph.AxesToNormalized(Graph.ScreenToAxes(new Point(e.X, e.Y))).X * (Commander.Camera.Width - 1))
                :
                (int)Math.Round((1 - Graph.AxesToNormalized(Graph.ScreenToAxes(new Point(e.X, e.Y))).X) * (Commander.Camera.Width - 1));

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
                if (i > CalibrationListView.Rows.Count) break;
                CalibrationPoint p = CalibrationList[i];
                //float X = Graph.XLeft + (float)p.Channel / (Commander.Camera.Width - 1) * Graph.XRange;
                float X = (float)Commander.Camera.Calibration[p.Channel];
                Graph.Annotate(GraphControl.Annotation.VERTLINE, Graph.ColorOrder[i % Graph.ColorOrder.Count], X);
            }
            int newRowChannel = (int)(CalibrationListView.Rows[CalibrationListView.NewRowIndex].Cells["Channel"].Value ?? 0);
            if (CalibrationListView.Rows.Count > CalibrationList.Count && newRowChannel != 0)
            {
                //float X = Graph.XLeft + (float)newRowChannel / (Commander.Camera.Width - 1) * Graph.XRange;
                float X = (float)Commander.Camera.Calibration[newRowChannel];
                Graph.Annotate(GraphControl.Annotation.VERTLINE, Graph.ColorOrder[i % Graph.ColorOrder.Count], X);
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
                        if (Ascending) SelectedChannel--;
                        else SelectedChannel++;
                        CalibrationListView.SelectedRows[0].Cells["Channel"].Value = SelectedChannel;
                    }
                    RedrawLines();
                    break;
                case Keys.Right:
                    if (!CalibrationListView.IsCurrentCellInEditMode && CalibrationListView.SelectedRows.Count == 1)
                    {
                        if (Ascending) SelectedChannel++;
                        else SelectedChannel--;
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
            var minidx = int.MaxValue;
            foreach (DataGridViewRow row in CalibrationListView.SelectedRows)
            {
                minidx = Math.Min(minidx, row.Index - 1); // Minimum preceeding index.
                if (!row.IsNewRow) CalibrationListView.Rows.Remove(row);
            }
            CalibrationListView.ClearSelection();
            minidx = Math.Max(0, minidx);
            if (minidx < CalibrationListView.Rows.Count)
                CalibrationListView.Rows[minidx].Selected = true; // Select previous row if possible.
            RedrawLines();
        }

        private void RunCal_Click(object sender, EventArgs e)
        {
            Tuple<double, double, double> fitdata = Data.LinearLeastSquares(CalibrationList.Select(it => (double)it.Channel).ToArray(),
                CalibrationList.Select(it => (double)it.Wavelength).ToArray());
            Commander.Camera.Calibration = Data.Calibrate(Commander.Camera.Width, fitdata.Item1, fitdata.Item2);
            CalibrationChanged.Raise(this, new LuiObjectParametersEventArgs(CameraBox.SelectedObject));
            Slope.Text = fitdata.Item1.ToString("n4");
            Intercept.Text = fitdata.Item2.ToString("n4");
            RSquared.Text = fitdata.Item3.ToString("n6");
        }

        private void SaveCal_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "CAL File|*.cal|All Files|*.*";
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
                        FileIO.WriteVector<double>(saveFile.FileName, Commander.Camera.Calibration);
                    } catch (IOException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    break;
                case 2:
                    // MAT
                    try
                    {
                        MatFile mat = new MatFile(saveFile.FileName);
                        MatVar<double> V = mat.CreateVariable<double>("cal", Commander.Camera.Calibration.Length, 1);
                        V.WriteNext(Commander.Camera.Calibration,1);
                        mat.Dispose();
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    break;
            }
        }

        private void FlipGraph_Click(object sender, EventArgs e)
        {
            Ascending = !Ascending; // Toggle direction.

            if (Ascending)
            {
                Graph.XLeft = (float)Commander.Camera.Calibration[0];
                Graph.XRight = (float)Commander.Camera.Calibration[Commander.Camera.Calibration.Length - 1];
            }
            else
            {
                Graph.XLeft = (float)Commander.Camera.Calibration[Commander.Camera.Calibration.Length - 1];
                Graph.XRight = (float)Commander.Camera.Calibration[0];
            }

            Graph.ClearAxes();
            Graph.ClearData();
            if (OD != null) Graph.DrawPoints(Commander.Camera.Calibration, OD);
            RedrawLines();
        }

        void ClearBlank_Click(object sender, EventArgs e)
        {
 	        BlankBuffer = null;
            ClearBlank.Enabled = false;
        }
    }
}

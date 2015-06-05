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

        struct WorkArgs
        {
            public WorkArgs(int N, CalibrateControl UI)
            {
                this.N = N;
                this.UI = UI;
            }
            public readonly int N;
            public readonly CalibrateControl UI;
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
        }

        public override void HandleCalibrationChanged(object sender, LuiObjectParametersEventArgs args)
        {
            // If a different camera is selected, do nothing (until that camera is selected by the user).
            if (!CameraBox.SelectedObject.Equals(args.Argument)) return;

            Graph.XLeft = (float)Commander.Camera.Calibration[0];
            Graph.XRight = (float)Commander.Camera.Calibration[Commander.Camera.Calibration.Length - 1];
            Graph.ClearAxes();

            RedrawLines();
        }

        protected override void DoWork(object sender, DoWorkEventArgs e)
        {
            Commander.Camera.AcquisitionMode = AndorCamera.AcquisitionModeSingle;
            Commander.Camera.TriggerMode = AndorCamera.TriggerModeExternalExposure;
            Commander.Camera.DDGTriggerMode = AndorCamera.DDGTriggerModeExternal;
            Commander.Camera.ReadMode = AndorCamera.ReadModeFVB;
            WorkArgs args = (WorkArgs)e.Argument;
            int N = args.N;

            worker.ReportProgress(0, Dialog.PROGRESS_DARK.ToString());

            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            Commander.BeamFlag.CloseLaserAndFlash();

            int[] DarkBuffer = Commander.Camera.Acquire();
            for (int i = 0; i < N - 1; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                Data.Accumulate(DarkBuffer, Commander.Camera.Acquire());
                worker.ReportProgress(33 + (i / N) * 33, Dialog.PROGRESS_DARK.ToString());
            }

            worker.ReportProgress(33, Dialog.BLANK.ToString());

            wait = true;
            bool[] waitparam = { wait };
            //args.UI.Invoke(new Action(BlockingBlankDialog), waitparam);
            args.UI.Invoke(new Action(args.UI.BlockingBlankDialog));
            //while (wait) ;

            Commander.BeamFlag.OpenFlash();

            int[] BlankBuffer = Commander.Camera.Acquire();
            for (int i = 0; i < N - 1; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                Data.Accumulate(BlankBuffer, Commander.Camera.Acquire());
                worker.ReportProgress((i / N) * 33, Dialog.PROGRESS_BLANK.ToString());
            }

            Commander.BeamFlag.CloseLaserAndFlash();

            worker.ReportProgress(66, Dialog.SAMPLE.ToString());

            wait = true;
            args.UI.Invoke(new Action(args.UI.BlockingSampleDialog));
            //while (wait) ;

            if (worker.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            worker.ReportProgress(66, Dialog.PROGRESS_DATA.ToString());

            Commander.BeamFlag.OpenFlash();

            int[] DataBuffer = Commander.Camera.Acquire();
            for (int i = 0; i < N - 1; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }
                Data.Accumulate(DataBuffer, Commander.Camera.Acquire());
                worker.ReportProgress(66 + (i / N) * 33, Dialog.PROGRESS_DATA.ToString());
            }
            Commander.BeamFlag.CloseLaserAndFlash();
            worker.ReportProgress(99, Dialog.PROGRESS_CALC.ToString());
            e.Result = Data.OpticalDensity(DataBuffer, BlankBuffer, DarkBuffer);
        }

        protected override void Collect_Click(object sender, EventArgs e)
        {
            Collect.Enabled = NScan.Enabled = false;
            Abort.Enabled = true;
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
            worker.RunWorkerAsync(new WorkArgs(N, this));
            OnTaskStarted(EventArgs.Empty);
        }

        protected override void WorkProgress(object sender, ProgressChangedEventArgs e)
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
            StatusProgress.Value = 100;
            Collect.Enabled = NScan.Enabled = true;
            Abort.Enabled = false;
            OnTaskFinished(EventArgs.Empty);
        }

        protected override void Graph_Click(object sender, MouseEventArgs e)
        {
            //PointF p = Graph.ScreenToData(new Point(e.X, e.Y));
            //SelectedChannel = (int)Math.Round(p.X);
            SelectedChannel = (int)Math.Round(Graph.AxesToNormalized(Graph.ScreenToAxes(new Point(e.X, e.Y))).X * (Commander.Camera.Width - 1));
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
            Commander.Camera.Calibration = Data.Calibrate((int)Commander.Camera.Width, fitdata.Item1, fitdata.Item2);
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

    }
}

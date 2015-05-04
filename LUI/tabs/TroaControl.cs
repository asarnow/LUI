using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using lasercom.camera;
using lasercom.ddg;
using LUI.config;
using lasercom;
using lasercom.io;
using System.IO;

namespace LUI.tabs
{
    public partial class TroaControl : LUI.tabs.LuiTab
    {
        public class RoleRow : INotifyPropertyChanged
        {
            private string _Role;
            public string Role
            {
                get
                {
                    return _Role;
                }
                set
                {
                    _Role = value;
                    NotifyPropertyChanged();
                }
            }

            private DelayGeneratorParameters _DDG;
            public DelayGeneratorParameters DDG
            {
                get
                {
                    return _DDG;
                }
                set
                {
                    _DDG = value;
                    NotifyPropertyChanged();
                }
            }

            private string _Delay;
            public string Delay
            {
                get
                {
                    return _Delay;
                }
                set
                {
                    if (value != _Delay)
                    {
                        _Delay = value;
                        NotifyPropertyChanged();
                    }
                }
            }

            private string _Trigger;
            public string Trigger
            {
                get
                {
                    return _Trigger;
                }
                set
                {
                    _Trigger = value;
                    NotifyPropertyChanged();
                }
            }

            private double _DelayValue;
            public double DelayValue
            {
                get
                {
                    return _DelayValue;
                }
                set
                {
                    _DelayValue = value;
                    NotifyPropertyChanged();
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            
            private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
            {
                var handler = PropertyChanged;
                if (handler != null)
                {
                    handler(this, new PropertyChangedEventArgs(propertyName));
                }
            }

            public static explicit operator RoleRow(DataRow dr)
            {
                RoleRow p = new RoleRow();
                p.Role = (string)dr.ItemArray[0];
                p.DDG = (DelayGeneratorParameters)dr.ItemArray[1];
                p.Delay = (string)dr.ItemArray[2];
                p.Trigger = (string)dr.ItemArray[3];
                p.DelayValue = (double)dr.ItemArray[4];
                return p;
            }
            public static explicit operator RoleRow(DataGridViewRow row)
            {
                return new RoleRow()
                {
                    Role = (string)row.Cells["Role"].Value,
                    DDG = (DelayGeneratorParameters)row.Cells["DDG"].Value,
                    Delay = (string)row.Cells["Delay"].Value,
                    Trigger = (string)row.Cells["Trigger"].Value,
                    DelayValue = (double)row.Cells["DelayValue"].Value
                };
            }
        }

        struct WorkArgs
        {
            public WorkArgs(int N, IList<double> Times, RoleRow PrimaryDelay, RoleRow Gate)
            {
                this.N = N;
                this.Times = new List<double>(Times);
                this.PrimaryDelayName = PrimaryDelay.Delay[0];
                this.TriggerName = PrimaryDelay.Trigger[0];
                //this.GateName = new Tuple<char, char>(Gate.Delay[0], Gate.Delay[1]);
                //this.GateTriggerName = Gate.Trigger[0];
                //this.Gate = Gate.DelayValue;
                //this.GateDelay = Gate.DelayValue;
                this.GateName = null;
                this.GateTriggerName = '\0';
                this.Gate = double.NaN;
                this.GateDelay = double.NaN;
            }
            public readonly int N;
            public readonly IList<double> Times;
            public readonly char PrimaryDelayName;
            public readonly char TriggerName;
            public readonly Tuple<char,char> GateName;
            public readonly char GateTriggerName;
            public readonly double GateDelay;
            public readonly double Gate;
        }

        struct ProgressObject
        {
            public ProgressObject(int[] Data, double Delay, Dialog Status)
            {
                this.Data = Data;
                this.Delay = Delay;
                this.Status = Status;
            }
            public readonly int[] Data;
            public readonly double Delay;
            public readonly Dialog Status;
        }

        public enum Dialog
        {
            INITIALIZE, PROGRESS, PROGRESS_DARK, PROGRESS_TIME, PROGRESS_TIME_COMPLETE, PROGRESS_FLASH, PROGRESS_TRANS
        }

        private BindingList<RoleRow> RoleList = new BindingList<RoleRow>();
        IList<double> Times { get; set; }
        RoleRow PrimaryDelay;
        RoleRow Gate;
        MatFile DataFile;

        public TroaControl(LuiConfig Config) : base(Config)
        {
            InitializeComponent();
            Init();

            RoleList.AllowEdit = true;
            RoleListView.DefaultValuesNeeded += DDGListView_DefaultValuesNeeded;
            RoleListView.DataSource = new BindingSource(RoleList, null);

            PrimaryDelay = new RoleRow();
            PrimaryDelay.Role = "Primary Delay";
            RoleList.Add(PrimaryDelay);

            Gate = new RoleRow();
            Gate.Role = "Gate";
            //RoleList.Add(Gate);

            PrimaryDelay.PropertyChanged += Role_PropertyChanged;
            Gate.PropertyChanged += Role_PropertyChanged;
        }

        private void Init()
        {
            SuspendLayout();

            //DataGridViewComboBoxColumn Role = (DataGridViewComboBoxColumn)RoleListView.Columns["Role"];
            //Role.Items.Add("Primary Delay");
            //Role.Items.Add("Gate");
            DataGridViewComboBoxColumn DDG = (DataGridViewComboBoxColumn)RoleListView.Columns["DDG"];
            DDG.DisplayMember = "Name";
            DDG.ValueMember = "Self";

            ResumeLayout();
        }

        protected override void Graph_Click(object sender, MouseEventArgs e)
        {
            base.Graph_Click(sender, e);
        }

        protected override void Collect_Click(object sender, EventArgs e)
        {
            Collect.Enabled = NScan.Enabled = CameraGain.Enabled = false;
            Abort.Enabled = true;
            
            Graph.ClearData();
            Graph.Invalidate();

            int N = (int)NScan.Value;
            worker = new BackgroundWorker();
            worker.DoWork += new System.ComponentModel.DoWorkEventHandler(DoWork);
            worker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(WorkProgress);
            worker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(WorkComplete);
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync(new WorkArgs(N, Times, PrimaryDelay, Gate));
        }

        protected override void DoWork(object sender, DoWorkEventArgs e)
        {
            var progress = new ProgressObject(null, 0, Dialog.INITIALIZE);
            worker.ReportProgress(0, progress); // Show zero progress.

            // Set camera for external gate and full vertical binning.
            if (Commander.Camera is AndorCamera)
            {
                Commander.Camera.AcquisitionMode = AndorCamera.AcquisitionModeSingle;
                Commander.Camera.TriggerMode = AndorCamera.TriggerModeExternalExposure;
                Commander.Camera.DDGTriggerMode = AndorCamera.DDGTriggerModeExternal;
                Commander.Camera.ReadMode = AndorCamera.ReadModeFVB;
            }

            var args = (WorkArgs)e.Argument;
            int N = args.N; // Save typing for later.
            int half = N / 2; // Integer division rounds down.
            IList<double> Times = args.Times;

            // Total scans = dark scans + ground state scans + plus time series scans.
            int TotalScans = 2*N + Times.Count * N;

            // Create the data store.
            InitDataFile((int)Commander.Camera.AcqSize, TotalScans);

            // Measure dark current.
            progress = new ProgressObject(null, 0, Dialog.PROGRESS_DARK);
            worker.ReportProgress(0, progress);

            int[] DarkBuffer = Commander.Dark();
            for (int i = 0; i < N - 1; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                Data.Accumulate(DarkBuffer, Commander.Dark());

                progress = new ProgressObject(null, 0, Dialog.PROGRESS_DARK);
                worker.ReportProgress((i + 1) / TotalScans, progress);
            }
            Data.DivideArray(DarkBuffer, N);

            // Buffer for acuisition data.
            int[] DataBuffer = new int[Commander.Camera.AcqSize];

            // Ground state scans - first half.
            for (int i = 0; i < half; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                uint ret = Commander.Flash(DataBuffer);

                progress = new ProgressObject(DataBuffer, 0, Dialog.PROGRESS_FLASH);
                worker.ReportProgress((N + (i+1)) / TotalScans, progress); // Handle new data.
            }

            // Excited state scans.
            for (int i = 0; i < Times.Count; i++)
            {
                double Delay = Times[i];
                progress = new ProgressObject(null, Delay, Dialog.PROGRESS_TIME);
                worker.ReportProgress((half + i * N) / TotalScans, progress); // Display current delay.
                for (int j = 0; j < N; j++)
                {
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    Commander.DDG.SetDelay(args.PrimaryDelayName, args.TriggerName, Delay); // Set delay time.
                    
                    uint ret = Commander.Trans(DataBuffer);

                    progress = new ProgressObject(DataBuffer, Delay, Dialog.PROGRESS_TRANS);
                    worker.ReportProgress( (N + half + (i+1) * (j+1)) / TotalScans , progress); // Handle new data.
                }
                progress = new ProgressObject(DataBuffer, Delay, Dialog.PROGRESS_TIME_COMPLETE);
            }

            // Ground state scans - second half.
            int half2 = N % 2 == 0 ? half : half + 1; // If N is odd, need 1 more GS scan in the second half.

            for (int i = 0; i < half2; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                uint ret = Commander.Flash(DataBuffer);

                progress = new ProgressObject(DataBuffer, 0, Dialog.PROGRESS_FLASH);
                worker.ReportProgress( (N + half + (N * Times.Count) + (i+1)) / TotalScans , progress);
            }


        }

        protected override void WorkProgress(object sender, ProgressChangedEventArgs e)
        {
            var progress = (ProgressObject)e.UserState;
            StatusProgress.Value = e.ProgressPercentage;
            switch (progress.Status)
            {
                case Dialog.INITIALIZE:
                    break;
                case Dialog.PROGRESS:
                    break;
                case Dialog.PROGRESS_DARK:
                    break;
                case Dialog.PROGRESS_TIME:
                    PrimaryDelay.DelayValue = progress.Delay;
                    break;
                case Dialog.PROGRESS_TIME_COMPLETE:
                    //Display(Data.DeltaOD(progress.Data,Accumulator));
                    break;
                case Dialog.PROGRESS_FLASH:
                    DataFile.WriteNextColumn(progress.Data);
                    //Data.Accummulate(Accumulator,progress.Data);
                    break;
                case Dialog.PROGRESS_TRANS:
                    DataFile.WriteNextColumn(progress.Data);
                    break;
            }
        }

        protected override void WorkComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                // Handle the exception thrown in the worker thread.
                MessageBox.Show(e.Error.ToString());
            }
            else if (e.Cancelled)
            {
                ProgressLabel.Text = "Aborted";
            }
            else
            {
                //TODO Diff all TS from final GS average
                //TODO Save data
                ProgressLabel.Text = "Complete";
            }
            StatusProgress.Value = 100;
            Collect.Enabled = NScan.Enabled = CameraGain.Enabled = true;
            Abort.Enabled = false;
        }

        private void DDGListView_DefaultValuesNeeded(object sender,
            System.Windows.Forms.DataGridViewRowEventArgs e)
        {
            e.Row.Cells["Role"] = null;
            e.Row.Cells["Delay"].Value = null;
            e.Row.Cells["Trigger"] = null;
            e.Row.Cells["DelayValue"].Value = 0.0;
        }

        public override void HandleParametersChanged(object sender, EventArgs e)
        {
            base.HandleParametersChanged(sender, e); // Takes care of ObjectSelectPanel.

            DataGridViewComboBoxColumn col = (DataGridViewComboBoxColumn)RoleListView.Columns["DDG"];
            col.Items.Clear();
            var parameters = Config.GetParameters(typeof(DelayGeneratorParameters));
            foreach (var p in parameters)
            {
                col.Items.Add(p);
            }
        }

        void Role_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var row = (RoleRow)sender;
            var dgvRow = RoleListView.Rows[FindRowByRoleName(row.Role)];
            if (e.PropertyName == "DDG") // Changed selected DDG
            {
                IDigitalDelayGenerator DDG = (IDigitalDelayGenerator)Config.GetObject(row.DDG);
                // Re-populate the available delay and trigger choices.
                var cell = (DataGridViewComboBoxCell)dgvRow.Cells["Delay"];
                cell.Items.Clear();
                if (row == PrimaryDelay)
                {
                    Commander.DDG = DDG;
                    foreach (string d in DDG.Delays) cell.Items.Add(d);
                }
                else if (row == Gate)
                {
                    foreach (string d in DDG.DelayPairs) cell.Items.Add(d);
                }
                cell = (DataGridViewComboBoxCell)dgvRow.Cells["Trigger"];
                cell.Items.Clear();
                foreach (string d in DDG.Triggers) cell.Items.Add(d);
            }
            else if (e.PropertyName == "Delay" || e.PropertyName == "Trigger" || e.PropertyName == "DelayValue")
            {
                // Get row.DDG object, set object's row.Delay = row.Trigger + row.DelayValue
            }
        }

        private int FindRowByRoleName(string searchValue)
        {
            int rowIndex = -1;

            DataGridViewRow row = RoleListView.Rows
                .Cast<DataGridViewRow>()
                .Where(r => r.Cells["Role"].Value.ToString().Equals(searchValue))
                .First();

            rowIndex = row.Index;

            return rowIndex;
        }

        private void LoadTimes_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Text File|*.txt|All Files|*.*";
            openFile.Title = "Load Time Series File";
            openFile.ShowDialog();

            if (openFile.FileName == "") return;

            try
            {
                Times = FileIO.ReadTimesFile(openFile.FileName);
            }
            catch (IOException ex)
            {
                Log.Error(ex);
                MessageBox.Show("Couldn't load times file at " + openFile.FileName);
            }
                
        }

        private void InitDataFile(int NumChannels, int NumScans)
        {
            string TempFileName = Path.GetTempFileName();
            DataFile = new MatFile(TempFileName, "luidata", NumChannels, NumScans, "int32");
        }
    }
}

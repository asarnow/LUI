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
                    Delays = (string[])_DDG.Type.GetField("Delays", BindingFlags.Public | BindingFlags.Static).GetValue(null);
                    Triggers = (string[])_DDG.Type.GetField("Triggers", BindingFlags.Public | BindingFlags.Static).GetValue(null);
                    NotifyPropertyChanged();
                }
            }

            private string[] _Delays;
            public string[] Delays
            {
                get
                {
                    return _Delays;
                }
                private set
                {
                    _Delays = value;
                }
            }

            private string[] _Triggers;
            public string[] Triggers
            {
                get
                {
                    return _Triggers;
                }
                private set
                {
                    _Triggers = value;
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
            public WorkArgs(int N, IList<double> Times)
            {
                this.N = N;
                this.Times = new List<double>(Times);
            }
            public readonly int N;
            public readonly IList<double> Times;
        }

        struct ProgressObject
        {
            public ProgressObject(object Data, Dialog Status)
            {
                this.Data = Data;
                this.Status = Status;
            }
            public readonly object Data;  
            public readonly Dialog Status;
        }

        public enum Dialog
        {
            PROGRESS, PROGRESS_FLASH, PROGRESS_TRANS
        }

        private BindingList<RoleRow> RoleList = new BindingList<RoleRow>();
        IList<double> Times { get; set; }

        public TroaControl(LuiConfig Config) : base(Config)
        {
            InitializeComponent();
            Init();

            RoleList.AllowEdit = true;
            RoleListView.DefaultValuesNeeded += DDGListView_DefaultValuesNeeded;
            RoleListView.DataSource = new BindingSource(RoleList, null);

            RoleRow PrimaryDelay = new RoleRow();
            PrimaryDelay.Role = "Primary Delay";
            RoleList.Add(PrimaryDelay);

            RoleRow Gate = new RoleRow();
            Gate.Role = "Gate";
            RoleList.Add(Gate);

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
            worker.RunWorkerAsync(new WorkArgs(N, Times));
        }

        protected override void DoWork(object sender, DoWorkEventArgs e)
        {
            var args = (WorkArgs)e.Argument;
            Commander.Camera.AcquisitionMode = AndorCamera.AcquisitionModeSingle;
            Commander.Camera.TriggerMode = AndorCamera.TriggerModeExternalExposure;
            Commander.Camera.DDGTriggerMode = AndorCamera.DDGTriggerModeExternal;
            Commander.Camera.ReadMode = AndorCamera.ReadModeFVB;

            int N = args.N;
            int half = N / 2; // Integer division rounds down.
            IList<double> Times = args.Times;

            int TotalScans = N + Times.Count * N;

            var progress = new ProgressObject(null, Dialog.PROGRESS);
            worker.ReportProgress(0, progress);

            int[] DataBuffer = new int[Commander.Camera.AcqSize];

            for (int i = 0; i < half; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                uint ret = Commander.Flash(DataBuffer);

                progress = new ProgressObject(DataBuffer, Dialog.PROGRESS_FLASH);
                worker.ReportProgress((i+1) / TotalScans, progress);
            }

            for (int i = 0; i < Times.Count; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }

                    uint ret = Commander.Trans(DataBuffer);

                    progress = new ProgressObject(DataBuffer, Dialog.PROGRESS_TRANS);
                    worker.ReportProgress( (half + (i+1) * (j+1)) / TotalScans , progress);
                }
            }

            // If N is odd, need 1 more GS scan in the second half.
            int half2 = N % 2 != 0 ? half : half + 1;

            for (int i = 0; i < half2; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    return;
                }

                uint ret = Commander.Flash(DataBuffer);

                progress = new ProgressObject(DataBuffer, Dialog.PROGRESS_FLASH);
                worker.ReportProgress( (half + (N * Times.Count) + (i+1)) / TotalScans , progress);
            }


        }

        protected override void WorkProgress(object sender, ProgressChangedEventArgs e)
        {
            var progress = (ProgressObject)e.UserState;
            switch (progress.Status)
            {
                case Dialog.PROGRESS:
                    break;
                case Dialog.PROGRESS_FLASH:
                    break;
                case Dialog.PROGRESS_TRANS:
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
                // Re-populate the available delay and trigger choices.
                var cell = (DataGridViewComboBoxCell)dgvRow.Cells["Delay"];
                cell.Items.Clear();
                foreach (string d in row.Delays) cell.Items.Add(d);
                cell = (DataGridViewComboBoxCell)dgvRow.Cells["Trigger"];
                cell.Items.Clear();
                foreach (string d in row.Triggers) cell.Items.Add(d);
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
            // Load time series file.
        }
    }
}

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

        private BindingList<RoleRow> RoleList = new BindingList<RoleRow>();

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
            base.Collect_Click(sender, e);
        }

        protected override void DoWork(object sender, DoWorkEventArgs e)
        {
            Commander.Camera.AcquisitionMode = AndorCamera.AcquisitionModeSingle;
            Commander.Camera.TriggerMode = AndorCamera.TriggerModeExternalExposure;
            Commander.Camera.DDGTriggerMode = AndorCamera.DDGTriggerModeExternal;
            Commander.Camera.ReadMode = AndorCamera.ReadModeFVB;


        }

        protected override void WorkProgress(object sender, ProgressChangedEventArgs e)
        {
            base.WorkProgress(sender, e);
        }

        protected override void WorkComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            base.WorkComplete(sender, e);
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using lasercom.gpib;
using lasercom.extensions;
using System.Reflection;

namespace LUI.controls
{
    public partial class GPIBOptionsDialog : LUIOptionsDialog
    {
        LabeledControl<ComboBox> GPIBProviderTypes;
        ListView GPIBProviders;
        Button Add, Remove;

        Dictionary<Type, GPIBProviderConfigPanel> ProviderConfigPanels;
        Dictionary<Type, MethodInfo> ProviderConfigCopyTo;
        Dictionary<Type, MethodInfo> ProviderConfigCopyFrom;

        /// <summary>
        /// Extends ListViewItem to hold two generic parameter objects.
        /// Persistent will be restored 
        /// </summary>
        private class GPIBProviderItem : ListViewItem
        {
            public GPIBProviderParameters Persistent;
            public GPIBProviderParameters Transient;

            public GPIBProviderItem(string text) : base(text) { }
        }

        public GPIBOptionsDialog()
        {
            InitializeComponent();
            Init();
        }

        public GPIBOptionsDialog(Size Size, bool Visibility)
        {
            InitializeComponent();
            this.Size = Size;
            this.Visible = Visibility;
            Init();
        }

        public GPIBOptionsDialog(Size Size) : this(Size, true) {}

        private void Init()
        {
            SuspendLayout();

            Panel ConfigPanel = new Panel();
            ConfigPanel.Dock = DockStyle.Fill;
            Controls.Add(ConfigPanel);

            Panel ListPanel = new Panel();
            ListPanel.Dock = DockStyle.Left;
            Controls.Add(ListPanel);

            #region Provider list and configuration panel setup
            GPIBProviders = new ListView();
            GPIBProviders.HeaderStyle = ColumnHeaderStyle.None;
            GPIBProviders.Columns.Add(new ColumnHeader());
            GPIBProviders.Columns[0].Width = GPIBProviders.Width;
            GPIBProviders.View = View.Details;
            GPIBProviders.ShowGroups = false;
            GPIBProviders.Dock = DockStyle.Top;
            GPIBProviders.HideSelection = false;
            GPIBProviders.MultiSelect = false;
            GPIBProviders.SelectedIndexChanged += SelectedGPIBProviderChanged;
            GPIBProviderItem nextItem = new GPIBProviderItem("New..."); // Dummy item
            GPIBProviders.Items.Add(nextItem);

            Panel ListControlsPanel = new Panel();
            ListControlsPanel.Dock = DockStyle.Top;

            #region Buttons
            Add = new Button();
            Add.Dock = DockStyle.Left;
            Add.Text = "Add";
            Add.Click += Add_Click;

            Remove = new Button();
            Remove.Dock = DockStyle.Left;
            Remove.Text = "Remove";
            Remove.Click += Remove_Click;

            ListControlsPanel.Controls.Add(Remove);
            ListControlsPanel.Controls.Add(Add);
            #endregion

            ListPanel.Controls.Add(ListControlsPanel);
            ListPanel.Controls.Add(GPIBProviders);
            #endregion

            #region Provider configuration panel
            Panel ConfigSubPanel = new Panel();
            ConfigSubPanel.Dock = DockStyle.Fill;
            ConfigPanel.Controls.Add(ConfigSubPanel);

            GPIBProviderTypes = new LabeledControl<ComboBox>(new ComboBox(), "GPIB Controller Type:");
            GPIBProviderTypes.Control.DropDownStyle = ComboBoxStyle.DropDownList;
            GPIBProviderTypes.Control.DisplayMember = "Name";
            List<Type> ProviderTypes = typeof(GPIBProviderParameters).GetSubclasses(true);
            ProviderTypes.ForEach(x => {
                GPIBProviderTypes.Control.Items.Add(x);
            });
            GPIBProviderTypes.Control.SelectedIndex = 0;
            GPIBProviderTypes.Control.SelectedIndexChanged += SelectedGPIBProviderTypeChanged;
            GPIBProviderTypes.Dock = DockStyle.Top;
            ConfigPanel.Controls.Add(GPIBProviderTypes);
            #endregion

            #region Type-specific configuration
            ProviderConfigPanels = new Dictionary<Type, GPIBProviderConfigPanel>();
            ProviderConfigCopyFrom = new Dictionary<Type,MethodInfo>();
            ProviderConfigCopyTo = new Dictionary<Type,MethodInfo>();
            foreach (Type t in new Type[]{ typeof(NIConfigPanel), typeof(PrologixConfigPanel)})
            {
                GPIBProviderConfigPanel c = (GPIBProviderConfigPanel)Activator.CreateInstance(t);
                c.FlowDirection = FlowDirection.TopDown;
                c.Dock = DockStyle.Fill;
                c.Visible = false;
                c.ConfigChanged += UpdateSelectedProvider;
                ConfigSubPanel.Controls.Add(c);
                ProviderConfigPanels.Add(c.ParameterType, c);
                ProviderConfigCopyFrom.Add(c.ParameterType, t.GetMethod("CopyFrom"));
                ProviderConfigCopyTo.Add(c.ParameterType, t.GetMethod("CopyTo"));
            }

            #endregion

            ResumeLayout(false);

            ProviderConfigPanels[(Type)GPIBProviderTypes.Control.SelectedItem].Visible = true;
            GPIBProviders.SelectedIndices.Add(0);
        }

        private void UpdateSelectedProvider(object sender, EventArgs e)
        {
            GPIBProviderItem selectedItem = (GPIBProviderItem)GPIBProviders.SelectedItems[0];
            GPIBProviderParameters gpibParameters = (GPIBProviderParameters)selectedItem.Transient;
            Type t = gpibParameters.GetType();
            ProviderConfigCopyTo[t].Invoke(ProviderConfigPanels[t], new object[] { gpibParameters });
        }

        private void SelectedGPIBProviderChanged(object sender, EventArgs e)
        {
            if (GPIBProviders.SelectedIndices.Count == 0) 
                return;

            GPIBProviderItem selectedItem = (GPIBProviderItem)GPIBProviders.SelectedItems[0];
            if (selectedItem.Index == GPIBProviders.Items.Count - 1)
            {
                Remove.Enabled = false;
                Add.Enabled = true;

                if (selectedItem.Transient == null)
                {
                    selectedItem.Transient = (GPIBProviderParameters)
                        Activator.CreateInstance((Type)GPIBProviderTypes.Control.SelectedItem);
                }
            }
            else
            {
                Remove.Enabled = true;
                Add.Enabled = false;
            }
            
            GPIBProviderParameters gpibParameters = (GPIBProviderParameters)selectedItem.Transient;
            Type t = gpibParameters.GetType();
            ProviderConfigCopyFrom[t].Invoke(ProviderConfigPanels[t], new object[] {gpibParameters});
            GPIBProviderTypes.Control.SelectedItem = t;
        }

        private void ShowConfigPanel(Type GPIBType)
        {
            foreach (Type t in ProviderConfigPanels.Keys)
            {
                if (t != GPIBType) ProviderConfigPanels[t].Visible = false;
            }
            ProviderConfigPanels[GPIBType].Visible = true;
        }

        private void SelectedGPIBProviderTypeChanged(object sender, EventArgs e)
        {
            GPIBProviderItem selectedItem = (GPIBProviderItem)GPIBProviders.SelectedItems[0];
            Type t = (Type)GPIBProviderTypes.Control.SelectedItem;
            selectedItem.Transient = (GPIBProviderParameters)Activator.CreateInstance(t);
            // Set the visible control
            
            ShowConfigPanel(t);
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            GPIBProviderItem selectedItem = (GPIBProviderItem)GPIBProviders.SelectedItems[0];
            int idx = Math.Max(selectedItem.Index - 1, 0);
            selectedItem.Selected = false;
            selectedItem.Remove();
            GPIBProviders.Items[idx].Selected = true;
        }

        private void Add_Click(object sender, EventArgs e)
        {
            GPIBProviderItem dummyRow = (GPIBProviderItem)GPIBProviders.Items[GPIBProviders.Items.Count - 1];
            AddGPIBProvider(dummyRow.Transient);
            dummyRow.Transient = null;
            dummyRow.Selected = false;
            GPIBProviders.Items[dummyRow.Index - 1].Selected = true;
        }

        public void AddGPIBProvider(GPIBProviderParameters gpibParameters)
        {
            GPIBProviderItem newItem = new GPIBProviderItem(gpibParameters.Name);
            //newItem.Transient = (GPIBProviderParameters)Activator.CreateInstance(gpibParameters.ProviderType);
            newItem.Transient = gpibParameters;
            newItem.Persistent = null;
            GPIBProviders.Items.Insert(GPIBProviders.Items.Count - 1, newItem);
        }

        public void Restore()
        {
            throw new NotImplementedException();
        }

        public override void OnApply()
        {
            // for each provider (except dummy)
            throw new NotImplementedException();
        }
    }
}

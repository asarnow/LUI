﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using lasercom.extensions;
using lasercom.objects;

namespace LUI.controls
{
    public class LuiOptionsListDialog<T,P> : LuiOptionsDialog where P:LuiObjectParameters,new()
    {
        LabeledControl<ComboBox> ObjectTypes;
        LabeledControl<TextBox> ObjectName;
        ListView ObjectView;
        Button Add, Remove;

        Panel ConfigSubPanel;

        Dictionary<Type, LuiObjectConfigPanel> ConfigPanels;

        Dictionary<string, T> Objects;

        /// <summary>
        /// Extends ListViewItem to hold two generic parameter objects.
        /// Persistent will be restored 
        /// </summary>
        private class LuiObjectItem : ListViewItem
        {
            public P Persistent;
            public P Transient;

            public LuiObjectItem(string text) : base(text) { }
        }

        public LuiOptionsListDialog()
        {
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Init();
        }

        public LuiOptionsListDialog(Size size, bool visibility)
        {
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Size = size;
            this.Visible = visibility;
            Init();
        }

        public LuiOptionsListDialog(Size size) : this(size, true) {}

        private void Init()
        {
            SuspendLayout();

            #region Object list and configuration panel setup
            Panel ConfigPanel = new Panel();
            ConfigPanel.Dock = DockStyle.Fill;
            Controls.Add(ConfigPanel);

            Panel ListPanel = new Panel();
            ListPanel.Dock = DockStyle.Left;
            Controls.Add(ListPanel);

            ObjectView = new ListView();
            ObjectView.HeaderStyle = ColumnHeaderStyle.None;
            ObjectView.Columns.Add(new ColumnHeader());
            ObjectView.Columns[0].Width = ObjectView.Width;
            ObjectView.View = View.Details;
            ObjectView.ShowGroups = false;
            ObjectView.Dock = DockStyle.Top;
            ObjectView.HideSelection = false;
            ObjectView.MultiSelect = false;
            ObjectView.SelectedIndexChanged += SelectedObjectChanged;
            LuiObjectItem nextItem = new LuiObjectItem("New..."); // Dummy item
            ObjectView.Items.Add(nextItem);

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
            ListPanel.Controls.Add(ObjectView);
            #endregion

            #region Configuration panel
            ConfigSubPanel = new Panel();
            ConfigSubPanel.Dock = DockStyle.Fill;
            ConfigPanel.Controls.Add(ConfigSubPanel);

            ObjectTypes = new LabeledControl<ComboBox>(new ComboBox(), "Type:");
            ObjectTypes.Dock = DockStyle.Top;
            ObjectTypes.Control.DropDownStyle = ComboBoxStyle.DropDownList;
            ObjectTypes.Control.DisplayMember = "Name";
            List<Type> AvailableTypes = typeof(T).GetSubclasses(true);
            AvailableTypes.ForEach(x => {
                ObjectTypes.Control.Items.Add(x);
            });
            ObjectTypes.Control.SelectedIndex = 0;
            ObjectTypes.Control.SelectedIndexChanged += SelectedObjectTypeChanged;
            
            ObjectName = new LabeledControl<TextBox>(new TextBox(), "Name:");
            ObjectName.Dock = DockStyle.Top;
            ObjectName.Control.TextChanged += SelectedObjectNameChanged;
            ConfigPanel.Controls.Add(ObjectName);
            ConfigPanel.Controls.Add(ObjectTypes);
            #endregion

            ConfigPanels = new Dictionary<Type, LuiObjectConfigPanel>();

            ResumeLayout(false);

            #region Set default selected items
            ConfigPanels[(Type)ObjectTypes.Control.SelectedItem].Visible = true;
            ObjectView.SelectedIndices.Add(0);
            #endregion
        }

        public void AddConfigPanel(LuiObjectConfigPanel c)
        {
            c.FlowDirection = FlowDirection.TopDown;
            c.Dock = DockStyle.Fill;
            c.Visible = false;
            c.ConfigChanged += UpdateSelectedObject;
            ConfigSubPanel.Controls.Add(c);
            ConfigPanels.Add(c.Target, c);
        }

        private void UpdateSelectedObject(object sender, EventArgs e)
        {
            LuiObjectItem selectedItem = (LuiObjectItem)ObjectView.SelectedItems[0];
            P gpibParameters = (P)selectedItem.Transient;
            ConfigPanels[gpibParameters.Type].CopyTo(gpibParameters);
        }

        private void SelectedObjectChanged(object sender, EventArgs e)
        {
            if (ObjectView.SelectedIndices.Count == 0) 
                return;

            LuiObjectItem selectedItem = (LuiObjectItem)ObjectView.SelectedItems[0];
            if (selectedItem.Index == ObjectView.Items.Count - 1)
            {
                Remove.Enabled = false;
                Add.Enabled = true;

                if (selectedItem.Transient == null)
                {
                    selectedItem.Transient = new P();
                    selectedItem.Transient.Type = (Type)ObjectTypes.Control.SelectedItem;
                }
            }
            else
            {
                Remove.Enabled = true;
                Add.Enabled = false;
            }
            
            P p = (P)selectedItem.Transient;
            ConfigPanels[p.Type].CopyFrom(p);
            ObjectTypes.Control.SelectedItem = p.Type;
            ObjectName.Control.Text = p.Name;
        }

        private void ShowConfigPanel(Type type)
        {
            foreach (Type t in ConfigPanels.Keys)
            {
                if (t != type) ConfigPanels[t].Visible = false;
            }
            ConfigPanels[type].Visible = true;
        }

        private void SelectedObjectTypeChanged(object sender, EventArgs e)
        {
            LuiObjectItem selectedItem = (LuiObjectItem)ObjectView.SelectedItems[0];
            Type t = (Type)ObjectTypes.Control.SelectedItem;
            selectedItem.Transient.Type = t;
            ShowConfigPanel(t);
        }

        private void SelectedObjectNameChanged(object sender, EventArgs e)
        {
            LuiObjectItem selectedItem = (LuiObjectItem)ObjectView.SelectedItems[0];
            selectedItem.Transient.Name = ObjectName.Control.Text;
            if (selectedItem.Index != ObjectView.Items.Count - 1)
            {
                selectedItem.Text = selectedItem.Transient.Name;
            }
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            LuiObjectItem selectedItem = (LuiObjectItem)ObjectView.SelectedItems[0];
            int idx = Math.Max(selectedItem.Index - 1, 0);
            selectedItem.Selected = false;
            selectedItem.Remove();
            ObjectView.Items[idx].Selected = true;
        }

        private void Add_Click(object sender, EventArgs e)
        {
            LuiObjectItem dummyRow = (LuiObjectItem)ObjectView.Items[ObjectView.Items.Count - 1];
            AddObject(dummyRow.Transient);
            dummyRow.Transient = null;
            dummyRow.Selected = false;
            ObjectView.Items[dummyRow.Index - 1].Selected = true;
        }

        public void AddObject(P p)
        {
            LuiObjectItem newItem = new LuiObjectItem(p.Name);
            //newItem.Transient = (LUIObjectParameters)Activator.CreateInstance(gpibParameters.ProviderType);
            newItem.Transient = p;
            newItem.Persistent = null;
            ObjectView.Items.Insert(ObjectView.Items.Count - 1, newItem);
        }

        public void Restore()
        {
            throw new NotImplementedException();
        }

        public override void OnApply()
        {
            // Persist all entries excet dummy
            /*
            for (int i = 0; i < GPIBProviderView.Items.Count - 1; i++)
            {   
                GPIBProviderItem item = (GPIBProviderItem)GPIBProviderView.Items[i];
                if (item.Persistent == null)
                {
                    item.Persistent = GPIBProviderFactory.CreateGPIBProviderParameters(item.Transient);
                }
                else if (!item.Persistent.Equals(item.Transient))
                {
                    GPIBProviderFactory.CopyParameters(item.Persistent, item.Transient);
                    
                }
                T provider = null;
                if (GPIBProviders.TryGetValue(item.Persistent.Name, out provider))
                {
                    GPIBProviders[item.Persistent.Name].Dispose();
                    GPIBProviders[item.Persistent.Name] = GPIBProviderFactory.CreateGPIBProvider(item.Persistent);
                }
                else
                {
                    GPIBProviders.Add(item.Persistent.Name, GPIBProviderFactory.CreateGPIBProvider(item.Persistent));
                }
            }
            */
        }
    }
}
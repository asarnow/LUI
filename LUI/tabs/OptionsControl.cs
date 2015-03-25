using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lasercom;
using lasercom.camera;
using lasercom.control;
using lasercom.ddg;
using lasercom.gpib;
using LUI.controls;
using LUI.config;

namespace LUI.tabs
{
    public class OptionsControl : UserControl
    {
        ListView OptionsListView;
        Button Apply;
        LuiOptionsDialog ActiveDialog;

        public OptionsControl(LuiConfig Config)
        {
            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Name = "OptionsControl";
            Load += new System.EventHandler(this.OnLoad);

            SuspendLayout();

            Panel OptionsPanel = new Panel();
            OptionsPanel.Dock = DockStyle.Fill;
            Controls.Add(OptionsPanel);

            Panel ListPanel = new Panel();
            ListPanel.Dock = DockStyle.Left;
            Controls.Add(ListPanel);

            OptionsListView = new OptionsListView();
            OptionsListView.Dock = DockStyle.Fill;
            OptionsListView.HideSelection = false; // Maintain highlighting if user changes control focus.
            OptionsListView.MultiSelect = false;
            //OptionsListView.LabelWrap = false;
            OptionsListView.HeaderStyle = ColumnHeaderStyle.None;
            OptionsListView.Columns.Add(new ColumnHeader());
            OptionsListView.View = View.Details;
            OptionsListView.ShowGroups = true;
            OptionsListView.SelectedIndexChanged += SelectedOptionsDialogChanged;
            ListPanel.Controls.Add(OptionsListView);

            #region Options dialogs
            ListViewGroup General = new ListViewGroup("General", HorizontalAlignment.Left);
            OptionsListView.Groups.Add(General);
            ListViewGroup Instruments = new ListViewGroup("Instruments", HorizontalAlignment.Left);
            OptionsListView.Groups.Add(Instruments);

            LoggingOptionsDialog LoggingOptionsDialog = new LoggingOptionsDialog(OptionsPanel.Size, false);
            LoggingOptionsDialog.Dock = DockStyle.Fill;
            ListViewItem LoggingOptionsItem = new ListViewItem("Logging", General);
            LoggingOptionsItem.Tag = LoggingOptionsDialog;
            OptionsListView.Items.Add(LoggingOptionsItem);
            OptionsPanel.Controls.Add(LoggingOptionsDialog);

            LuiOptionsListDialog<AbstractBeamFlags,BeamFlagsParameters> BeamFlagOptionsDialog =
                new LuiOptionsListDialog<AbstractBeamFlags, BeamFlagsParameters>(OptionsPanel.Size, false);
            BeamFlagOptionsDialog.AddConfigPanel(new BeamFlagsConfigPanel());
            BeamFlagOptionsDialog.AddConfigPanel(new DummyBeamFlagsConfigPanel());
            BeamFlagOptionsDialog.SetDefaultSelectedItems();
            BeamFlagOptionsDialog.Dock = DockStyle.Fill;
            ListViewItem BeamFlagOptionsItem = new ListViewItem("Beam Flags", Instruments);
            BeamFlagOptionsItem.Tag = BeamFlagOptionsDialog;
            OptionsListView.Items.Add(BeamFlagOptionsItem);
            OptionsPanel.Controls.Add(BeamFlagOptionsDialog);

            LuiOptionsListDialog<ICamera, CameraParameters> CameraOptionsDialog = 
                new LuiOptionsListDialog<ICamera, CameraParameters>(OptionsPanel.Size, false);
            CameraOptionsDialog.AddConfigPanel(new AndorCameraConfigPanel());
            CameraOptionsDialog.AddConfigPanel(new CameraTempControlledConfigPanel());
            CameraOptionsDialog.AddConfigPanel(new DummyAndorCameraConfigPanel());
            CameraOptionsDialog.AddConfigPanel(new DummyCameraConfigPanel());
            CameraOptionsDialog.SetDefaultSelectedItems();
            CameraOptionsDialog.Dock = DockStyle.Fill;
            ListViewItem CameraOptionsItem = new ListViewItem("Camera", Instruments);
            CameraOptionsItem.Tag = CameraOptionsDialog;
            OptionsListView.Items.Add(CameraOptionsItem);
            OptionsPanel.Controls.Add(CameraOptionsDialog);

            LuiOptionsListDialog<IGpibProvider, GpibProviderParameters> GPIBOptionsDialog =
                new LuiOptionsListDialog<IGpibProvider, GpibProviderParameters>(OptionsPanel.Size, false);
            GPIBOptionsDialog.AddConfigPanel(new NIConfigPanel());
            GPIBOptionsDialog.AddConfigPanel(new PrologixConfigPanel());
            GPIBOptionsDialog.SetDefaultSelectedItems();
            GPIBOptionsDialog.Dock = DockStyle.Fill;
            ListViewItem GPIBOptionsItem = new ListViewItem("GPIB Controllers", Instruments);
            GPIBOptionsItem.Tag = GPIBOptionsDialog;
            OptionsListView.Items.Add(GPIBOptionsItem);
            OptionsPanel.Controls.Add(GPIBOptionsDialog);

            LuiOptionsListDialog<IDigitalDelayGenerator, DelayGeneratorParameters> DDGOptionsDialog = 
                new LuiOptionsListDialog<IDigitalDelayGenerator, DelayGeneratorParameters>(OptionsPanel.Size, false);
            DDGOptionsDialog.AddConfigPanel(new DDG535ConfigPanel());
            DDGOptionsDialog.AddConfigPanel(new DummyDigitalDelayGeneratorConfigPanel());
            DDGOptionsDialog.SetDefaultSelectedItems();
            DDGOptionsDialog.Dock = DockStyle.Fill;
            ListViewItem DDGOptionsItem = new ListViewItem("Digital Delay Generators", Instruments);
            DDGOptionsItem.Tag = DDGOptionsDialog;
            OptionsListView.Items.Add(DDGOptionsItem);
            OptionsPanel.Controls.Add(DDGOptionsDialog);
            #endregion
            
            OptionsListView.Columns[0].Width = -1; // Sets width to that of widest item.
            OptionsListView.Items[0].Selected = true; // Default options dialog.

            SetConfig(Config);

            Apply = new Button();
            Apply.Text = "Apply";
            Apply.Size = new Size(91, 34);
            Apply.Anchor = ((System.Windows.Forms.AnchorStyles)
                       ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            Apply.Click += Apply_Click;
            Apply.Enabled = false;
            foreach(ListViewItem item in OptionsListView.Items) ((LuiOptionsDialog)item.Tag).OptionsChanged += (s, e) => Apply.Enabled = true;
            OptionsPanel.Controls.Add(Apply);
            Apply.BringToFront();

            ResumeLayout(false);
        }

        private void OnLoad(object sender, EventArgs e)
        {
            // Selecting the ListView causes selected item to be highlighted with system color.
            OptionsListView.Select();
        }

        private void Apply_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in OptionsListView.Items) ((LuiOptionsDialog)item.Tag).HandleApply(sender, e);
            Apply.Enabled = false;
        }

        private void Close_Click(object sender, EventArgs e)
        {

        }

        private void SelectedOptionsDialogChanged(object sender, EventArgs e)
        {
            if (ActiveDialog != null) ActiveDialog.Visible = false;

            if (OptionsListView.SelectedItems.Count != 0)
            {
                ListViewItem selectedItem = OptionsListView.SelectedItems[0];
                ActiveDialog = (LuiOptionsDialog)selectedItem.Tag;
            }

            if (ActiveDialog != null)
            {
                ActiveDialog.Visible = true;
                //ActiveDialog.OnSetActive();
            }
        }

        public void SetConfig(LuiConfig Config)
        {
            foreach (ListViewItem it in OptionsListView.Items)
            {
                ((LuiOptionsDialog)it.Tag).Config = Config;
            }
        }
    }
}

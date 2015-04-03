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
using log4net;

namespace LUI.tabs
{
    public class OptionsControl : UserControl
    {
        protected static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        ListView OptionsListView;
        LuiOptionsDialog ActiveDialog;
        Button ApplyConfig;
        Button SaveConfig;
        Button LoadConfig;

        private LuiConfig _Config;
        private LuiConfig Config
        {
            get
            {
                return _Config;
            }
            set
            {
                if (_Config != null) _Config.Dispose();
                _Config = value;
                foreach (ListViewItem it in OptionsListView.Items)
                {
                    ((LuiOptionsDialog)it.Tag).Config = Config;
                }
            }
        }

        public OptionsControl(LuiConfig config)
        {
            SuspendLayout();

            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Name = "OptionsControl";

            #region Panels and list of options dialogs
            Panel OptionsPanel = new Panel();
            OptionsPanel.Dock = DockStyle.Fill;
            Controls.Add(OptionsPanel);

            Panel ListPanel = new Panel();
            ListPanel.Dock = DockStyle.Left;
            Controls.Add(ListPanel);

            OptionsListView = new OptionsListView();
            OptionsListView.Dock = DockStyle.Fill;
            OptionsListView.HideSelection = false; // Maintain highlighting if user changes control focus.
            OptionsListView.MultiSelect = false; // Only select one item at a time.
            //OptionsListView.LabelWrap = false;
            // A dummy column header and details view are needed to get the grouped style
            // we want for the list of options dialogs.
            OptionsListView.HeaderStyle = ColumnHeaderStyle.None;
            OptionsListView.Columns.Add(new ColumnHeader());
            OptionsListView.View = View.Details;
            OptionsListView.ShowGroups = true;
            OptionsListView.SelectedIndexChanged += HandleSelectedOptionsDialogChanged;
            ListPanel.Controls.Add(OptionsListView);
            #endregion

            #region Options dialogs
            ListViewGroup General = new ListViewGroup("General", HorizontalAlignment.Left);
            OptionsListView.Groups.Add(General);
            ListViewGroup Instruments = new ListViewGroup("Instruments", HorizontalAlignment.Left);
            OptionsListView.Groups.Add(Instruments);

            LoggingOptionsDialog LoggingOptionsDialog = new LoggingOptionsDialog(OptionsPanel.Size);
            LoggingOptionsDialog.Dock = DockStyle.Fill;
            ListViewItem LoggingOptionsItem = new ListViewItem("Logging", General);
            LoggingOptionsItem.Tag = LoggingOptionsDialog;
            OptionsListView.Items.Add(LoggingOptionsItem);
            OptionsPanel.Controls.Add(LoggingOptionsDialog);

            LuiOptionsListDialog<AbstractBeamFlags,BeamFlagsParameters> BeamFlagOptionsDialog =
                new LuiOptionsListDialog<AbstractBeamFlags, BeamFlagsParameters>(OptionsPanel.Size);
            BeamFlagOptionsDialog.AddConfigPanel(new BeamFlagsConfigPanel());
            BeamFlagOptionsDialog.AddConfigPanel(new DummyBeamFlagsConfigPanel());
            BeamFlagOptionsDialog.SetDefaultSelectedItems();
            BeamFlagOptionsDialog.Dock = DockStyle.Fill;
            ListViewItem BeamFlagOptionsItem = new ListViewItem("Beam Flags", Instruments);
            BeamFlagOptionsItem.Tag = BeamFlagOptionsDialog;
            OptionsListView.Items.Add(BeamFlagOptionsItem);
            OptionsPanel.Controls.Add(BeamFlagOptionsDialog);

            LuiOptionsListDialog<ICamera, CameraParameters> CameraOptionsDialog = 
                new LuiOptionsListDialog<ICamera, CameraParameters>(OptionsPanel.Size);
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
                new LuiOptionsListDialog<IGpibProvider, GpibProviderParameters>(OptionsPanel.Size);
            GPIBOptionsDialog.AddConfigPanel(new NIConfigPanel());
            GPIBOptionsDialog.AddConfigPanel(new PrologixConfigPanel());
            GPIBOptionsDialog.SetDefaultSelectedItems();
            GPIBOptionsDialog.Dock = DockStyle.Fill;
            ListViewItem GPIBOptionsItem = new ListViewItem("GPIB Controllers", Instruments);
            GPIBOptionsItem.Tag = GPIBOptionsDialog;
            OptionsListView.Items.Add(GPIBOptionsItem);
            OptionsPanel.Controls.Add(GPIBOptionsDialog);

            LuiOptionsListDialog<IDigitalDelayGenerator, DelayGeneratorParameters> DDGOptionsDialog = 
                new LuiOptionsListDialog<IDigitalDelayGenerator, DelayGeneratorParameters>(OptionsPanel.Size);
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

            // Note OptionsChanged and ConfigChanged handlers are not yet bound.
            //SetConfig(config);
            Config = config;

            #region Buttons
            FlowLayoutPanel ButtonPanel = new FlowLayoutPanel();
            ButtonPanel.FlowDirection = FlowDirection.RightToLeft;
            ButtonPanel.Anchor = ((System.Windows.Forms.AnchorStyles)
                        ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            ButtonPanel.AutoSize = true;
            ButtonPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;

            ApplyConfig = new Button();
            ApplyConfig.Text = "Apply";
            ApplyConfig.Size = new Size(91, 34);
            ApplyConfig.Click += ApplyConfig_Click;
            ApplyConfig.Enabled = false;

            SaveConfig = new Button();
            SaveConfig.Text = "Save";
            SaveConfig.Size = new Size(91, 34);
            SaveConfig.Enabled = false;
            SaveConfig.Click += SaveConfig_Click;

            LoadConfig = new Button();
            LoadConfig.Text = "Load";
            LoadConfig.Size = new Size(91, 34);
            LoadConfig.Click += LoadConfig_Click;

            ButtonPanel.Controls.Add(LoadConfig);
            ButtonPanel.Controls.Add(SaveConfig);
            ButtonPanel.Controls.Add(ApplyConfig);

            OptionsPanel.Controls.Add(ButtonPanel);

            ButtonPanel.BringToFront();
            #endregion

            ResumeLayout(false);
        }

        protected override void OnLoad(EventArgs e)
        {
            foreach (ListViewItem item in OptionsListView.Items)
            {
                var luiOptionsDialog = (LuiOptionsDialog)item.Tag;
                // Set in OnLoad so initialization doesn't trigger the events.
                luiOptionsDialog.OptionsChanged += (s, ev) => ApplyConfig.Enabled = true;
                luiOptionsDialog.ConfigChanged += (s, ev) => ApplyConfig.Enabled = true;

                // Control initialization doesn't happen until control is visible,
                // so we defer setting visibility until the control is loaded.
                luiOptionsDialog.Visible = false;
            }
            // Selecting the ListView causes selected item to be highlighted with system color.
            OptionsListView.Select();

            OptionsListView.Items[0].Selected = true; // Select default options dialog.
            base.OnLoad(e); // Forward to base class event handler.
        }

        private void ApplyConfig_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in OptionsListView.Items) ((LuiOptionsDialog)item.Tag).HandleApply(sender, e);
            //((Button)sender).Enabled = false;
            ApplyConfig.Enabled = false;
            SaveConfig.Enabled = true;
        }

        private void SaveConfig_Click(object sender, EventArgs e)
        {
            Config.Save();
            SaveConfig.Enabled = false;
        }

        private void LoadConfig_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            //ofd.RestoreDirectory = true;
            ofd.Filter = "XML Files|*.xml";
            ofd.FileName = "config.xml";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Config = LuiConfig.FromFile(ofd.FileName);
            }
        }

        private void HandleSelectedOptionsDialogChanged(object sender, EventArgs e)
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

    }
}

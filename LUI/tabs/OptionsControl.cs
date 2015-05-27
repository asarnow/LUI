using lasercom.camera;
using lasercom.control;
using lasercom.ddg;
using lasercom.extensions;
using lasercom.gpib;
using log4net;
using LUI.config;
using LUI.controls;
using System;
using System.Drawing;
using System.Windows.Forms;

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

        LuiConfig Config;

        public event EventHandler OptionsApplied;
        
        public OptionsControl(LuiConfig config)
        {
            SuspendLayout();

            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            Name = "OptionsControl";

            #region Panels and list of options dialogs
            Panel OptionsPanel = new Panel(); // Container for options dialogs.
            OptionsPanel.Dock = DockStyle.Fill; // Panel will fill all left-over space.
            Controls.Add(OptionsPanel); // Must add the DockStyle.Fill control first.

            Panel ListPanel = new Panel(); // Container for listview of options dialogs.
            ListPanel.Dock = DockStyle.Left; // Panel will dock to the left.
            Controls.Add(ListPanel);

            OptionsListView = new OptionsListView();
            OptionsListView.Dock = DockStyle.Fill; // Fill available space.
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
            GPIBOptionsDialog.AddConfigPanel(new DummyGpibProviderConfigPanel());
            GPIBOptionsDialog.SetDefaultSelectedItems();
            GPIBOptionsDialog.Dock = DockStyle.Fill;
            ListViewItem GPIBOptionsItem = new ListViewItem("GPIB Controllers", Instruments);
            GPIBOptionsItem.Tag = GPIBOptionsDialog;
            OptionsListView.Items.Add(GPIBOptionsItem);
            OptionsPanel.Controls.Add(GPIBOptionsDialog);

            LuiOptionsListDialog<IDigitalDelayGenerator, DelayGeneratorParameters> DDGOptionsDialog = 
                new LuiOptionsListDialog<IDigitalDelayGenerator, DelayGeneratorParameters>(OptionsPanel.Size);
            DDGOptionsDialog.AddConfigPanel(new DG535ConfigPanel(GPIBOptionsDialog));
            DDGOptionsDialog.AddConfigPanel(new DummyDigitalDelayGeneratorConfigPanel());
            DDGOptionsDialog.SetDefaultSelectedItems();
            DDGOptionsDialog.Dock = DockStyle.Fill;
            ListViewItem DDGOptionsItem = new ListViewItem("Digital Delay Generators", Instruments);
            DDGOptionsItem.Tag = DDGOptionsDialog;
            OptionsListView.Items.Add(DDGOptionsItem);
            OptionsPanel.Controls.Add(DDGOptionsDialog);

            LuiOptionsListDialog<IPump, PumpParameters> PumpOptionsDialog =
                new LuiOptionsListDialog<IPump, PumpParameters>(OptionsPanel.Size);
            PumpOptionsDialog.AddConfigPanel(new HarvardPumpConfigPanel());
            PumpOptionsDialog.AddConfigPanel(new DummyPumpConfigPanel());
            PumpOptionsDialog.SetDefaultSelectedItems();
            PumpOptionsDialog.Dock = DockStyle.Fill;
            ListViewItem PumpOptionsItem = new ListViewItem("Syringe Pumps", Instruments);
            PumpOptionsItem.Tag = PumpOptionsDialog;
            OptionsListView.Items.Add(PumpOptionsItem);
            OptionsPanel.Controls.Add(PumpOptionsDialog);

            #endregion

            OptionsListView.Columns[0].Width = -1; // Sets width to that of widest item.

            // Note OptionsChanged and ConfigChanged handlers are not yet bound.
            Config = config; // Refers to the global config object.
            SetChildConfig(Config); // Sets options dialogs to reference & match this config.

            #region Buttons
            FlowLayoutPanel ButtonPanel = new FlowLayoutPanel(); // Container for the buttons.
            ButtonPanel.FlowDirection = FlowDirection.RightToLeft;
            // Button panel will anchor to the bottom left of the whole control,
            // and be Z-ordered on top of the OptionsPanel.
            ButtonPanel.Anchor = ((System.Windows.Forms.AnchorStyles)
                        ((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            ButtonPanel.AutoSize = true; // Fit to the buttons.
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

            ButtonPanel.BringToFront(); // Display on top of any overlapping controls (OptionsPanel).
            #endregion

            ResumeLayout(false);
        }

        public void SetChildConfig(LuiConfig config)
        {
            // Set all options dialogs to reference & match the given config.
            foreach (ListViewItem it in OptionsListView.Items)
            {
                ((LuiOptionsDialog)it.Tag).Config = config;
            }
        }

        public void ChildrenMatchConfig(LuiConfig config)
        {
            // Set all options dialogs to match the given config.
            foreach (ListViewItem it in OptionsListView.Items)
            {
                ((LuiOptionsDialog)it.Tag).MatchConfig(config);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            foreach (ListViewItem item in OptionsListView.Items)
            {
                var luiOptionsDialog = (LuiOptionsDialog)item.Tag;
                // Set in OnLoad so initialization doesn't trigger the events.
                luiOptionsDialog.OptionsChanged += HandleCanApply;
                luiOptionsDialog.ConfigMatched += HandleCanApply;

                // Control initialization doesn't happen unless control is visible,
                // so we defer setting visibility until the control is loaded.
                luiOptionsDialog.Visible = false;
            }
            // Selecting the ListView causes selected item to be highlighted with system color.
            OptionsListView.Select();

            OptionsListView.Items[0].Selected = true; // Select default options dialog.
            base.OnLoad(e); // Forward to base class event handler.
        }

        private void HandleCanApply(object sender, EventArgs e)
        {
            ApplyConfig.Enabled = true; // Can apply after options changed
            SaveConfig.Enabled = false; // Can't save until new options applied.
        }

        private void ApplyConfig_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in OptionsListView.Items) ((LuiOptionsDialog)item.Tag).HandleApply(sender, e);
            OptionsApplied.Raise(this, EventArgs.Empty);
            ApplyConfig.Enabled = false; // Can't apply again until options change.
            SaveConfig.Enabled = true; // Can save config after apply.
        }

        private void SaveConfig_Click(object sender, EventArgs e)
        {
            Config.Save();
            SaveConfig.Enabled = false; // Can't save again until new changes made and applied.
        }

        private void LoadConfig_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();
            //ofd.RestoreDirectory = true;
            ofd.Filter = "XML Files|*.xml";
            ofd.FileName = "config.xml";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                // Update options dialogs to match the new config.
                // The new config is not instantiated and the old config is not replaced.
                ChildrenMatchConfig(LuiConfig.FromFile(ofd.FileName));
            }
        }

        private void HandleSelectedOptionsDialogChanged(object sender, EventArgs e)
        {
            // The SelectedIndexChanged event of the ListView will trigger twice:
            // once as the previous item is deselected, and again as the new item is selected.
            // We skip the first event call by checking for zero selected items.
            if (OptionsListView.SelectedItems.Count != 0)
            {
                if (ActiveDialog != null) ActiveDialog.Visible = false; // Hide previously active dialog.
                ListViewItem selectedItem = OptionsListView.SelectedItems[0];
                ActiveDialog = (LuiOptionsDialog)selectedItem.Tag; // Update the active dialog.

                if (ActiveDialog != null)
                {
                    ActiveDialog.Visible = true; // Show newly active dialog.
                    //ActiveDialog.OnSetActive(); // Hypothetical so far.
                }
            }
        }

    }
}

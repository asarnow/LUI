﻿using System;
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

namespace LUI.tabs
{
    public partial class OptionsControl : UserControl
    {
        ListView OptionsListView;

        LuiOptionsDialog ActiveDialog;

        public OptionsControl()
        {
            InitializeComponent();

            SuspendLayout();

            Panel OptionsPanel = new Panel();
            OptionsPanel.Dock = DockStyle.Fill;
            Controls.Add(OptionsPanel);

            Panel ListPanel = new Panel();
            ListPanel.Dock = DockStyle.Left;
            Controls.Add(ListPanel);

            OptionsListView = new ListView();
            OptionsListView.Dock = DockStyle.Fill;
            ListPanel.Controls.Add(OptionsListView);
            //Controls.Add(OptionsListView);
            OptionsListView.HideSelection = false;
            //OptionsListView.LabelWrap = false;
            OptionsListView.HeaderStyle = ColumnHeaderStyle.None;
            OptionsListView.Columns.Add(new ColumnHeader());
            OptionsListView.Columns[0].Width = OptionsListView.Width;
            OptionsListView.View = View.Details;
            OptionsListView.ShowGroups = true;
            OptionsListView.SelectedIndexChanged += SelectedOptionsDialogChanged;
                
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
            BeamFlagOptionsDialog.Dock = DockStyle.Fill;
            ListViewItem BeamFlagOptionsItem = new ListViewItem("Beam Flags", Instruments);
            BeamFlagOptionsItem.Tag = BeamFlagOptionsDialog;
            OptionsListView.Items.Add(BeamFlagOptionsItem);
            OptionsPanel.Controls.Add(BeamFlagOptionsDialog);

            LuiOptionsListDialog<ICamera, CameraParameters> CameraOptionsDialog = 
                new LuiOptionsListDialog<ICamera, CameraParameters>(OptionsPanel.Size, false);
            CameraOptionsDialog.Dock = DockStyle.Fill;
            ListViewItem CameraOptionsItem = new ListViewItem("Camera", Instruments);
            CameraOptionsItem.Tag = CameraOptionsDialog;
            OptionsListView.Items.Add(CameraOptionsItem);
            OptionsPanel.Controls.Add(CameraOptionsDialog);

            LuiOptionsListDialog<IGpibProvider, GpibProviderParameters> GPIBOptionsDialog =
                new LuiOptionsListDialog<IGpibProvider, GpibProviderParameters>(OptionsPanel.Size, false);
            GPIBOptionsDialog.Dock = DockStyle.Fill;
            GPIBOptionsDialog.AddConfigPanel(new NIConfigPanel());
            GPIBOptionsDialog.AddConfigPanel(new PrologixConfigPanel());
            ListViewItem GPIBOptionsItem = new ListViewItem("GPIB Controllers", Instruments);
            GPIBOptionsItem.Tag = GPIBOptionsDialog;
            OptionsListView.Items.Add(GPIBOptionsItem);
            OptionsPanel.Controls.Add(GPIBOptionsDialog);

            LuiOptionsListDialog<IDigitalDelayGenerator, DelayGeneratorParameters> DDGOptionsDialog = 
                new LuiOptionsListDialog<IDigitalDelayGenerator, DelayGeneratorParameters>(OptionsPanel.Size, false);
            DDGOptionsDialog.Dock = DockStyle.Fill;
            ListViewItem DDGOptionsItem = new ListViewItem("Digital Delay Generators", Instruments);
            DDGOptionsItem.Tag = DDGOptionsDialog;
            OptionsListView.Items.Add(DDGOptionsItem);
            OptionsPanel.Controls.Add(DDGOptionsDialog);

            OptionsListView.Items[0].Selected = true;

            ResumeLayout(false);
        }

        private void OnLoad(object sender, EventArgs e)
        {

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

    }
}
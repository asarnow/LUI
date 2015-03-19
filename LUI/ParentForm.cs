using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using lasercom;
using LUI.config;
using LUI.tabs;

namespace LUI
{
    /// <summary>
    /// Windows Form containing the entire LUI application.
    /// The TabControl's pages are populated with UserControls which handle
    /// the various features of the application.
    /// </summary>
    public partial class ParentForm : Form
    {
        LuiConfig Config;
        Commander Commander;

        public enum State { IDLE, TROS, CALIBRATE, ALIGN, POWER, RESIDUALS }

        TROSControl TROSControl;
        CalibrateControl CalibrateControl;
        LaserPowerControl LaserPowerControl;
        ResidualsControl ResidualsControl;
        OptionsControl OptionsControl;

        public State TaskBusy
        {
            get
            {
                if (ResidualsControl.IsBusy) return State.RESIDUALS;
                if (CalibrateControl.IsBusy) return State.CALIBRATE;
                //if (TROSControl.IsBusy) return State.TROS;
                if (LaserPowerControl.IsBusy) return State.POWER;
                else return State.IDLE;
            }
        }

        public ParentForm(LuiConfig Config)
        {
            InitializeComponent();

            if (Config.Ready)
            {
                Commander = Config.CreateCommander();
            }
            else
            {
                Commander = new Commander();
            }
            
            OptionsControl = new OptionsControl();
            OptionsControl.Size = OptionsPage.Size;
            OptionsPage.Controls.Add(OptionsControl);

            TROSControl = new TROSControl(Commander);
            TROSPage.Controls.Add(TROSControl);

            CalibrateControl = new CalibrateControl(Commander);
            CalibrationPage.Controls.Add(CalibrateControl);
            //Commander.CalibrationChanged += CalibrateControl.HandleCalibrationChanged;

            LaserPowerControl = new LaserPowerControl(Commander);
            PowerPage.Controls.Add(LaserPowerControl);
            //Commander.CalibrationChanged += LaserPowerControl.HandleCalibrationChanged;

            ResidualsControl = new ResidualsControl(Commander);
            ResidualsPage.Controls.Add(ResidualsControl);
            //Commander.CalibrationChanged += ResidualsControl.HandleCalibrationChanged;

            if (!Config.Ready)
            {
                Tabs.SelectedTab = OptionsPage;
            }
        }

        private static void MakeEmebeddable(Form Form)
        {
            Form.TopLevel = false;
            Form.Visible = true;
            Form.FormBorderStyle = FormBorderStyle.None;
            Form.Dock = DockStyle.Fill;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            Commander.Camera.Close();
            Commander.BeamFlags.CloseLaserAndFlash();
            Commander.BeamFlags.EnsurePortDisposed();
        }

    }
}

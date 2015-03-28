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
        //LuiConfig Config;
        Commander Commander;

        public enum State { IDLE, TROS, CALIBRATE, ALIGN, POWER, RESIDUALS }

        private TabControl Tabs;
        private TabPage TROSPage;
        private TabPage CalibrationPage;
        private TabPage HomePage;
        private TabPage SpecPage;
        private TabPage ResidualsPage;
        private TabPage PowerPage;
        private TabPage OptionsPage;

        private TROSControl TROSControl;
        private CalibrateControl CalibrateControl;
        private LaserPowerControl LaserPowerControl;
        private ResidualsControl ResidualsControl;
        private OptionsControl OptionsControl;

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
            SuspendLayout();

            // Dispose resources when the form is closed;
            FormClosed += (s,e) => Config.Dispose();

            AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1113, 691);
            Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            Name = "ParentForm";
            Text = "LUI";

            #region Setup tabs
            Tabs = new TabControl();
            Tabs.SuspendLayout();
            Tabs.Location = new System.Drawing.Point(0, 0);
            Tabs.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            Tabs.Name = "Tabs";
            Tabs.SelectedIndex = 0;
            //Tabs.Anchor = (AnchorStyles)(AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            Tabs.Dock = DockStyle.Fill;
            Tabs.TabIndex = 0;

            HomePage = new TabPage();
            SpecPage = new TabPage();
            TROSPage = new TabPage();
            ResidualsPage = new TabPage();
            CalibrationPage = new TabPage();
            PowerPage = new TabPage();
            OptionsPage = new TabPage();
            
            HomePage.BackColor = System.Drawing.SystemColors.Control;
            HomePage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            HomePage.Name = "HomePage";
            HomePage.TabIndex = 2;
            HomePage.Text = "Home";
            
            SpecPage.BackColor = System.Drawing.SystemColors.Control;
            SpecPage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            SpecPage.Name = "SpecPage";
            SpecPage.TabIndex = 3;
            SpecPage.Text = "Spectrum";
            
            TROSPage.BackColor = System.Drawing.SystemColors.Control;
            TROSPage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            TROSPage.Name = "TROSPage";
            TROSPage.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            TROSPage.TabIndex = 0;
            TROSPage.Text = "TROS";
            
            ResidualsPage.BackColor = System.Drawing.SystemColors.Control;
            ResidualsPage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            ResidualsPage.Name = "ResidualsPage";
            ResidualsPage.TabIndex = 4;
            ResidualsPage.Text = "Residuals";
            
            CalibrationPage.BackColor = System.Drawing.SystemColors.Control;
            CalibrationPage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            CalibrationPage.Name = "CalibrationPage";
            CalibrationPage.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            CalibrationPage.TabIndex = 1;
            CalibrationPage.Text = "Calibration";
            
            PowerPage.BackColor = System.Drawing.SystemColors.Control;
            PowerPage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            PowerPage.Name = "PowerPage";
            PowerPage.TabIndex = 5;
            PowerPage.Text = "Laser Power";

            OptionsPage.BackColor = System.Drawing.SystemColors.Control;
            OptionsPage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            OptionsPage.Name = "OptionsPage";
            OptionsPage.TabIndex = 7;
            OptionsPage.Text = "Options";
            
            Tabs.TabPages.Add(HomePage);
            Tabs.TabPages.Add(SpecPage);
            Tabs.TabPages.Add(TROSPage);
            Tabs.TabPages.Add(ResidualsPage);
            Tabs.TabPages.Add(CalibrationPage);
            Tabs.TabPages.Add(PowerPage);
            Tabs.TabPages.Add(OptionsPage);

            Controls.Add(Tabs);
            #endregion

            if (Config.Ready)
            {
                Commander = Config.CreateCommander();
            }
            else
            {
                Commander = new Commander();
            }

            OptionsControl = new OptionsControl(Config);
            OptionsControl.Dock = DockStyle.Fill;
            OptionsPage.Controls.Add(OptionsControl);

            TROSControl = new TROSControl(Commander);
            TROSPage.Controls.Add(TROSControl);

            CalibrateControl = new CalibrateControl(Commander);
            CalibrateControl.Dock = DockStyle.Fill;
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

            Tabs.ResumeLayout();
            ResumeLayout();
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
        }

    }
}

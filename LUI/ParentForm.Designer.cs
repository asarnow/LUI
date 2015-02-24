namespace LUI
{
    partial class ParentForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Tabs = new System.Windows.Forms.TabControl();
            this.HomePage = new System.Windows.Forms.TabPage();
            this.SpecPage = new System.Windows.Forms.TabPage();
            this.TROSPage = new System.Windows.Forms.TabPage();
            this.ResidualsPage = new System.Windows.Forms.TabPage();
            this.CalibrationPage = new System.Windows.Forms.TabPage();
            this.PowerPage = new System.Windows.Forms.TabPage();
            this.OptionsPage = new System.Windows.Forms.TabPage();
            this.Tabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.HomePage);
            this.Tabs.Controls.Add(this.SpecPage);
            this.Tabs.Controls.Add(this.TROSPage);
            this.Tabs.Controls.Add(this.ResidualsPage);
            this.Tabs.Controls.Add(this.CalibrationPage);
            this.Tabs.Controls.Add(this.PowerPage);
            this.Tabs.Controls.Add(this.OptionsPage);
            this.Tabs.Location = new System.Drawing.Point(0, 0);
            this.Tabs.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(1113, 691);
            this.Tabs.TabIndex = 0;
            // 
            // HomePage
            // 
            this.HomePage.BackColor = System.Drawing.SystemColors.Control;
            this.HomePage.Location = new System.Drawing.Point(4, 22);
            this.HomePage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.HomePage.Name = "HomePage";
            this.HomePage.Size = new System.Drawing.Size(1105, 665);
            this.HomePage.TabIndex = 2;
            this.HomePage.Text = "Home";
            // 
            // SpecPage
            // 
            this.SpecPage.BackColor = System.Drawing.SystemColors.Control;
            this.SpecPage.Location = new System.Drawing.Point(4, 22);
            this.SpecPage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.SpecPage.Name = "SpecPage";
            this.SpecPage.Size = new System.Drawing.Size(1105, 665);
            this.SpecPage.TabIndex = 3;
            this.SpecPage.Text = "Spectrum";
            // 
            // TROSPage
            // 
            this.TROSPage.BackColor = System.Drawing.SystemColors.Control;
            this.TROSPage.Location = new System.Drawing.Point(4, 22);
            this.TROSPage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TROSPage.Name = "TROSPage";
            this.TROSPage.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TROSPage.Size = new System.Drawing.Size(1105, 665);
            this.TROSPage.TabIndex = 0;
            this.TROSPage.Text = "TROS";
            // 
            // ResidualsPage
            // 
            this.ResidualsPage.BackColor = System.Drawing.SystemColors.Control;
            this.ResidualsPage.Location = new System.Drawing.Point(4, 22);
            this.ResidualsPage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.ResidualsPage.Name = "ResidualsPage";
            this.ResidualsPage.Size = new System.Drawing.Size(1105, 665);
            this.ResidualsPage.TabIndex = 4;
            this.ResidualsPage.Text = "Residuals";
            // 
            // CalibrationPage
            // 
            this.CalibrationPage.BackColor = System.Drawing.SystemColors.Control;
            this.CalibrationPage.Location = new System.Drawing.Point(4, 22);
            this.CalibrationPage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CalibrationPage.Name = "CalibrationPage";
            this.CalibrationPage.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.CalibrationPage.Size = new System.Drawing.Size(1105, 665);
            this.CalibrationPage.TabIndex = 1;
            this.CalibrationPage.Text = "Calibration";
            // 
            // PowerPage
            // 
            this.PowerPage.BackColor = System.Drawing.SystemColors.Control;
            this.PowerPage.Location = new System.Drawing.Point(4, 22);
            this.PowerPage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.PowerPage.Name = "PowerPage";
            this.PowerPage.Size = new System.Drawing.Size(1105, 665);
            this.PowerPage.TabIndex = 5;
            this.PowerPage.Text = "Laser Power";
            // 
            // OptionsPage
            // 
            this.OptionsPage.BackColor = System.Drawing.SystemColors.Control;
            this.OptionsPage.Location = new System.Drawing.Point(4, 22);
            this.OptionsPage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.OptionsPage.Name = "OptionsPage";
            this.OptionsPage.Size = new System.Drawing.Size(1105, 665);
            this.OptionsPage.TabIndex = 7;
            this.OptionsPage.Text = "Options";
            // 
            // ParentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1111, 688);
            this.Controls.Add(this.Tabs);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ParentForm";
            this.Text = "LUI";
            this.Tabs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage TROSPage;
        private System.Windows.Forms.TabPage CalibrationPage;
        private System.Windows.Forms.TabPage HomePage;
        private System.Windows.Forms.TabPage SpecPage;
        private System.Windows.Forms.TabPage ResidualsPage;
        private System.Windows.Forms.TabPage PowerPage;
        private System.Windows.Forms.TabPage OptionsPage;
    }
}
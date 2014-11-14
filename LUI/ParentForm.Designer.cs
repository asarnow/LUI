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
            this.TROSPage = new System.Windows.Forms.TabPage();
            this.CalibratePage = new System.Windows.Forms.TabPage();
            this.Tabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.TROSPage);
            this.Tabs.Controls.Add(this.CalibratePage);
            this.Tabs.Location = new System.Drawing.Point(0, 0);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(1479, 824);
            this.Tabs.TabIndex = 0;
            // 
            // TROSPage
            // 
            this.TROSPage.BackColor = System.Drawing.SystemColors.Control;
            this.TROSPage.Location = new System.Drawing.Point(4, 25);
            this.TROSPage.Name = "TROSPage";
            this.TROSPage.Padding = new System.Windows.Forms.Padding(3);
            this.TROSPage.Size = new System.Drawing.Size(1471, 795);
            this.TROSPage.TabIndex = 0;
            this.TROSPage.Text = "TROS";
            // 
            // CalibratePage
            // 
            this.CalibratePage.BackColor = System.Drawing.SystemColors.Control;
            this.CalibratePage.Location = new System.Drawing.Point(4, 25);
            this.CalibratePage.Name = "CalibratePage";
            this.CalibratePage.Padding = new System.Windows.Forms.Padding(3);
            this.CalibratePage.Size = new System.Drawing.Size(1471, 795);
            this.CalibratePage.TabIndex = 1;
            this.CalibratePage.Text = "Calibrate";
            // 
            // ParentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1476, 821);
            this.Controls.Add(this.Tabs);
            this.Name = "ParentForm";
            this.Text = "LUI";
            this.Tabs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage TROSPage;
        private System.Windows.Forms.TabPage CalibratePage;
    }
}
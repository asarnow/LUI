namespace LUI.tabs
{
    partial class CalibrateControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalibrateControl));
            this.RSquaredLabel = new LUI.controls.DisabledRichTextBox();
            this.InterceptLabel = new System.Windows.Forms.Label();
            this.SlopeLabel = new System.Windows.Forms.Label();
            this.SaveCal = new System.Windows.Forms.Button();
            this.Slope = new System.Windows.Forms.TextBox();
            this.Intercept = new System.Windows.Forms.TextBox();
            this.RSquared = new System.Windows.Forms.TextBox();
            this.RunCal = new System.Windows.Forms.Button();
            this.CalibrationListView = new System.Windows.Forms.DataGridView();
            this.Channel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Wavelength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RemoveCalItem = new System.Windows.Forms.Button();

            this.ParentPanel.SuspendLayout();
            this.StatusBox.SuspendLayout();
            this.CommandsBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NScan)).BeginInit();
            this.BeamFlagBox.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CalibrationListView)).BeginInit();
            
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ObjectSelector);
            this.panel1.Controls.Add(this.RSquaredLabel);
            this.panel1.Controls.Add(this.InterceptLabel);
            this.panel1.Controls.Add(this.SlopeLabel);
            this.panel1.Controls.Add(this.SaveCal);
            this.panel1.Controls.Add(this.Slope);
            this.panel1.Controls.Add(this.Intercept);
            this.panel1.Controls.Add(this.RSquared);
            this.panel1.Controls.Add(this.RunCal);
            this.panel1.Controls.Add(this.CalibrationListView);
            this.panel1.Controls.Add(this.RemoveCalItem);
            this.panel1.Location = new System.Drawing.Point(2, 432);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(824, 210);
            this.panel1.TabIndex = 7;
            // 
            // RSquaredLabel
            // 
            this.RSquaredLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RSquaredLabel.Location = new System.Drawing.Point(632, 13);
            this.RSquaredLabel.Name = "RSquaredLabel";
            this.RSquaredLabel.ReadOnly = true;
            this.RSquaredLabel.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.RSquaredLabel.Size = new System.Drawing.Size(34, 14);
            this.RSquaredLabel.TabIndex = 17;
            this.RSquaredLabel.Text = "R2";
            this.RSquaredLabel.WordWrap = false;
            // 
            // InterceptLabel
            // 
            this.InterceptLabel.AutoSize = true;
            this.InterceptLabel.Location = new System.Drawing.Point(629, 58);
            this.InterceptLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.InterceptLabel.Name = "InterceptLabel";
            this.InterceptLabel.Size = new System.Drawing.Size(49, 13);
            this.InterceptLabel.TabIndex = 15;
            this.InterceptLabel.Text = "Intercept";
            // 
            // SlopeLabel
            // 
            this.SlopeLabel.AutoSize = true;
            this.SlopeLabel.Location = new System.Drawing.Point(629, 36);
            this.SlopeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.SlopeLabel.Name = "SlopeLabel";
            this.SlopeLabel.Size = new System.Drawing.Size(34, 13);
            this.SlopeLabel.TabIndex = 14;
            this.SlopeLabel.Text = "Slope";
            // 
            // SaveCal
            // 
            this.SaveCal.Location = new System.Drawing.Point(478, 45);
            this.SaveCal.Name = "SaveCal";
            this.SaveCal.Size = new System.Drawing.Size(68, 28);
            this.SaveCal.TabIndex = 12;
            this.SaveCal.Text = "Save";
            this.SaveCal.UseVisualStyleBackColor = true;
            this.SaveCal.Click += new System.EventHandler(this.SaveCal_Click);
            // 
            // Slope
            // 
            this.Slope.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Slope.Location = new System.Drawing.Point(551, 34);
            this.Slope.Margin = new System.Windows.Forms.Padding(2);
            this.Slope.Name = "Slope";
            this.Slope.Size = new System.Drawing.Size(76, 20);
            this.Slope.TabIndex = 11;
            // 
            // Intercept
            // 
            this.Intercept.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Intercept.Location = new System.Drawing.Point(551, 57);
            this.Intercept.Margin = new System.Windows.Forms.Padding(2);
            this.Intercept.Name = "Intercept";
            this.Intercept.Size = new System.Drawing.Size(76, 20);
            this.Intercept.TabIndex = 10;
            // 
            // RSquared
            // 
            this.RSquared.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RSquared.Location = new System.Drawing.Point(551, 11);
            this.RSquared.Margin = new System.Windows.Forms.Padding(2);
            this.RSquared.Name = "RSquared";
            this.RSquared.Size = new System.Drawing.Size(76, 20);
            this.RSquared.TabIndex = 9;
            // 
            // RunCal
            // 
            this.RunCal.Location = new System.Drawing.Point(478, 11);
            this.RunCal.Name = "RunCal";
            this.RunCal.Size = new System.Drawing.Size(68, 28);
            this.RunCal.TabIndex = 8;
            this.RunCal.Text = "Calibrate";
            this.RunCal.UseVisualStyleBackColor = true;
            this.RunCal.Click += new System.EventHandler(this.RunCal_Click);
            // 
            // CalibrationListView
            // 
            this.CalibrationListView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.CalibrationListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CalibrationListView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Channel,
            this.Wavelength});
            this.CalibrationListView.Location = new System.Drawing.Point(3, 3);
            this.CalibrationListView.Name = "CalibrationListView";
            this.CalibrationListView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CalibrationListView.Size = new System.Drawing.Size(247, 204);
            this.CalibrationListView.TabIndex = 7;
            // 
            // Channel
            // 
            this.Channel.DataPropertyName = "Channel";
            this.Channel.HeaderText = "Channel";
            this.Channel.Name = "Channel";
            this.Channel.ReadOnly = true;
            // 
            // Wavelength
            // 
            this.Wavelength.DataPropertyName = "Wavelength";
            this.Wavelength.HeaderText = "Wavelength";
            this.Wavelength.Name = "Wavelength";
            // 
            // RemoveCalItem
            // 
            this.RemoveCalItem.Location = new System.Drawing.Point(256, 37);
            this.RemoveCalItem.Name = "RemoveCalItem";
            this.RemoveCalItem.Size = new System.Drawing.Size(68, 28);
            this.RemoveCalItem.TabIndex = 5;
            this.RemoveCalItem.Text = "Remove";
            this.RemoveCalItem.UseVisualStyleBackColor = true;
            this.RemoveCalItem.Click += new System.EventHandler(this.RemoveCalItem_Click);
            // 
            // CalibrateControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "CalibrateControl";
            this.Size = new System.Drawing.Size(1107, 667);
            this.ParentPanel.ResumeLayout(false);
            this.StatusBox.ResumeLayout(false);
            this.StatusBox.PerformLayout();
            this.CommandsBox.ResumeLayout(false);
            this.CommandsBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NScan)).EndInit();
            this.BeamFlagBox.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CalibrationListView)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Button RemoveCalItem;
        private System.Windows.Forms.DataGridView CalibrationListView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Channel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Wavelength;
        private System.Windows.Forms.TextBox RSquared;
        private System.Windows.Forms.Button RunCal;
        private System.Windows.Forms.Button SaveCal;
        private System.Windows.Forms.TextBox Slope;
        private System.Windows.Forms.TextBox Intercept;
        private System.Windows.Forms.Label InterceptLabel;
        private System.Windows.Forms.Label SlopeLabel;
        private controls.DisabledRichTextBox RSquaredLabel;
    }
}

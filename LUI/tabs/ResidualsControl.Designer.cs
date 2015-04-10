namespace LUI.tabs
{
    partial class ResidualsControl
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
            System.Windows.Forms.Label GainLabel;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label DiffSumLabel;
            System.Windows.Forms.Label NAverageLabel;
            System.Windows.Forms.Label TotalCountsLabel;
            System.Windows.Forms.Label PeakLabel;
            System.Windows.Forms.Label label2;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResidualsControl));

            this.CameraGain = new System.Windows.Forms.NumericUpDown();

            this.ShowDifference = new System.Windows.Forms.CheckBox();
            this.ShowLast = new System.Windows.Forms.CheckBox();
            this.PersistentGraphing = new System.Windows.Forms.CheckBox();
            this.SaveProfile = new System.Windows.Forms.Button();
            this.DiffSum = new System.Windows.Forms.TextBox();
            this.NAverage = new System.Windows.Forms.NumericUpDown();
            this.CountsN = new System.Windows.Forms.TextBox();
            this.PeakNLabel = new System.Windows.Forms.Label();
            this.PeakN = new System.Windows.Forms.TextBox();
            this.Counts = new System.Windows.Forms.TextBox();
            this.LoadProfile = new System.Windows.Forms.Button();
            this.Peak = new System.Windows.Forms.TextBox();

            GainLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            DiffSumLabel = new System.Windows.Forms.Label();
            NAverageLabel = new System.Windows.Forms.Label();
            TotalCountsLabel = new System.Windows.Forms.Label();
            PeakLabel = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            this.ParentPanel.SuspendLayout();
            this.StatusBox.SuspendLayout();
            this.CommandsBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NScan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CameraGain)).BeginInit();
            this.BeamFlagBox.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NAverage)).BeginInit();
            this.SuspendLayout();
            // 
            // GainLabel
            // 
            GainLabel.AutoSize = true;
            GainLabel.Location = new System.Drawing.Point(179, 169);
            GainLabel.Name = "GainLabel";
            GainLabel.Size = new System.Drawing.Size(68, 13);
            GainLabel.TabIndex = 6;
            GainLabel.Text = "Camera Gain";
            // 
            // DiffSumLabel
            // 
            DiffSumLabel.AutoSize = true;
            DiffSumLabel.Location = new System.Drawing.Point(149, 101);
            DiffSumLabel.Name = "DiffSumLabel";
            DiffSumLabel.Size = new System.Drawing.Size(92, 13);
            DiffSumLabel.TabIndex = 15;
            DiffSumLabel.Text = "Sum of Difference";
            // 
            // NAverageLabel
            // 
            NAverageLabel.AutoSize = true;
            NAverageLabel.Location = new System.Drawing.Point(625, 100);
            NAverageLabel.Name = "NAverageLabel";
            NAverageLabel.Size = new System.Drawing.Size(90, 13);
            NAverageLabel.TabIndex = 13;
            NAverageLabel.Text = "Points in Average";
            // 
            // TotalCountsLabel
            // 
            TotalCountsLabel.AutoSize = true;
            TotalCountsLabel.Location = new System.Drawing.Point(109, 75);
            TotalCountsLabel.Name = "TotalCountsLabel";
            TotalCountsLabel.Size = new System.Drawing.Size(67, 13);
            TotalCountsLabel.TabIndex = 8;
            TotalCountsLabel.Text = "Total Counts";
            // 
            // PeakLabel
            // 
            PeakLabel.AutoSize = true;
            PeakLabel.Location = new System.Drawing.Point(109, 47);
            PeakLabel.Name = "PeakLabel";
            PeakLabel.Size = new System.Drawing.Size(55, 13);
            PeakLabel.TabIndex = 7;
            PeakLabel.Text = "Peak Size";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(582, 75);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(118, 13);
            label2.TabIndex = 18;
            label2.Text = "(uncheck for alignment)";
            // 
            // CommandsBox
            // 
            this.CommandsBox.Controls.Add(GainLabel);
            this.CommandsBox.Controls.Add(this.CameraGain);
            // 
            // CameraGain
            // 
            this.CameraGain.Location = new System.Drawing.Point(114, 167);
            this.CameraGain.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.CameraGain.Name = "CameraGain";
            this.CameraGain.Size = new System.Drawing.Size(59, 20);
            this.CameraGain.TabIndex = 5;
            this.CameraGain.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.CameraGain.ValueChanged += new System.EventHandler(this.CameraGain_ValueChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ShowDifference);
            this.panel1.Controls.Add(this.ShowLast);
            this.panel1.Controls.Add(this.PersistentGraphing);
            this.panel1.Controls.Add(this.SaveProfile);
            this.panel1.Controls.Add(label2);
            this.panel1.Controls.Add(DiffSumLabel);
            this.panel1.Controls.Add(this.DiffSum);
            this.panel1.Controls.Add(NAverageLabel);
            this.panel1.Controls.Add(this.NAverage);
            this.panel1.Controls.Add(this.CountsN);
            this.panel1.Controls.Add(this.PeakNLabel);
            this.panel1.Controls.Add(this.PeakN);
            this.panel1.Controls.Add(TotalCountsLabel);
            this.panel1.Controls.Add(PeakLabel);
            this.panel1.Controls.Add(this.Counts);
            this.panel1.Controls.Add(this.LoadProfile);
            this.panel1.Controls.Add(this.Peak);
            // 
            // ShowDifference
            // 
            this.ShowDifference.AutoSize = true;
            this.ShowDifference.Checked = true;
            this.ShowDifference.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowDifference.Location = new System.Drawing.Point(582, 9);
            this.ShowDifference.Name = "ShowDifference";
            this.ShowDifference.Size = new System.Drawing.Size(105, 17);
            this.ShowDifference.TabIndex = 4;
            this.ShowDifference.Text = "Show Difference";
            this.ShowDifference.UseVisualStyleBackColor = true;
            this.ShowDifference.CheckedChanged += new System.EventHandler(this.ShowDifference_CheckedChanged);
            // 
            // ShowLast
            // 
            this.ShowLast.AutoSize = true;
            this.ShowLast.Checked = true;
            this.ShowLast.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowLast.Location = new System.Drawing.Point(582, 32);
            this.ShowLast.Name = "ShowLast";
            this.ShowLast.Size = new System.Drawing.Size(124, 17);
            this.ShowLast.TabIndex = 3;
            this.ShowLast.Text = "Show Loaded Profile";
            this.ShowLast.UseVisualStyleBackColor = true;
            this.ShowLast.CheckedChanged += new System.EventHandler(this.ShowLast_CheckedChanged);
            // 
            // PersistentGraphing
            // 
            this.PersistentGraphing.AutoSize = true;
            this.PersistentGraphing.Checked = true;
            this.PersistentGraphing.CheckState = System.Windows.Forms.CheckState.Checked;
            this.PersistentGraphing.Location = new System.Drawing.Point(582, 55);
            this.PersistentGraphing.Name = "PersistentGraphing";
            this.PersistentGraphing.Size = new System.Drawing.Size(118, 17);
            this.PersistentGraphing.TabIndex = 17;
            this.PersistentGraphing.Text = "Persistent Graphing";
            this.PersistentGraphing.UseVisualStyleBackColor = true;
            // 
            // SaveProfile
            // 
            this.SaveProfile.Location = new System.Drawing.Point(720, 34);
            this.SaveProfile.Margin = new System.Windows.Forms.Padding(2);
            this.SaveProfile.Name = "SaveProfile";
            this.SaveProfile.Size = new System.Drawing.Size(102, 28);
            this.SaveProfile.TabIndex = 16;
            this.SaveProfile.Text = "Save Profile";
            this.SaveProfile.UseVisualStyleBackColor = true;
            this.SaveProfile.Click += new System.EventHandler(this.SaveProfile_Click);
            // 
            // DiffSum
            // 
            this.DiffSum.Location = new System.Drawing.Point(43, 98);
            this.DiffSum.Name = "DiffSum";
            this.DiffSum.ReadOnly = true;
            this.DiffSum.Size = new System.Drawing.Size(100, 20);
            this.DiffSum.TabIndex = 14;
            this.DiffSum.Text = "0";
            this.DiffSum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // NAverage
            // 
            this.NAverage.Location = new System.Drawing.Point(583, 98);
            this.NAverage.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.NAverage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NAverage.Name = "NAverage";
            this.NAverage.Size = new System.Drawing.Size(36, 20);
            this.NAverage.TabIndex = 12;
            this.NAverage.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.NAverage.ValueChanged += new System.EventHandler(this.NAverage_ValueChanged);
            // 
            // CountsN
            // 
            this.CountsN.Location = new System.Drawing.Point(182, 72);
            this.CountsN.Name = "CountsN";
            this.CountsN.ReadOnly = true;
            this.CountsN.Size = new System.Drawing.Size(100, 20);
            this.CountsN.TabIndex = 11;
            this.CountsN.Text = "0";
            this.CountsN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PeakNLabel
            // 
            this.PeakNLabel.AutoSize = true;
            this.PeakNLabel.Location = new System.Drawing.Point(288, 47);
            this.PeakNLabel.Name = "PeakNLabel";
            this.PeakNLabel.Size = new System.Drawing.Size(83, 13);
            this.PeakNLabel.TabIndex = 10;
            this.PeakNLabel.Text = "5 Point Average";
            // 
            // PeakN
            // 
            this.PeakN.Location = new System.Drawing.Point(182, 44);
            this.PeakN.Name = "PeakN";
            this.PeakN.ReadOnly = true;
            this.PeakN.Size = new System.Drawing.Size(100, 20);
            this.PeakN.TabIndex = 9;
            this.PeakN.Text = "0";
            this.PeakN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Counts
            // 
            this.Counts.Location = new System.Drawing.Point(3, 72);
            this.Counts.Name = "Counts";
            this.Counts.ReadOnly = true;
            this.Counts.Size = new System.Drawing.Size(100, 20);
            this.Counts.TabIndex = 6;
            this.Counts.Text = "0";
            this.Counts.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LoadProfile
            // 
            this.LoadProfile.Location = new System.Drawing.Point(720, 2);
            this.LoadProfile.Margin = new System.Windows.Forms.Padding(2);
            this.LoadProfile.Name = "LoadProfile";
            this.LoadProfile.Size = new System.Drawing.Size(102, 28);
            this.LoadProfile.TabIndex = 2;
            this.LoadProfile.Text = "Load Profile";
            this.LoadProfile.UseVisualStyleBackColor = true;
            this.LoadProfile.Click += new System.EventHandler(this.LoadProfile_Click);
            // 
            // Peak
            // 
            this.Peak.Location = new System.Drawing.Point(3, 44);
            this.Peak.Name = "Peak";
            this.Peak.ReadOnly = true;
            this.Peak.Size = new System.Drawing.Size(100, 20);
            this.Peak.TabIndex = 0;
            this.Peak.Text = "0";
            this.Peak.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ResidualsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ResidualsControl";
            this.Size = new System.Drawing.Size(1107, 667);
            this.ParentPanel.ResumeLayout(false);
            this.StatusBox.ResumeLayout(false);
            this.StatusBox.PerformLayout();
            this.CommandsBox.ResumeLayout(false);
            this.CommandsBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NScan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NScan)).EndInit();
            this.BeamFlagBox.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NAverage)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TextBox Peak;
        private System.Windows.Forms.Button LoadProfile;
        private System.Windows.Forms.CheckBox ShowDifference;
        private System.Windows.Forms.CheckBox ShowLast;
        private System.Windows.Forms.TextBox Counts;
        private System.Windows.Forms.TextBox PeakN;
        private System.Windows.Forms.TextBox CountsN;
        private System.Windows.Forms.Label PeakNLabel;
        private System.Windows.Forms.NumericUpDown NAverage;
        private System.Windows.Forms.NumericUpDown CameraGain;
        private System.Windows.Forms.TextBox DiffSum;
        private System.Windows.Forms.Button SaveProfile;
        private System.Windows.Forms.CheckBox PersistentGraphing;
    }
}

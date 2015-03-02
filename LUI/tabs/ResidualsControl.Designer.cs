namespace LUI.controls
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.StatusBox = new System.Windows.Forms.GroupBox();
            this.ProgressLabel = new System.Windows.Forms.Label();
            this.StatusProgress = new System.Windows.Forms.ProgressBar();
            this.CommandsBox = new System.Windows.Forms.GroupBox();
            this.CameraGain = new System.Windows.Forms.NumericUpDown();
            this.Clear = new System.Windows.Forms.Button();
            this.NScan = new System.Windows.Forms.NumericUpDown();
            this.Collect = new System.Windows.Forms.Button();
            this.Abort = new System.Windows.Forms.Button();
            this.BeamFlagBox = new System.Windows.Forms.GroupBox();
            this.CloseLamp = new System.Windows.Forms.Button();
            this.OpenLamp = new System.Windows.Forms.Button();
            this.CloseLaser = new System.Windows.Forms.Button();
            this.OpenLaser = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PersistentGraphing = new System.Windows.Forms.CheckBox();
            this.SaveProfile = new System.Windows.Forms.Button();
            this.DiffSum = new System.Windows.Forms.TextBox();
            this.NAverage = new System.Windows.Forms.NumericUpDown();
            this.CountsN = new System.Windows.Forms.TextBox();
            this.PeakNLabel = new System.Windows.Forms.Label();
            this.PeakN = new System.Windows.Forms.TextBox();
            this.Counts = new System.Windows.Forms.TextBox();
            this.ShowDifference = new System.Windows.Forms.CheckBox();
            this.ShowLast = new System.Windows.Forms.CheckBox();
            this.LoadProfile = new System.Windows.Forms.Button();
            this.Peak = new System.Windows.Forms.TextBox();
            this.Graph = new LUI.controls.GraphControl();
            GainLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            DiffSumLabel = new System.Windows.Forms.Label();
            NAverageLabel = new System.Windows.Forms.Label();
            TotalCountsLabel = new System.Windows.Forms.Label();
            PeakLabel = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.StatusBox.SuspendLayout();
            this.CommandsBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.NScan)).BeginInit();
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
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(155, 26);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(37, 13);
            label1.TabIndex = 4;
            label1.Text = "Scans";
            // 
            // DiffSumLabel
            // 
            DiffSumLabel.AutoSize = true;
            DiffSumLabel.Location = new System.Drawing.Point(202, 139);
            DiffSumLabel.Name = "DiffSumLabel";
            DiffSumLabel.Size = new System.Drawing.Size(92, 13);
            DiffSumLabel.TabIndex = 15;
            DiffSumLabel.Text = "Sum of Difference";
            // 
            // NAverageLabel
            // 
            NAverageLabel.AutoSize = true;
            NAverageLabel.Location = new System.Drawing.Point(542, 62);
            NAverageLabel.Name = "NAverageLabel";
            NAverageLabel.Size = new System.Drawing.Size(90, 13);
            NAverageLabel.TabIndex = 13;
            NAverageLabel.Text = "Points in Average";
            // 
            // TotalCountsLabel
            // 
            TotalCountsLabel.AutoSize = true;
            TotalCountsLabel.Location = new System.Drawing.Point(141, 56);
            TotalCountsLabel.Name = "TotalCountsLabel";
            TotalCountsLabel.Size = new System.Drawing.Size(67, 13);
            TotalCountsLabel.TabIndex = 8;
            TotalCountsLabel.Text = "Total Counts";
            // 
            // PeakLabel
            // 
            PeakLabel.AutoSize = true;
            PeakLabel.Location = new System.Drawing.Point(141, 28);
            PeakLabel.Name = "PeakLabel";
            PeakLabel.Size = new System.Drawing.Size(55, 13);
            PeakLabel.TabIndex = 7;
            PeakLabel.Text = "Peak Size";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(647, 149);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(118, 13);
            label2.TabIndex = 18;
            label2.Text = "(uncheck for alignment)";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.StatusBox, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.CommandsBox, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.BeamFlagBox, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.Graph, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1107, 667);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // StatusBox
            // 
            this.StatusBox.Controls.Add(this.ProgressLabel);
            this.StatusBox.Controls.Add(this.StatusProgress);
            this.StatusBox.Location = new System.Drawing.Point(831, 3);
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.Size = new System.Drawing.Size(271, 209);
            this.StatusBox.TabIndex = 2;
            this.StatusBox.TabStop = false;
            this.StatusBox.Text = "Status";
            // 
            // ProgressLabel
            // 
            this.ProgressLabel.AutoSize = true;
            this.ProgressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProgressLabel.Location = new System.Drawing.Point(49, 81);
            this.ProgressLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ProgressLabel.Name = "ProgressLabel";
            this.ProgressLabel.Size = new System.Drawing.Size(34, 17);
            this.ProgressLabel.TabIndex = 7;
            this.ProgressLabel.Text = "Idle";
            // 
            // StatusProgress
            // 
            this.StatusProgress.Location = new System.Drawing.Point(52, 100);
            this.StatusProgress.Margin = new System.Windows.Forms.Padding(2);
            this.StatusProgress.Name = "StatusProgress";
            this.StatusProgress.Size = new System.Drawing.Size(170, 28);
            this.StatusProgress.TabIndex = 6;
            // 
            // CommandsBox
            // 
            this.CommandsBox.Controls.Add(GainLabel);
            this.CommandsBox.Controls.Add(this.CameraGain);
            this.CommandsBox.Controls.Add(this.Clear);
            this.CommandsBox.Controls.Add(label1);
            this.CommandsBox.Controls.Add(this.NScan);
            this.CommandsBox.Controls.Add(this.Collect);
            this.CommandsBox.Controls.Add(this.Abort);
            this.CommandsBox.Location = new System.Drawing.Point(831, 218);
            this.CommandsBox.Name = "CommandsBox";
            this.CommandsBox.Size = new System.Drawing.Size(271, 209);
            this.CommandsBox.TabIndex = 3;
            this.CommandsBox.TabStop = false;
            this.CommandsBox.Text = "Commands";
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
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(5, 121);
            this.Clear.Margin = new System.Windows.Forms.Padding(2);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(102, 28);
            this.Clear.TabIndex = 1;
            this.Clear.Text = "Clear Graph";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // NScan
            // 
            this.NScan.Location = new System.Drawing.Point(113, 24);
            this.NScan.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.NScan.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NScan.Name = "NScan";
            this.NScan.Size = new System.Drawing.Size(36, 20);
            this.NScan.TabIndex = 3;
            this.NScan.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // Collect
            // 
            this.Collect.Location = new System.Drawing.Point(6, 19);
            this.Collect.Name = "Collect";
            this.Collect.Size = new System.Drawing.Size(102, 28);
            this.Collect.TabIndex = 2;
            this.Collect.Text = "Collect UV/vis";
            this.Collect.UseVisualStyleBackColor = true;
            this.Collect.Click += new System.EventHandler(this.Collect_Click);
            // 
            // Abort
            // 
            this.Abort.Enabled = false;
            this.Abort.Location = new System.Drawing.Point(6, 53);
            this.Abort.Name = "Abort";
            this.Abort.Size = new System.Drawing.Size(68, 28);
            this.Abort.TabIndex = 1;
            this.Abort.Text = "Abort";
            this.Abort.UseVisualStyleBackColor = true;
            // 
            // BeamFlagBox
            // 
            this.BeamFlagBox.Controls.Add(this.CloseLamp);
            this.BeamFlagBox.Controls.Add(this.OpenLamp);
            this.BeamFlagBox.Controls.Add(this.CloseLaser);
            this.BeamFlagBox.Controls.Add(this.OpenLaser);
            this.BeamFlagBox.Location = new System.Drawing.Point(831, 433);
            this.BeamFlagBox.Name = "BeamFlagBox";
            this.BeamFlagBox.Size = new System.Drawing.Size(271, 87);
            this.BeamFlagBox.TabIndex = 5;
            this.BeamFlagBox.TabStop = false;
            this.BeamFlagBox.Text = "Beam Flags";
            // 
            // CloseLamp
            // 
            this.CloseLamp.Location = new System.Drawing.Point(114, 53);
            this.CloseLamp.Name = "CloseLamp";
            this.CloseLamp.Size = new System.Drawing.Size(102, 28);
            this.CloseLamp.TabIndex = 7;
            this.CloseLamp.Text = "Close Lamp";
            this.CloseLamp.UseVisualStyleBackColor = true;
            this.CloseLamp.Click += new System.EventHandler(this.CloseLamp_Click);
            // 
            // OpenLamp
            // 
            this.OpenLamp.Location = new System.Drawing.Point(114, 19);
            this.OpenLamp.Name = "OpenLamp";
            this.OpenLamp.Size = new System.Drawing.Size(102, 28);
            this.OpenLamp.TabIndex = 6;
            this.OpenLamp.Text = "Open Lamp";
            this.OpenLamp.UseVisualStyleBackColor = true;
            this.OpenLamp.Click += new System.EventHandler(this.OpenLamp_Click);
            // 
            // CloseLaser
            // 
            this.CloseLaser.Location = new System.Drawing.Point(6, 53);
            this.CloseLaser.Name = "CloseLaser";
            this.CloseLaser.Size = new System.Drawing.Size(102, 28);
            this.CloseLaser.TabIndex = 5;
            this.CloseLaser.Text = "Close Laser";
            this.CloseLaser.UseVisualStyleBackColor = true;
            this.CloseLaser.Click += new System.EventHandler(this.CloseLaser_Click);
            // 
            // OpenLaser
            // 
            this.OpenLaser.Location = new System.Drawing.Point(6, 19);
            this.OpenLaser.Name = "OpenLaser";
            this.OpenLaser.Size = new System.Drawing.Size(102, 28);
            this.OpenLaser.TabIndex = 4;
            this.OpenLaser.Text = "Open Laser";
            this.OpenLaser.UseVisualStyleBackColor = true;
            this.OpenLaser.Click += new System.EventHandler(this.OpenLaser_Click);
            // 
            // panel1
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.panel1, 3);
            this.panel1.Controls.Add(label2);
            this.panel1.Controls.Add(this.PersistentGraphing);
            this.panel1.Controls.Add(this.SaveProfile);
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
            this.panel1.Controls.Add(this.ShowDifference);
            this.panel1.Controls.Add(this.ShowLast);
            this.panel1.Controls.Add(this.LoadProfile);
            this.panel1.Controls.Add(this.Peak);
            this.panel1.Location = new System.Drawing.Point(2, 432);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(824, 210);
            this.panel1.TabIndex = 7;
            // 
            // PersistentGraphing
            // 
            this.PersistentGraphing.AutoSize = true;
            this.PersistentGraphing.Checked = true;
            this.PersistentGraphing.CheckState = System.Windows.Forms.CheckState.Checked;
            this.PersistentGraphing.Location = new System.Drawing.Point(641, 129);
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
            this.DiffSum.Location = new System.Drawing.Point(96, 136);
            this.DiffSum.Name = "DiffSum";
            this.DiffSum.ReadOnly = true;
            this.DiffSum.Size = new System.Drawing.Size(100, 20);
            this.DiffSum.TabIndex = 14;
            this.DiffSum.Text = "0";
            this.DiffSum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // NAverage
            // 
            this.NAverage.Location = new System.Drawing.Point(500, 60);
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
            this.CountsN.Location = new System.Drawing.Point(240, 49);
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
            this.PeakNLabel.Location = new System.Drawing.Point(346, 28);
            this.PeakNLabel.Name = "PeakNLabel";
            this.PeakNLabel.Size = new System.Drawing.Size(83, 13);
            this.PeakNLabel.TabIndex = 10;
            this.PeakNLabel.Text = "5 Point Average";
            // 
            // PeakN
            // 
            this.PeakN.Location = new System.Drawing.Point(240, 25);
            this.PeakN.Name = "PeakN";
            this.PeakN.ReadOnly = true;
            this.PeakN.Size = new System.Drawing.Size(100, 20);
            this.PeakN.TabIndex = 9;
            this.PeakN.Text = "0";
            this.PeakN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Counts
            // 
            this.Counts.Location = new System.Drawing.Point(35, 53);
            this.Counts.Name = "Counts";
            this.Counts.ReadOnly = true;
            this.Counts.Size = new System.Drawing.Size(100, 20);
            this.Counts.TabIndex = 6;
            this.Counts.Text = "0";
            this.Counts.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ShowDifference
            // 
            this.ShowDifference.AutoSize = true;
            this.ShowDifference.Checked = true;
            this.ShowDifference.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowDifference.Location = new System.Drawing.Point(641, 106);
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
            this.ShowLast.Location = new System.Drawing.Point(641, 83);
            this.ShowLast.Name = "ShowLast";
            this.ShowLast.Size = new System.Drawing.Size(124, 17);
            this.ShowLast.TabIndex = 3;
            this.ShowLast.Text = "Show Loaded Profile";
            this.ShowLast.UseVisualStyleBackColor = true;
            this.ShowLast.CheckedChanged += new System.EventHandler(this.ShowLast_CheckedChanged);
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
            this.Peak.Location = new System.Drawing.Point(35, 25);
            this.Peak.Name = "Peak";
            this.Peak.ReadOnly = true;
            this.Peak.Size = new System.Drawing.Size(100, 20);
            this.Peak.TabIndex = 0;
            this.Peak.Text = "0";
            this.Peak.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Graph
            // 
            this.Graph.AxesColor = System.Drawing.Color.Blue;
            this.Graph.AxesFont = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Graph.AxesFontColor = System.Drawing.Color.Black;
            this.Graph.ColorOrder = ((System.Collections.Generic.List<System.Drawing.Color>)(resources.GetObject("Graph.ColorOrder")));
            this.tableLayoutPanel1.SetColumnSpan(this.Graph, 3);
            this.Graph.InitialScaleHeight = 0F;
            this.Graph.InitialXRight = 1023F;
            this.Graph.InitialXLeft = 0F;
            this.Graph.InitialYMax = float.NegativeInfinity;
            this.Graph.InitialYMin = float.PositiveInfinity;
            this.Graph.Location = new System.Drawing.Point(2, 2);
            this.Graph.Margin = new System.Windows.Forms.Padding(2);
            this.Graph.Marker = "*";
            this.Graph.MarkerColor = System.Drawing.Color.Blue;
            this.Graph.MarkerFont = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Graph.Name = "Graph";
            this.Graph.NXTicks = 10;
            this.Graph.NYTicks = 10;
            this.Graph.PadBottom = 0.01F;
            this.Graph.PadLeft = 0.01F;
            this.Graph.PadRight = 0.01F;
            this.Graph.PadTop = 0.01F;
            this.tableLayoutPanel1.SetRowSpan(this.Graph, 2);
            this.Graph.ScaleHeight = 0F;
            this.Graph.Size = new System.Drawing.Size(824, 426);
            this.Graph.TabIndex = 8;
            this.Graph.XAxisHeight = 0.1F;
            this.Graph.XLabelFormat = "f0";
            this.Graph.XRight = 1023F;
            this.Graph.XLeft = 0F;
            this.Graph.YAxisWidth = 0.05F;
            this.Graph.YLabelFormat = "n3";
            this.Graph.YMax = float.NegativeInfinity;
            this.Graph.YMin = float.PositiveInfinity;
            // 
            // ResidualsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "ResidualsControl";
            this.Size = new System.Drawing.Size(1107, 667);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.StatusBox.ResumeLayout(false);
            this.StatusBox.PerformLayout();
            this.CommandsBox.ResumeLayout(false);
            this.CommandsBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.NScan)).EndInit();
            this.BeamFlagBox.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NAverage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.GroupBox StatusBox;
        private System.Windows.Forms.GroupBox CommandsBox;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.NumericUpDown NScan;
        private System.Windows.Forms.Button Collect;
        private System.Windows.Forms.Button Abort;
        private System.Windows.Forms.GroupBox BeamFlagBox;
        private System.Windows.Forms.Button CloseLamp;
        private System.Windows.Forms.Button OpenLamp;
        private System.Windows.Forms.Button CloseLaser;
        private System.Windows.Forms.Button OpenLaser;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label ProgressLabel;
        private System.Windows.Forms.ProgressBar StatusProgress;
        private controls.GraphControl Graph;
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

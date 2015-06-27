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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label DiffSumLabel;
            System.Windows.Forms.Label NAverageLabel;
            System.Windows.Forms.Label TotalCountsLabel;
            System.Windows.Forms.Label PeakLabel;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            this.ShowDifference = new System.Windows.Forms.CheckBox();
            this.ShowLast = new System.Windows.Forms.CheckBox();
            this.PersistentGraphing = new System.Windows.Forms.CheckBox();
            this.SaveProfile = new System.Windows.Forms.Button();
            this.DiffSum = new System.Windows.Forms.TextBox();
            this.NAverage = new System.Windows.Forms.NumericUpDown();
            this.CountsN = new System.Windows.Forms.TextBox();
            this.PeakN = new System.Windows.Forms.TextBox();
            this.Counts = new System.Windows.Forms.TextBox();
            this.LoadProfile = new System.Windows.Forms.Button();
            this.Peak = new System.Windows.Forms.TextBox();
            this.CollectLaser = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.DdgConfigBox = new LUI.controls.DdgCommandPanel();
            this.OptionsBox = new System.Windows.Forms.GroupBox();
            this.GraphScroll = new System.Windows.Forms.VScrollBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ImageMode = new System.Windows.Forms.RadioButton();
            this.FvbMode = new System.Windows.Forms.RadioButton();
            this.CameraExtras = new System.Windows.Forms.GroupBox();
            this.ScrollTip = new System.Windows.Forms.ToolTip(this.components);
            label1 = new System.Windows.Forms.Label();
            DiffSumLabel = new System.Windows.Forms.Label();
            NAverageLabel = new System.Windows.Forms.Label();
            TotalCountsLabel = new System.Windows.Forms.Label();
            PeakLabel = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            this.ParentPanel.SuspendLayout();
            this.StatusBox.SuspendLayout();
            this.CommandsBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NScan)).BeginInit();
            this.CommonObjectPanel.SuspendLayout();
            this.LeftChildArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraGain)).BeginInit();
            this.RightChildArea.SuspendLayout();
            this.LeftPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NAverage)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.OptionsBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.CameraExtras.SuspendLayout();
            this.SuspendLayout();
            // 
            // Graph
            // 
            this.Graph.Size = new System.Drawing.Size(1159, 543);
            this.Graph.XLeft = 1F;
            this.Graph.XRight = 1024F;
            // 
            // LeftChildArea
            // 
            this.LeftChildArea.Controls.Add(this.OptionsBox);
            this.LeftChildArea.Controls.Add(this.DdgConfigBox);
            this.LeftChildArea.Controls.Add(this.groupBox1);
            this.LeftChildArea.Controls.Add(this.SaveProfile);
            this.LeftChildArea.Controls.Add(this.LoadProfile);
            this.LeftChildArea.Location = new System.Drawing.Point(0, 543);
            this.LeftChildArea.Size = new System.Drawing.Size(1176, 278);
            // 
            // RightChildArea
            // 
            this.RightChildArea.Controls.Add(this.CameraExtras);
            // 
            // LeftPanel
            // 
            this.LeftPanel.Controls.Add(this.GraphScroll);
            this.LeftPanel.Controls.SetChildIndex(this.LeftChildArea, 0);
            this.LeftPanel.Controls.SetChildIndex(this.GraphScroll, 0);
            this.LeftPanel.Controls.SetChildIndex(this.Graph, 0);
            // 
            // label1
            // 
            label1.Location = new System.Drawing.Point(0, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(100, 23);
            label1.TabIndex = 0;
            // 
            // DiffSumLabel
            // 
            DiffSumLabel.AutoSize = true;
            DiffSumLabel.Location = new System.Drawing.Point(6, 110);
            DiffSumLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            DiffSumLabel.Name = "DiffSumLabel";
            DiffSumLabel.Size = new System.Drawing.Size(92, 13);
            DiffSumLabel.TabIndex = 15;
            DiffSumLabel.Text = "Sum of Difference";
            // 
            // NAverageLabel
            // 
            NAverageLabel.AutoSize = true;
            NAverageLabel.Location = new System.Drawing.Point(63, 81);
            NAverageLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            NAverageLabel.Name = "NAverageLabel";
            NAverageLabel.Size = new System.Drawing.Size(99, 13);
            NAverageLabel.TabIndex = 13;
            NAverageLabel.Text = "Local Average Size";
            // 
            // TotalCountsLabel
            // 
            TotalCountsLabel.AutoSize = true;
            TotalCountsLabel.Location = new System.Drawing.Point(142, 16);
            TotalCountsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            TotalCountsLabel.Name = "TotalCountsLabel";
            TotalCountsLabel.Size = new System.Drawing.Size(67, 13);
            TotalCountsLabel.TabIndex = 8;
            TotalCountsLabel.Text = "Total Counts";
            // 
            // PeakLabel
            // 
            PeakLabel.AutoSize = true;
            PeakLabel.Location = new System.Drawing.Point(332, 16);
            PeakLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            PeakLabel.Name = "PeakLabel";
            PeakLabel.Size = new System.Drawing.Size(55, 13);
            PeakLabel.TabIndex = 7;
            PeakLabel.Text = "Peak Size";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(6, 71);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(80, 13);
            label2.TabIndex = 16;
            label2.Text = "Global Average";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(6, 36);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(76, 13);
            label3.TabIndex = 17;
            label3.Text = "Local Average";
            // 
            // ShowDifference
            // 
            this.ShowDifference.AutoSize = true;
            this.ShowDifference.Checked = true;
            this.ShowDifference.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowDifference.Location = new System.Drawing.Point(7, 22);
            this.ShowDifference.Margin = new System.Windows.Forms.Padding(4);
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
            this.ShowLast.Location = new System.Drawing.Point(7, 50);
            this.ShowLast.Margin = new System.Windows.Forms.Padding(4);
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
            this.PersistentGraphing.Location = new System.Drawing.Point(174, 22);
            this.PersistentGraphing.Margin = new System.Windows.Forms.Padding(4);
            this.PersistentGraphing.Name = "PersistentGraphing";
            this.PersistentGraphing.Size = new System.Drawing.Size(118, 17);
            this.PersistentGraphing.TabIndex = 17;
            this.PersistentGraphing.Text = "Persistent Graphing";
            this.PersistentGraphing.UseVisualStyleBackColor = true;
            // 
            // SaveProfile
            // 
            this.SaveProfile.Location = new System.Drawing.Point(960, 42);
            this.SaveProfile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SaveProfile.Name = "SaveProfile";
            this.SaveProfile.Size = new System.Drawing.Size(136, 34);
            this.SaveProfile.TabIndex = 16;
            this.SaveProfile.Text = "Save Profile";
            this.SaveProfile.UseVisualStyleBackColor = true;
            this.SaveProfile.Click += new System.EventHandler(this.SaveProfile_Click);
            // 
            // DiffSum
            // 
            this.DiffSum.Location = new System.Drawing.Point(106, 107);
            this.DiffSum.Margin = new System.Windows.Forms.Padding(4);
            this.DiffSum.Name = "DiffSum";
            this.DiffSum.ReadOnly = true;
            this.DiffSum.Size = new System.Drawing.Size(132, 20);
            this.DiffSum.TabIndex = 14;
            this.DiffSum.Text = "0";
            this.DiffSum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // NAverage
            // 
            this.NAverage.Location = new System.Drawing.Point(7, 79);
            this.NAverage.Margin = new System.Windows.Forms.Padding(4);
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
            this.NAverage.Size = new System.Drawing.Size(48, 20);
            this.NAverage.TabIndex = 12;
            this.NAverage.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // CountsN
            // 
            this.CountsN.Location = new System.Drawing.Point(89, 33);
            this.CountsN.Margin = new System.Windows.Forms.Padding(4);
            this.CountsN.Name = "CountsN";
            this.CountsN.ReadOnly = true;
            this.CountsN.Size = new System.Drawing.Size(174, 20);
            this.CountsN.TabIndex = 11;
            this.CountsN.Text = "0";
            this.CountsN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PeakN
            // 
            this.PeakN.Location = new System.Drawing.Point(271, 33);
            this.PeakN.Margin = new System.Windows.Forms.Padding(4);
            this.PeakN.Name = "PeakN";
            this.PeakN.ReadOnly = true;
            this.PeakN.Size = new System.Drawing.Size(174, 20);
            this.PeakN.TabIndex = 9;
            this.PeakN.Text = "0";
            this.PeakN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Counts
            // 
            this.Counts.Location = new System.Drawing.Point(89, 68);
            this.Counts.Margin = new System.Windows.Forms.Padding(4);
            this.Counts.Name = "Counts";
            this.Counts.ReadOnly = true;
            this.Counts.Size = new System.Drawing.Size(174, 20);
            this.Counts.TabIndex = 6;
            this.Counts.Text = "0";
            this.Counts.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LoadProfile
            // 
            this.LoadProfile.Location = new System.Drawing.Point(960, 2);
            this.LoadProfile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LoadProfile.Name = "LoadProfile";
            this.LoadProfile.Size = new System.Drawing.Size(136, 34);
            this.LoadProfile.TabIndex = 2;
            this.LoadProfile.Text = "Load Profile";
            this.LoadProfile.UseVisualStyleBackColor = true;
            this.LoadProfile.Click += new System.EventHandler(this.LoadProfile_Click);
            // 
            // Peak
            // 
            this.Peak.Location = new System.Drawing.Point(271, 68);
            this.Peak.Margin = new System.Windows.Forms.Padding(4);
            this.Peak.Name = "Peak";
            this.Peak.ReadOnly = true;
            this.Peak.Size = new System.Drawing.Size(174, 20);
            this.Peak.TabIndex = 0;
            this.Peak.Text = "0";
            this.Peak.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CollectLaser
            // 
            this.CollectLaser.AutoSize = true;
            this.CollectLaser.Location = new System.Drawing.Point(174, 50);
            this.CollectLaser.Name = "CollectLaser";
            this.CollectLaser.Size = new System.Drawing.Size(124, 17);
            this.CollectLaser.TabIndex = 19;
            this.CollectLaser.Text = "Collect Laser Scatter";
            this.CollectLaser.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSize = true;
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(label3);
            this.groupBox1.Controls.Add(label2);
            this.groupBox1.Controls.Add(this.Peak);
            this.groupBox1.Controls.Add(this.Counts);
            this.groupBox1.Controls.Add(PeakLabel);
            this.groupBox1.Controls.Add(TotalCountsLabel);
            this.groupBox1.Controls.Add(this.PeakN);
            this.groupBox1.Controls.Add(this.CountsN);
            this.groupBox1.Controls.Add(this.DiffSum);
            this.groupBox1.Controls.Add(DiffSumLabel);
            this.groupBox1.Location = new System.Drawing.Point(370, 128);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(452, 147);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Statistics";
            // 
            // DdgConfigBox
            // 
            this.DdgConfigBox.AutoSize = true;
            this.DdgConfigBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DdgConfigBox.Location = new System.Drawing.Point(3, 3);
            this.DdgConfigBox.Margin = new System.Windows.Forms.Padding(2);
            this.DdgConfigBox.Name = "DdgConfigBox";
            this.DdgConfigBox.Size = new System.Drawing.Size(362, 59);
            this.DdgConfigBox.TabIndex = 21;
            // 
            // OptionsBox
            // 
            this.OptionsBox.AutoSize = true;
            this.OptionsBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.OptionsBox.Controls.Add(this.ShowDifference);
            this.OptionsBox.Controls.Add(this.ShowLast);
            this.OptionsBox.Controls.Add(this.PersistentGraphing);
            this.OptionsBox.Controls.Add(this.CollectLaser);
            this.OptionsBox.Controls.Add(this.NAverage);
            this.OptionsBox.Controls.Add(NAverageLabel);
            this.OptionsBox.Location = new System.Drawing.Point(370, 3);
            this.OptionsBox.Name = "OptionsBox";
            this.OptionsBox.Size = new System.Drawing.Size(304, 119);
            this.OptionsBox.TabIndex = 22;
            this.OptionsBox.TabStop = false;
            this.OptionsBox.Text = "Options";
            // 
            // GraphScroll
            // 
            this.GraphScroll.Dock = System.Windows.Forms.DockStyle.Right;
            this.GraphScroll.Location = new System.Drawing.Point(1159, 0);
            this.GraphScroll.Name = "GraphScroll";
            this.GraphScroll.Size = new System.Drawing.Size(17, 543);
            this.GraphScroll.TabIndex = 14;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.ImageMode);
            this.panel1.Controls.Add(this.FvbMode);
            this.panel1.Location = new System.Drawing.Point(6, 19);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(183, 23);
            this.panel1.TabIndex = 23;
            // 
            // ImageMode
            // 
            this.ImageMode.AutoSize = true;
            this.ImageMode.Location = new System.Drawing.Point(126, 3);
            this.ImageMode.Name = "ImageMode";
            this.ImageMode.Size = new System.Drawing.Size(54, 17);
            this.ImageMode.TabIndex = 1;
            this.ImageMode.Text = "Image";
            this.ImageMode.UseVisualStyleBackColor = true;
            // 
            // FvbMode
            // 
            this.FvbMode.AutoSize = true;
            this.FvbMode.Checked = true;
            this.FvbMode.Location = new System.Drawing.Point(3, 3);
            this.FvbMode.Name = "FvbMode";
            this.FvbMode.Size = new System.Drawing.Size(117, 17);
            this.FvbMode.TabIndex = 0;
            this.FvbMode.TabStop = true;
            this.FvbMode.Text = "Full Vertical Binning";
            this.FvbMode.UseVisualStyleBackColor = true;
            // 
            // CameraExtras
            // 
            this.CameraExtras.Controls.Add(this.panel1);
            this.CameraExtras.Dock = System.Windows.Forms.DockStyle.Top;
            this.CameraExtras.Location = new System.Drawing.Point(0, 0);
            this.CameraExtras.Name = "CameraExtras";
            this.CameraExtras.Size = new System.Drawing.Size(300, 100);
            this.CameraExtras.TabIndex = 0;
            this.CameraExtras.TabStop = false;
            this.CameraExtras.Text = "Additional Camera Options";
            // 
            // ScrollTip
            // 
            this.ScrollTip.AutoPopDelay = 10000;
            this.ScrollTip.InitialDelay = 50;
            this.ScrollTip.ReshowDelay = 100;
            this.ScrollTip.ShowAlways = true;
            // 
            // ResidualsControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Name = "ResidualsControl";
            this.ParentPanel.ResumeLayout(false);
            this.ParentPanel.PerformLayout();
            this.StatusBox.ResumeLayout(false);
            this.StatusBox.PerformLayout();
            this.CommandsBox.ResumeLayout(false);
            this.CommandsBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NScan)).EndInit();
            this.CommonObjectPanel.ResumeLayout(false);
            this.CommonObjectPanel.PerformLayout();
            this.LeftChildArea.ResumeLayout(false);
            this.LeftChildArea.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraGain)).EndInit();
            this.RightChildArea.ResumeLayout(false);
            this.LeftPanel.ResumeLayout(false);
            this.LeftPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NAverage)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.OptionsBox.ResumeLayout(false);
            this.OptionsBox.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.CameraExtras.ResumeLayout(false);
            this.CameraExtras.PerformLayout();
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
        private System.Windows.Forms.NumericUpDown NAverage;
        private System.Windows.Forms.TextBox DiffSum;
        private System.Windows.Forms.Button SaveProfile;
        private System.Windows.Forms.CheckBox PersistentGraphing;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox CollectLaser;
        private controls.DdgCommandPanel DdgConfigBox;
        private System.Windows.Forms.GroupBox OptionsBox;
        private System.Windows.Forms.VScrollBar GraphScroll;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton FvbMode;
        private System.Windows.Forms.RadioButton ImageMode;
        private System.Windows.Forms.GroupBox CameraExtras;
        private System.Windows.Forms.ToolTip ScrollTip;
    }
}

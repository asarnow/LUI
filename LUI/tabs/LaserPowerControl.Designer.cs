namespace LUI.tabs
{
    partial class LaserPowerControl
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
            this.CountsDisplay = new System.Windows.Forms.TextBox();
            this.CountsLabel = new System.Windows.Forms.Label();
            this.PumpBox = new LUI.controls.ObjectCommandPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PumpAlways = new System.Windows.Forms.RadioButton();
            this.PumpTs = new System.Windows.Forms.RadioButton();
            this.PumpNever = new System.Windows.Forms.RadioButton();
            this.Discard = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.ParentPanel.SuspendLayout();
            this.StatusBox.SuspendLayout();
            this.CommandsBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NScan)).BeginInit();
            this.CommonObjectPanel.SuspendLayout();
            this.LeftChildArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraGain)).BeginInit();
            this.RightChildArea.SuspendLayout();
            this.LeftPanel.SuspendLayout();
            this.PumpBox.Flow.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Discard)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            this.StatusBox.Controls.SetChildIndex(this.ProgressLabel, 0);
            this.StatusBox.Controls.SetChildIndex(this.CameraStatus, 0);
            this.StatusBox.Controls.SetChildIndex(this.ScanProgress, 0);
            // 
            // Graph
            // 
            this.Graph.XLeft = 1F;
            this.Graph.XRight = 1024F;
            // 
            // LeftChildArea
            // 
            this.LeftChildArea.Controls.Add(this.CountsLabel);
            this.LeftChildArea.Controls.Add(this.CountsDisplay);
            // 
            // RightChildArea
            // 
            this.RightChildArea.Controls.Add(this.PumpBox);
            // 
            // CountsDisplay
            // 
            this.CountsDisplay.Location = new System.Drawing.Point(47, 33);
            this.CountsDisplay.Margin = new System.Windows.Forms.Padding(4);
            this.CountsDisplay.Name = "CountsDisplay";
            this.CountsDisplay.ReadOnly = true;
            this.CountsDisplay.Size = new System.Drawing.Size(132, 20);
            this.CountsDisplay.TabIndex = 9;
            this.CountsDisplay.Text = "0";
            this.CountsDisplay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CountsLabel
            // 
            this.CountsLabel.AutoSize = true;
            this.CountsLabel.Location = new System.Drawing.Point(187, 36);
            this.CountsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.CountsLabel.Name = "CountsLabel";
            this.CountsLabel.Size = new System.Drawing.Size(76, 13);
            this.CountsLabel.TabIndex = 10;
            this.CountsLabel.Text = "Optical density";
            // 
            // PumpBox
            // 
            this.PumpBox.AutoSize = true;
            this.PumpBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PumpBox.Dock = System.Windows.Forms.DockStyle.Top;
            // 
            // PumpBox.Flow
            // 
            this.PumpBox.Flow.AutoSize = true;
            this.PumpBox.Flow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PumpBox.Flow.Controls.Add(this.panel1);
            this.PumpBox.Flow.Controls.Add(this.panel3);
            this.PumpBox.Flow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PumpBox.Flow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.PumpBox.Flow.Location = new System.Drawing.Point(3, 16);
            this.PumpBox.Flow.Name = "Flow";
            this.PumpBox.Flow.Size = new System.Drawing.Size(294, 101);
            this.PumpBox.Flow.TabIndex = 0;
            this.PumpBox.Location = new System.Drawing.Point(0, 0);
            this.PumpBox.Name = "PumpBox";
            this.PumpBox.SelectedObject = null;
            this.PumpBox.Size = new System.Drawing.Size(300, 120);
            this.PumpBox.TabIndex = 1;
            this.PumpBox.Text = "Syringe Pump";
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.PumpAlways);
            this.panel1.Controls.Add(this.PumpTs);
            this.panel1.Controls.Add(this.PumpNever);
            this.panel1.Location = new System.Drawing.Point(3, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(198, 30);
            this.panel1.TabIndex = 2;
            // 
            // PumpAlways
            // 
            this.PumpAlways.AutoSize = true;
            this.PumpAlways.Location = new System.Drawing.Point(137, 10);
            this.PumpAlways.Name = "PumpAlways";
            this.PumpAlways.Size = new System.Drawing.Size(58, 17);
            this.PumpAlways.TabIndex = 2;
            this.PumpAlways.TabStop = true;
            this.PumpAlways.Text = "Always";
            this.PumpAlways.UseVisualStyleBackColor = true;
            // 
            // PumpTs
            // 
            this.PumpTs.AutoSize = true;
            this.PumpTs.Location = new System.Drawing.Point(68, 10);
            this.PumpTs.Name = "PumpTs";
            this.PumpTs.Size = new System.Drawing.Size(63, 17);
            this.PumpTs.TabIndex = 1;
            this.PumpTs.TabStop = true;
            this.PumpTs.Text = "TS Only";
            this.PumpTs.UseVisualStyleBackColor = true;
            // 
            // PumpNever
            // 
            this.PumpNever.AutoSize = true;
            this.PumpNever.Checked = true;
            this.PumpNever.Location = new System.Drawing.Point(8, 10);
            this.PumpNever.Name = "PumpNever";
            this.PumpNever.Size = new System.Drawing.Size(54, 17);
            this.PumpNever.TabIndex = 0;
            this.PumpNever.TabStop = true;
            this.PumpNever.Text = "Never";
            this.PumpNever.UseVisualStyleBackColor = true;
            // 
            // Discard
            // 
            this.Discard.Location = new System.Drawing.Point(71, 3);
            this.Discard.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.Discard.Name = "Discard";
            this.Discard.Size = new System.Drawing.Size(35, 20);
            this.Discard.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Discard first";
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel3.Controls.Add(this.Discard);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Location = new System.Drawing.Point(3, 72);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(109, 26);
            this.panel3.TabIndex = 4;
            // 
            // LaserPowerControl
            // 
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Name = "LaserPowerControl";
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
            this.RightChildArea.PerformLayout();
            this.LeftPanel.ResumeLayout(false);
            this.LeftPanel.PerformLayout();
            this.PumpBox.Flow.ResumeLayout(false);
            this.PumpBox.Flow.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Discard)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox CountsDisplay;
        private System.Windows.Forms.Label CountsLabel;
        private controls.ObjectCommandPanel PumpBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton PumpAlways;
        private System.Windows.Forms.RadioButton PumpTs;
        private System.Windows.Forms.RadioButton PumpNever;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.NumericUpDown Discard;
        private System.Windows.Forms.Label label4;
    }
}

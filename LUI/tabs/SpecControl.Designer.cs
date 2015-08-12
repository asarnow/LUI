namespace LUI.tabs
{
    partial class SpecControl
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
            this.SaveData = new System.Windows.Forms.Button();
            this.PumpBox = new LUI.controls.ObjectCommandPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PumpAlways = new System.Windows.Forms.RadioButton();
            this.PumpNever = new System.Windows.Forms.RadioButton();
            this.CountsLabel = new System.Windows.Forms.Label();
            this.CountsDisplay = new System.Windows.Forms.TextBox();
            this.ClearBlank = new System.Windows.Forms.Button();
            this.CurvesView = new LUI.controls.PlotCurveListView();
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
            // CommandsBox
            // 
            this.CommandsBox.Controls.Add(this.ClearBlank);
            this.CommandsBox.Controls.SetChildIndex(this.Abort, 0);
            this.CommandsBox.Controls.SetChildIndex(this.Collect, 0);
            this.CommandsBox.Controls.SetChildIndex(this.NScan, 0);
            this.CommandsBox.Controls.SetChildIndex(this.Clear, 0);
            this.CommandsBox.Controls.SetChildIndex(this.Pause, 0);
            this.CommandsBox.Controls.SetChildIndex(this.ClearBlank, 0);
            // 
            // Graph
            // 
            this.Graph.Size = new System.Drawing.Size(1176, 663);
            this.Graph.XLeft = 1F;
            this.Graph.XRight = 1024F;
            // 
            // LeftChildArea
            // 
            this.LeftChildArea.Controls.Add(this.CurvesView);
            this.LeftChildArea.Controls.Add(this.CountsLabel);
            this.LeftChildArea.Controls.Add(this.CountsDisplay);
            this.LeftChildArea.Controls.Add(this.SaveData);
            this.LeftChildArea.Location = new System.Drawing.Point(0, 663);
            this.LeftChildArea.Size = new System.Drawing.Size(1176, 158);
            // 
            // RightChildArea
            // 
            this.RightChildArea.Controls.Add(this.PumpBox);
            // 
            // SaveData
            // 
            this.SaveData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveData.Location = new System.Drawing.Point(1034, 4);
            this.SaveData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SaveData.Name = "SaveData";
            this.SaveData.Size = new System.Drawing.Size(136, 34);
            this.SaveData.TabIndex = 13;
            this.SaveData.Text = "Save Data";
            this.SaveData.UseVisualStyleBackColor = true;
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
            this.panel1.Controls.Add(this.PumpNever);
            this.panel1.Location = new System.Drawing.Point(3, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(129, 30);
            this.panel1.TabIndex = 2;
            // 
            // PumpAlways
            // 
            this.PumpAlways.AutoSize = true;
            this.PumpAlways.Location = new System.Drawing.Point(68, 10);
            this.PumpAlways.Name = "PumpAlways";
            this.PumpAlways.Size = new System.Drawing.Size(58, 17);
            this.PumpAlways.TabIndex = 2;
            this.PumpAlways.TabStop = true;
            this.PumpAlways.Text = "Always";
            this.PumpAlways.UseVisualStyleBackColor = true;
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
            // CountsLabel
            // 
            this.CountsLabel.AutoSize = true;
            this.CountsLabel.Location = new System.Drawing.Point(438, 9);
            this.CountsLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.CountsLabel.Name = "CountsLabel";
            this.CountsLabel.Size = new System.Drawing.Size(76, 13);
            this.CountsLabel.TabIndex = 15;
            this.CountsLabel.Text = "Optical density";
            // 
            // CountsDisplay
            // 
            this.CountsDisplay.Location = new System.Drawing.Point(298, 6);
            this.CountsDisplay.Margin = new System.Windows.Forms.Padding(4);
            this.CountsDisplay.Name = "CountsDisplay";
            this.CountsDisplay.ReadOnly = true;
            this.CountsDisplay.Size = new System.Drawing.Size(132, 20);
            this.CountsDisplay.TabIndex = 14;
            this.CountsDisplay.Text = "0";
            this.CountsDisplay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ClearBlank
            // 
            this.ClearBlank.Location = new System.Drawing.Point(8, 106);
            this.ClearBlank.Name = "ClearBlank";
            this.ClearBlank.Size = new System.Drawing.Size(91, 34);
            this.ClearBlank.TabIndex = 6;
            this.ClearBlank.Text = "Clear Blank";
            this.ClearBlank.UseVisualStyleBackColor = true;
            // 
            // CurvesView
            // 
            this.CurvesView.Graph = null;
            this.CurvesView.Location = new System.Drawing.Point(3, 5);
            this.CurvesView.Name = "CurvesView";
            this.CurvesView.Size = new System.Drawing.Size(288, 150);
            this.CurvesView.TabIndex = 16;
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
            this.panel3.TabIndex = 5;
            // 
            // SpecControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "SpecControl";
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

        private System.Windows.Forms.Button SaveData;
        private controls.ObjectCommandPanel PumpBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton PumpAlways;
        private System.Windows.Forms.RadioButton PumpNever;
        private System.Windows.Forms.Label CountsLabel;
        private System.Windows.Forms.TextBox CountsDisplay;
        private System.Windows.Forms.Button ClearBlank;
        private controls.PlotCurveListView CurvesView;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.NumericUpDown Discard;
        private System.Windows.Forms.Label label4;
    }
}

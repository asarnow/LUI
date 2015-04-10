namespace LUI.tabs
{
    partial class LuiTab
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LuiTab));
            this.ParentPanel = new System.Windows.Forms.Panel();
            this.StatusBox = new System.Windows.Forms.GroupBox();
            this.ProgressLabel = new System.Windows.Forms.Label();
            this.StatusProgress = new System.Windows.Forms.ProgressBar();
            this.CommandsBox = new System.Windows.Forms.GroupBox();
            this.Clear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.NScan = new System.Windows.Forms.NumericUpDown();
            this.Collect = new System.Windows.Forms.Button();
            this.Abort = new System.Windows.Forms.Button();
            this.BeamFlagBox = new System.Windows.Forms.GroupBox();
            this.CloseLamp = new System.Windows.Forms.Button();
            this.OpenLamp = new System.Windows.Forms.Button();
            this.CloseLaser = new System.Windows.Forms.Button();
            this.OpenLaser = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ObjectSelector = new LUI.controls.ObjectSelectPanel();
            this.Graph = new LUI.controls.GraphControl();
            this.ParentPanel.SuspendLayout();
            this.StatusBox.SuspendLayout();
            this.CommandsBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NScan)).BeginInit();
            this.BeamFlagBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ParentPanel
            // 
            this.ParentPanel.Controls.Add(this.StatusBox);
            this.ParentPanel.Controls.Add(this.CommandsBox);
            this.ParentPanel.Controls.Add(this.BeamFlagBox);
            this.ParentPanel.Controls.Add(this.panel1);
            this.ParentPanel.Controls.Add(this.Graph);
            this.ParentPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ParentPanel.Location = new System.Drawing.Point(0, 0);
            this.ParentPanel.Name = "ParentPanel";
            this.ParentPanel.Size = new System.Drawing.Size(1107, 667);
            this.ParentPanel.TabIndex = 0;
            // 
            // StatusBox
            // 
            this.StatusBox.Controls.Add(this.ProgressLabel);
            this.StatusBox.Controls.Add(this.StatusProgress);
            this.StatusBox.Location = new System.Drawing.Point(829, 3);
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.Size = new System.Drawing.Size(271, 209);
            this.StatusBox.TabIndex = 9;
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
            this.CommandsBox.Controls.Add(this.Clear);
            this.CommandsBox.Controls.Add(this.label1);
            this.CommandsBox.Controls.Add(this.NScan);
            this.CommandsBox.Controls.Add(this.Collect);
            this.CommandsBox.Controls.Add(this.Abort);
            this.CommandsBox.Location = new System.Drawing.Point(829, 218);
            this.CommandsBox.Name = "CommandsBox";
            this.CommandsBox.Size = new System.Drawing.Size(271, 209);
            this.CommandsBox.TabIndex = 10;
            this.CommandsBox.TabStop = false;
            this.CommandsBox.Text = "Commands";
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
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(155, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Scans";
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
            1,
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
            // 
            // Abort
            // 
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
            this.BeamFlagBox.Location = new System.Drawing.Point(829, 433);
            this.BeamFlagBox.Name = "BeamFlagBox";
            this.BeamFlagBox.Size = new System.Drawing.Size(271, 87);
            this.BeamFlagBox.TabIndex = 11;
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
            // 
            // OpenLamp
            // 
            this.OpenLamp.Location = new System.Drawing.Point(114, 19);
            this.OpenLamp.Name = "OpenLamp";
            this.OpenLamp.Size = new System.Drawing.Size(102, 28);
            this.OpenLamp.TabIndex = 6;
            this.OpenLamp.Text = "Open Lamp";
            this.OpenLamp.UseVisualStyleBackColor = true;
            // 
            // CloseLaser
            // 
            this.CloseLaser.Location = new System.Drawing.Point(6, 53);
            this.CloseLaser.Name = "CloseLaser";
            this.CloseLaser.Size = new System.Drawing.Size(102, 28);
            this.CloseLaser.TabIndex = 5;
            this.CloseLaser.Text = "Close Laser";
            this.CloseLaser.UseVisualStyleBackColor = true;
            // 
            // OpenLaser
            // 
            this.OpenLaser.Location = new System.Drawing.Point(6, 19);
            this.OpenLaser.Name = "OpenLaser";
            this.OpenLaser.Size = new System.Drawing.Size(102, 28);
            this.OpenLaser.TabIndex = 4;
            this.OpenLaser.Text = "Open Laser";
            this.OpenLaser.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.ObjectSelector);
            this.panel1.Location = new System.Drawing.Point(0, 432);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(824, 210);
            this.panel1.TabIndex = 12;
            // 
            // ObjectSelector
            // 
            this.ObjectSelector.AutoSize = true;
            this.ObjectSelector.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ObjectSelector.Location = new System.Drawing.Point(432, 174);
            this.ObjectSelector.Name = "ObjectSelector";
            this.ObjectSelector.SelectedBeamFlags = null;
            this.ObjectSelector.SelectedCamera = null;
            this.ObjectSelector.Size = new System.Drawing.Size(389, 33);
            this.ObjectSelector.TabIndex = 8;
            // 
            // Graph
            // 
            this.Graph.AxesColor = System.Drawing.Color.Blue;
            this.Graph.AxesFont = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Graph.AxesFontColor = System.Drawing.Color.Black;
            this.Graph.ColorOrder = ((System.Collections.Generic.List<System.Drawing.Color>)(resources.GetObject("Graph.ColorOrder")));
            this.Graph.InitialScaleHeight = 0F;
            this.Graph.InitialXLeft = 0F;
            this.Graph.InitialXRight = 1023F;
            this.Graph.InitialYMax = float.NegativeInfinity;
            this.Graph.InitialYMin = float.PositiveInfinity;
            this.Graph.Location = new System.Drawing.Point(0, 2);
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
            this.Graph.ScaleHeight = 0F;
            this.Graph.Size = new System.Drawing.Size(824, 426);
            this.Graph.TabIndex = 13;
            this.Graph.XAxisHeight = 0.1F;
            this.Graph.XLabelFormat = "f0";
            this.Graph.XLeft = 0F;
            this.Graph.XRight = 1023F;
            this.Graph.YAxisWidth = 0.05F;
            this.Graph.YLabelFormat = "n3";
            this.Graph.YMax = float.NegativeInfinity;
            this.Graph.YMin = float.PositiveInfinity;
            // 
            // LuiTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ParentPanel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "LuiTab";
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
            this.ResumeLayout(false);

        }

        #endregion

        protected System.Windows.Forms.Panel ParentPanel;
        protected System.Windows.Forms.GroupBox StatusBox;
        protected System.Windows.Forms.Label ProgressLabel;
        protected System.Windows.Forms.ProgressBar StatusProgress;
        protected System.Windows.Forms.GroupBox CommandsBox;
        protected System.Windows.Forms.Button Clear;
        private System.Windows.Forms.Label label1;
        protected System.Windows.Forms.NumericUpDown NScan;
        protected System.Windows.Forms.Button Collect;
        protected System.Windows.Forms.Button Abort;
        protected System.Windows.Forms.GroupBox BeamFlagBox;
        protected System.Windows.Forms.Button CloseLamp;
        protected System.Windows.Forms.Button OpenLamp;
        protected System.Windows.Forms.Button CloseLaser;
        protected System.Windows.Forms.Button OpenLaser;
        protected System.Windows.Forms.Panel panel1;
        protected controls.ObjectSelectPanel ObjectSelector;
        protected controls.GraphControl Graph;


    }
}

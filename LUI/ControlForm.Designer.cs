namespace LUI
{
    partial class ControlForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.MenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadTimesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadCalibrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.timesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calibrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.singleShotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.documentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutLUIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SpecGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.StatusBox = new System.Windows.Forms.GroupBox();
            this.ProgressLabel = new System.Windows.Forms.Label();
            this.StatusProgress = new System.Windows.Forms.ProgressBar();
            this.CommandsBox = new System.Windows.Forms.GroupBox();
            this.TROA = new System.Windows.Forms.Button();
            this.Clear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Averages = new System.Windows.Forms.NumericUpDown();
            this.Collect = new System.Windows.Forms.Button();
            this.Abort = new System.Windows.Forms.Button();
            this.Start = new System.Windows.Forms.Button();
            this.BeamFlagBox = new System.Windows.Forms.GroupBox();
            this.CloseLamp = new System.Windows.Forms.Button();
            this.OpenLamp = new System.Windows.Forms.Button();
            this.CloseLaser = new System.Windows.Forms.Button();
            this.OpenLaser = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.MenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SpecGraph)).BeginInit();
            this.StatusBox.SuspendLayout();
            this.CommandsBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Averages)).BeginInit();
            this.BeamFlagBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.MenuStrip);
            this.tableLayoutPanel1.Controls.Add(this.SpecGraph, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.StatusBox, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.CommandsBox, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.BeamFlagBox, 3, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1107, 667);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.MenuStrip.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(276, 24);
            this.MenuStrip.TabIndex = 0;
            this.MenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadTimesToolStripMenuItem,
            this.loadCalibrationToolStripMenuItem,
            this.saveDataToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loadTimesToolStripMenuItem
            // 
            this.loadTimesToolStripMenuItem.Name = "loadTimesToolStripMenuItem";
            this.loadTimesToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.loadTimesToolStripMenuItem.Text = "Load times...";
            this.loadTimesToolStripMenuItem.Click += new System.EventHandler(this.loadTimesToolStripMenuItem_Click);
            // 
            // loadCalibrationToolStripMenuItem
            // 
            this.loadCalibrationToolStripMenuItem.Name = "loadCalibrationToolStripMenuItem";
            this.loadCalibrationToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.loadCalibrationToolStripMenuItem.Text = "Load calibration...";
            // 
            // saveDataToolStripMenuItem
            // 
            this.saveDataToolStripMenuItem.Name = "saveDataToolStripMenuItem";
            this.saveDataToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.saveDataToolStripMenuItem.Text = "Save data...";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.optionsToolStripMenuItem.Text = "Options...";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.timesToolStripMenuItem,
            this.flowToolStripMenuItem,
            this.calibrationToolStripMenuItem,
            this.singleShotToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // timesToolStripMenuItem
            // 
            this.timesToolStripMenuItem.Name = "timesToolStripMenuItem";
            this.timesToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.timesToolStripMenuItem.Text = "Times";
            // 
            // flowToolStripMenuItem
            // 
            this.flowToolStripMenuItem.Name = "flowToolStripMenuItem";
            this.flowToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.flowToolStripMenuItem.Text = "Flow";
            // 
            // calibrationToolStripMenuItem
            // 
            this.calibrationToolStripMenuItem.Name = "calibrationToolStripMenuItem";
            this.calibrationToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.calibrationToolStripMenuItem.Text = "Calibration";
            // 
            // singleShotToolStripMenuItem
            // 
            this.singleShotToolStripMenuItem.Name = "singleShotToolStripMenuItem";
            this.singleShotToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.singleShotToolStripMenuItem.Text = "Single shot";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.documentationToolStripMenuItem,
            this.aboutLUIToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // documentationToolStripMenuItem
            // 
            this.documentationToolStripMenuItem.Name = "documentationToolStripMenuItem";
            this.documentationToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.documentationToolStripMenuItem.Text = "Documentation";
            // 
            // aboutLUIToolStripMenuItem
            // 
            this.aboutLUIToolStripMenuItem.Name = "aboutLUIToolStripMenuItem";
            this.aboutLUIToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
            this.aboutLUIToolStripMenuItem.Text = "About LUI";
            // 
            // SpecGraph
            // 
            chartArea2.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea2.AxisX.Maximum = 1023D;
            chartArea2.AxisX.Minimum = 0D;
            chartArea2.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea2.AxisY.Maximum = 65536D;
            chartArea2.AxisY.Minimum = 0D;
            chartArea2.Name = "MainChart";
            this.SpecGraph.ChartAreas.Add(chartArea2);
            this.tableLayoutPanel1.SetColumnSpan(this.SpecGraph, 3);
            legend2.Enabled = false;
            legend2.Name = "Legend1";
            this.SpecGraph.Legends.Add(legend2);
            this.SpecGraph.Location = new System.Drawing.Point(3, 29);
            this.SpecGraph.Name = "SpecGraph";
            this.tableLayoutPanel1.SetRowSpan(this.SpecGraph, 2);
            series2.ChartArea = "MainChart";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.SpecGraph.Series.Add(series2);
            this.SpecGraph.Size = new System.Drawing.Size(822, 314);
            this.SpecGraph.TabIndex = 1;
            this.SpecGraph.Text = "chart1";
            // 
            // StatusBox
            // 
            this.StatusBox.Controls.Add(this.ProgressLabel);
            this.StatusBox.Controls.Add(this.StatusProgress);
            this.StatusBox.Location = new System.Drawing.Point(831, 29);
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.Size = new System.Drawing.Size(273, 154);
            this.StatusBox.TabIndex = 2;
            this.StatusBox.TabStop = false;
            this.StatusBox.Text = "Status";
            // 
            // ProgressLabel
            // 
            this.ProgressLabel.AutoSize = true;
            this.ProgressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProgressLabel.Location = new System.Drawing.Point(4, 71);
            this.ProgressLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ProgressLabel.Name = "ProgressLabel";
            this.ProgressLabel.Size = new System.Drawing.Size(34, 17);
            this.ProgressLabel.TabIndex = 5;
            this.ProgressLabel.Text = "Idle";
            // 
            // StatusProgress
            // 
            this.StatusProgress.Location = new System.Drawing.Point(7, 89);
            this.StatusProgress.Margin = new System.Windows.Forms.Padding(2);
            this.StatusProgress.Name = "StatusProgress";
            this.StatusProgress.Size = new System.Drawing.Size(170, 28);
            this.StatusProgress.TabIndex = 4;
            // 
            // CommandsBox
            // 
            this.CommandsBox.Controls.Add(this.TROA);
            this.CommandsBox.Controls.Add(this.Clear);
            this.CommandsBox.Controls.Add(this.label1);
            this.CommandsBox.Controls.Add(this.Averages);
            this.CommandsBox.Controls.Add(this.Collect);
            this.CommandsBox.Controls.Add(this.Abort);
            this.CommandsBox.Controls.Add(this.Start);
            this.CommandsBox.Location = new System.Drawing.Point(831, 189);
            this.CommandsBox.Name = "CommandsBox";
            this.CommandsBox.Size = new System.Drawing.Size(273, 154);
            this.CommandsBox.TabIndex = 3;
            this.CommandsBox.TabStop = false;
            this.CommandsBox.Text = "Commands";
            // 
            // TROA
            // 
            this.TROA.Location = new System.Drawing.Point(80, 53);
            this.TROA.Margin = new System.Windows.Forms.Padding(2);
            this.TROA.Name = "TROA";
            this.TROA.Size = new System.Drawing.Size(102, 28);
            this.TROA.TabIndex = 5;
            this.TROA.Text = "Collect TROA";
            this.TROA.UseVisualStyleBackColor = true;
            this.TROA.Click += new System.EventHandler(this.TROA_Click);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(230, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Scans";
            // 
            // Averages
            // 
            this.Averages.Location = new System.Drawing.Point(188, 21);
            this.Averages.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.Averages.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Averages.Name = "Averages";
            this.Averages.Size = new System.Drawing.Size(36, 20);
            this.Averages.TabIndex = 3;
            this.Averages.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Collect
            // 
            this.Collect.Location = new System.Drawing.Point(80, 19);
            this.Collect.Name = "Collect";
            this.Collect.Size = new System.Drawing.Size(102, 28);
            this.Collect.TabIndex = 2;
            this.Collect.Text = "Collect UV/vis";
            this.Collect.UseVisualStyleBackColor = true;
            this.Collect.Click += new System.EventHandler(this.Collect_Click);
            // 
            // Abort
            // 
            this.Abort.Location = new System.Drawing.Point(6, 53);
            this.Abort.Name = "Abort";
            this.Abort.Size = new System.Drawing.Size(68, 28);
            this.Abort.TabIndex = 1;
            this.Abort.Text = "Abort";
            this.Abort.UseVisualStyleBackColor = true;
            this.Abort.Click += new System.EventHandler(this.Abort_Click);
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(6, 19);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(68, 28);
            this.Start.TabIndex = 0;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            // 
            // BeamFlagBox
            // 
            this.BeamFlagBox.Controls.Add(this.CloseLamp);
            this.BeamFlagBox.Controls.Add(this.OpenLamp);
            this.BeamFlagBox.Controls.Add(this.CloseLaser);
            this.BeamFlagBox.Controls.Add(this.OpenLaser);
            this.BeamFlagBox.Location = new System.Drawing.Point(831, 349);
            this.BeamFlagBox.Name = "BeamFlagBox";
            this.BeamFlagBox.Size = new System.Drawing.Size(233, 87);
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
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "TIM Files (*.tim)|*.tim|CAL Files (*.cal)|*.cal|TXT Files (*.txt)|*.txt|All Files" +
    " (*.*)|*.*";
            this.openFileDialog1.Title = "Open";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "TIM Files (*.tim)|*.tim|CAL Files (*.cal)|*.cal|TXT Files (*.txt)|*.txt|All Files" +
    " (*.*)|*.*";
            this.saveFileDialog1.Title = "Save";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // ControlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1107, 667);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MainMenuStrip = this.MenuStrip;
            this.Name = "ControlForm";
            this.Text = "LUI 3.0 Control";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.MenuStrip.ResumeLayout(false);
            this.MenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SpecGraph)).EndInit();
            this.StatusBox.ResumeLayout(false);
            this.StatusBox.PerformLayout();
            this.CommandsBox.ResumeLayout(false);
            this.CommandsBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Averages)).EndInit();
            this.BeamFlagBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.MenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadTimesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadCalibrationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveDataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem documentationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutLUIToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem timesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem flowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem calibrationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem singleShotToolStripMenuItem;
        private System.Windows.Forms.DataVisualization.Charting.Chart SpecGraph;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.GroupBox StatusBox;
        private System.Windows.Forms.GroupBox CommandsBox;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Button Collect;
        private System.Windows.Forms.Button Abort;
        private System.Windows.Forms.NumericUpDown Averages;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.Button TROA;
        private System.Windows.Forms.ProgressBar StatusProgress;
        private System.Windows.Forms.Label ProgressLabel;
        private System.Windows.Forms.GroupBox BeamFlagBox;
        private System.Windows.Forms.Button CloseLamp;
        private System.Windows.Forms.Button OpenLamp;
        private System.Windows.Forms.Button CloseLaser;
        private System.Windows.Forms.Button OpenLaser;
    }
}
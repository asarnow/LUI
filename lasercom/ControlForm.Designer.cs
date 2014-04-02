namespace lasercom
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
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
            this.Chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.StatusBox = new System.Windows.Forms.GroupBox();
            this.CommandsBox = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Averages = new System.Windows.Forms.NumericUpDown();
            this.Collect = new System.Windows.Forms.Button();
            this.Abort = new System.Windows.Forms.Button();
            this.Start = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Chart1)).BeginInit();
            this.CommandsBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Averages)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Controls.Add(this.menuStrip1);
            this.tableLayoutPanel1.Controls.Add(this.Chart1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.StatusBox, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.CommandsBox, 3, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 26F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(992, 667);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(248, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
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
            // Chart1
            // 
            chartArea3.Name = "ChartArea1";
            this.Chart1.ChartAreas.Add(chartArea3);
            this.tableLayoutPanel1.SetColumnSpan(this.Chart1, 3);
            legend3.Enabled = false;
            legend3.Name = "Legend1";
            this.Chart1.Legends.Add(legend3);
            this.Chart1.Location = new System.Drawing.Point(3, 29);
            this.Chart1.Name = "Chart1";
            this.tableLayoutPanel1.SetRowSpan(this.Chart1, 2);
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.Chart1.Series.Add(series3);
            this.Chart1.Size = new System.Drawing.Size(738, 314);
            this.Chart1.TabIndex = 1;
            this.Chart1.Text = "chart1";
            // 
            // StatusBox
            // 
            this.StatusBox.Location = new System.Drawing.Point(747, 29);
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.Size = new System.Drawing.Size(242, 154);
            this.StatusBox.TabIndex = 2;
            this.StatusBox.TabStop = false;
            this.StatusBox.Text = "Status";
            // 
            // CommandsBox
            // 
            this.CommandsBox.Controls.Add(this.label1);
            this.CommandsBox.Controls.Add(this.Averages);
            this.CommandsBox.Controls.Add(this.Collect);
            this.CommandsBox.Controls.Add(this.Abort);
            this.CommandsBox.Controls.Add(this.Start);
            this.CommandsBox.Location = new System.Drawing.Point(747, 189);
            this.CommandsBox.Name = "CommandsBox";
            this.CommandsBox.Size = new System.Drawing.Size(242, 154);
            this.CommandsBox.TabIndex = 3;
            this.CommandsBox.TabStop = false;
            this.CommandsBox.Text = "Commands";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(163, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Averages";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Averages
            // 
            this.Averages.Location = new System.Drawing.Point(121, 21);
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
            this.Collect.Location = new System.Drawing.Point(64, 19);
            this.Collect.Name = "Collect";
            this.Collect.Size = new System.Drawing.Size(51, 23);
            this.Collect.TabIndex = 2;
            this.Collect.Text = "Collect";
            this.Collect.UseVisualStyleBackColor = true;
            this.Collect.Click += new System.EventHandler(this.Collect_Click);
            // 
            // Abort
            // 
            this.Abort.Location = new System.Drawing.Point(7, 50);
            this.Abort.Name = "Abort";
            this.Abort.Size = new System.Drawing.Size(51, 23);
            this.Abort.TabIndex = 1;
            this.Abort.Text = "Abort";
            this.Abort.UseVisualStyleBackColor = true;
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(7, 19);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(51, 23);
            this.Start.TabIndex = 0;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
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
            this.ClientSize = new System.Drawing.Size(992, 667);
            this.Controls.Add(this.tableLayoutPanel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ControlForm";
            this.Text = "LUI 3.0 Control";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Chart1)).EndInit();
            this.CommandsBox.ResumeLayout(false);
            this.CommandsBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Averages)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.MenuStrip menuStrip1;
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
        private System.Windows.Forms.DataVisualization.Charting.Chart Chart1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.GroupBox StatusBox;
        private System.Windows.Forms.GroupBox CommandsBox;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.Button Collect;
        private System.Windows.Forms.Button Abort;
        private System.Windows.Forms.NumericUpDown Averages;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
    }
}
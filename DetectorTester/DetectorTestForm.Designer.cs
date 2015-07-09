namespace DetectorTester
{
    partial class DetectorTestForm
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.specGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.imageBox = new System.Windows.Forms.PictureBox();
            this.specButton = new System.Windows.Forms.Button();
            this.darkButton = new System.Windows.Forms.Button();
            this.commandsGroupBox = new System.Windows.Forms.GroupBox();
            this.Abort = new System.Windows.Forms.Button();
            this.Pause = new System.Windows.Forms.Button();
            this.Clear = new System.Windows.Forms.Button();
            this.blankButton = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.CameraTemp = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.LastRow = new System.Windows.Forms.NumericUpDown();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.CameraGain = new System.Windows.Forms.NumericUpDown();
            this.panel2 = new System.Windows.Forms.Panel();
            this.FirstRow = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.ReadModes = new System.Windows.Forms.FlowLayoutPanel();
            this.fvbButton = new System.Windows.Forms.RadioButton();
            this.imageAcqButton = new System.Windows.Forms.RadioButton();
            this.panel6 = new System.Windows.Forms.Panel();
            this.NScans = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.VBin = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.kineticSeriesGroupBox = new System.Windows.Forms.GroupBox();
            this.exciteCheck = new System.Windows.Forms.CheckBox();
            this.seriesLengthLabel = new System.Windows.Forms.Label();
            this.seriesLength = new System.Windows.Forms.NumericUpDown();
            this.RightTop = new System.Windows.Forms.Panel();
            this.CameraConfigBox = new System.Windows.Forms.GroupBox();
            this.ReportBox = new System.Windows.Forms.GroupBox();
            this.CameraStatus = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Top = new System.Windows.Forms.Panel();
            this.GraphScroll = new System.Windows.Forms.VScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this.specGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.commandsGroupBox.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraTemp)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LastRow)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraGain)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FirstRow)).BeginInit();
            this.ReadModes.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NScans)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VBin)).BeginInit();
            this.kineticSeriesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seriesLength)).BeginInit();
            this.RightTop.SuspendLayout();
            this.CameraConfigBox.SuspendLayout();
            this.ReportBox.SuspendLayout();
            this.Top.SuspendLayout();
            this.SuspendLayout();
            // 
            // specGraph
            // 
            chartArea1.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisX.Maximum = 1023D;
            chartArea1.AxisX.Minimum = 0D;
            chartArea1.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisY.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisY.Maximum = 65536D;
            chartArea1.AxisY.Minimum = 0D;
            chartArea1.Name = "specArea";
            this.specGraph.ChartAreas.Add(chartArea1);
            this.specGraph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.specGraph.Location = new System.Drawing.Point(0, 0);
            this.specGraph.Margin = new System.Windows.Forms.Padding(2);
            this.specGraph.Name = "specGraph";
            series1.ChartArea = "specArea";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Name = "spec";
            this.specGraph.Series.Add(series1);
            this.specGraph.Size = new System.Drawing.Size(639, 312);
            this.specGraph.TabIndex = 0;
            this.specGraph.Text = "chart1";
            // 
            // imageBox
            // 
            this.imageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox.Location = new System.Drawing.Point(0, 312);
            this.imageBox.Margin = new System.Windows.Forms.Padding(2);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(872, 267);
            this.imageBox.TabIndex = 1;
            this.imageBox.TabStop = false;
            // 
            // specButton
            // 
            this.specButton.Location = new System.Drawing.Point(4, 17);
            this.specButton.Margin = new System.Windows.Forms.Padding(2);
            this.specButton.Name = "specButton";
            this.specButton.Size = new System.Drawing.Size(62, 24);
            this.specButton.TabIndex = 2;
            this.specButton.Text = "Capture";
            this.specButton.UseVisualStyleBackColor = true;
            this.specButton.Click += new System.EventHandler(this.specButton_Click);
            // 
            // darkButton
            // 
            this.darkButton.Location = new System.Drawing.Point(70, 17);
            this.darkButton.Margin = new System.Windows.Forms.Padding(2);
            this.darkButton.Name = "darkButton";
            this.darkButton.Size = new System.Drawing.Size(62, 24);
            this.darkButton.TabIndex = 4;
            this.darkButton.Text = "Dark";
            this.darkButton.UseVisualStyleBackColor = true;
            this.darkButton.Click += new System.EventHandler(this.darkButton_Click);
            // 
            // commandsGroupBox
            // 
            this.commandsGroupBox.AutoSize = true;
            this.commandsGroupBox.Controls.Add(this.Abort);
            this.commandsGroupBox.Controls.Add(this.Pause);
            this.commandsGroupBox.Controls.Add(this.Clear);
            this.commandsGroupBox.Controls.Add(this.blankButton);
            this.commandsGroupBox.Controls.Add(this.specButton);
            this.commandsGroupBox.Controls.Add(this.darkButton);
            this.commandsGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.commandsGroupBox.Location = new System.Drawing.Point(0, 52);
            this.commandsGroupBox.Margin = new System.Windows.Forms.Padding(2);
            this.commandsGroupBox.Name = "commandsGroupBox";
            this.commandsGroupBox.Padding = new System.Windows.Forms.Padding(2);
            this.commandsGroupBox.Size = new System.Drawing.Size(216, 87);
            this.commandsGroupBox.TabIndex = 5;
            this.commandsGroupBox.TabStop = false;
            this.commandsGroupBox.Text = "Commands";
            // 
            // Abort
            // 
            this.Abort.Location = new System.Drawing.Point(136, 45);
            this.Abort.Margin = new System.Windows.Forms.Padding(2);
            this.Abort.Name = "Abort";
            this.Abort.Size = new System.Drawing.Size(62, 24);
            this.Abort.TabIndex = 8;
            this.Abort.Text = "Abort";
            this.Abort.UseVisualStyleBackColor = true;
            this.Abort.Click += new System.EventHandler(this.Abort_Click);
            // 
            // Pause
            // 
            this.Pause.Location = new System.Drawing.Point(136, 17);
            this.Pause.Margin = new System.Windows.Forms.Padding(2);
            this.Pause.Name = "Pause";
            this.Pause.Size = new System.Drawing.Size(62, 24);
            this.Pause.TabIndex = 7;
            this.Pause.Text = "Pause";
            this.Pause.UseVisualStyleBackColor = true;
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(70, 45);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(62, 24);
            this.Clear.TabIndex = 6;
            this.Clear.Text = "Clear";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // blankButton
            // 
            this.blankButton.Location = new System.Drawing.Point(4, 45);
            this.blankButton.Margin = new System.Windows.Forms.Padding(2);
            this.blankButton.Name = "blankButton";
            this.blankButton.Size = new System.Drawing.Size(62, 24);
            this.blankButton.TabIndex = 5;
            this.blankButton.Text = "Blank";
            this.blankButton.UseVisualStyleBackColor = true;
            this.blankButton.Click += new System.EventHandler(this.blankButton_Click);
            // 
            // panel5
            // 
            this.panel5.AutoSize = true;
            this.panel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel5.Controls.Add(this.CameraTemp);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Location = new System.Drawing.Point(83, 52);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(80, 20);
            this.panel5.TabIndex = 9;
            // 
            // CameraTemp
            // 
            this.CameraTemp.Location = new System.Drawing.Point(38, -2);
            this.CameraTemp.Margin = new System.Windows.Forms.Padding(2);
            this.CameraTemp.Name = "CameraTemp";
            this.CameraTemp.Size = new System.Drawing.Size(40, 20);
            this.CameraTemp.TabIndex = 14;
            this.CameraTemp.ValueChanged += new System.EventHandler(this.CameraTemp_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Temp";
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.LastRow);
            this.panel3.Location = new System.Drawing.Point(109, 104);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(98, 20);
            this.panel3.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Last Row";
            // 
            // LastRow
            // 
            this.LastRow.Location = new System.Drawing.Point(56, -2);
            this.LastRow.Margin = new System.Windows.Forms.Padding(2);
            this.LastRow.Name = "LastRow";
            this.LastRow.Size = new System.Drawing.Size(40, 20);
            this.LastRow.TabIndex = 14;
            // 
            // panel4
            // 
            this.panel4.AutoSize = true;
            this.panel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.CameraGain);
            this.panel4.Location = new System.Drawing.Point(6, 52);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(71, 20);
            this.panel4.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Gain";
            // 
            // CameraGain
            // 
            this.CameraGain.Location = new System.Drawing.Point(29, -2);
            this.CameraGain.Margin = new System.Windows.Forms.Padding(2);
            this.CameraGain.Name = "CameraGain";
            this.CameraGain.Size = new System.Drawing.Size(40, 20);
            this.CameraGain.TabIndex = 14;
            this.CameraGain.ValueChanged += new System.EventHandler(this.CameraGain_ValueChanged);
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Controls.Add(this.FirstRow);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(6, 104);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(97, 20);
            this.panel2.TabIndex = 10;
            // 
            // FirstRow
            // 
            this.FirstRow.Location = new System.Drawing.Point(55, -2);
            this.FirstRow.Margin = new System.Windows.Forms.Padding(2);
            this.FirstRow.Name = "FirstRow";
            this.FirstRow.Size = new System.Drawing.Size(40, 20);
            this.FirstRow.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "First Row";
            // 
            // ReadModes
            // 
            this.ReadModes.AutoSize = true;
            this.ReadModes.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ReadModes.Controls.Add(this.fvbButton);
            this.ReadModes.Controls.Add(this.imageAcqButton);
            this.ReadModes.Controls.Add(this.panel6);
            this.ReadModes.Location = new System.Drawing.Point(6, 19);
            this.ReadModes.Name = "ReadModes";
            this.ReadModes.Size = new System.Drawing.Size(198, 27);
            this.ReadModes.TabIndex = 9;
            // 
            // fvbButton
            // 
            this.fvbButton.AutoSize = true;
            this.fvbButton.Location = new System.Drawing.Point(2, 2);
            this.fvbButton.Margin = new System.Windows.Forms.Padding(2);
            this.fvbButton.Name = "fvbButton";
            this.fvbButton.Size = new System.Drawing.Size(45, 17);
            this.fvbButton.TabIndex = 7;
            this.fvbButton.TabStop = true;
            this.fvbButton.Text = "FVB";
            this.fvbButton.UseVisualStyleBackColor = true;
            this.fvbButton.CheckedChanged += new System.EventHandler(this.fvbButton_CheckedChanged);
            // 
            // imageAcqButton
            // 
            this.imageAcqButton.AutoSize = true;
            this.imageAcqButton.Location = new System.Drawing.Point(51, 2);
            this.imageAcqButton.Margin = new System.Windows.Forms.Padding(2);
            this.imageAcqButton.Name = "imageAcqButton";
            this.imageAcqButton.Size = new System.Drawing.Size(54, 17);
            this.imageAcqButton.TabIndex = 8;
            this.imageAcqButton.TabStop = true;
            this.imageAcqButton.Text = "Image";
            this.imageAcqButton.UseVisualStyleBackColor = true;
            // 
            // panel6
            // 
            this.panel6.AutoSize = true;
            this.panel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel6.Controls.Add(this.NScans);
            this.panel6.Controls.Add(this.label6);
            this.panel6.Location = new System.Drawing.Point(110, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(85, 21);
            this.panel6.TabIndex = 16;
            // 
            // NScans
            // 
            this.NScans.Location = new System.Drawing.Point(43, -1);
            this.NScans.Margin = new System.Windows.Forms.Padding(2);
            this.NScans.Name = "NScans";
            this.NScans.Size = new System.Drawing.Size(40, 20);
            this.NScans.TabIndex = 14;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(2, 1);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(37, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Scans";
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.Controls.Add(this.VBin);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(6, 78);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(126, 20);
            this.panel1.TabIndex = 9;
            // 
            // VBin
            // 
            this.VBin.Location = new System.Drawing.Point(84, -2);
            this.VBin.Margin = new System.Windows.Forms.Padding(2);
            this.VBin.Name = "VBin";
            this.VBin.Size = new System.Drawing.Size(40, 20);
            this.VBin.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Vertical Binning";
            // 
            // kineticSeriesGroupBox
            // 
            this.kineticSeriesGroupBox.Controls.Add(this.exciteCheck);
            this.kineticSeriesGroupBox.Controls.Add(this.seriesLengthLabel);
            this.kineticSeriesGroupBox.Controls.Add(this.seriesLength);
            this.kineticSeriesGroupBox.Location = new System.Drawing.Point(84, 355);
            this.kineticSeriesGroupBox.Margin = new System.Windows.Forms.Padding(2);
            this.kineticSeriesGroupBox.Name = "kineticSeriesGroupBox";
            this.kineticSeriesGroupBox.Padding = new System.Windows.Forms.Padding(2);
            this.kineticSeriesGroupBox.Size = new System.Drawing.Size(216, 70);
            this.kineticSeriesGroupBox.TabIndex = 6;
            this.kineticSeriesGroupBox.TabStop = false;
            this.kineticSeriesGroupBox.Text = "Kinetic Series";
            // 
            // exciteCheck
            // 
            this.exciteCheck.AutoSize = true;
            this.exciteCheck.Location = new System.Drawing.Point(11, 41);
            this.exciteCheck.Margin = new System.Windows.Forms.Padding(2);
            this.exciteCheck.Name = "exciteCheck";
            this.exciteCheck.Size = new System.Drawing.Size(94, 17);
            this.exciteCheck.TabIndex = 7;
            this.exciteCheck.Text = " Excite sample";
            this.exciteCheck.UseVisualStyleBackColor = true;
            // 
            // seriesLengthLabel
            // 
            this.seriesLengthLabel.AutoSize = true;
            this.seriesLengthLabel.Location = new System.Drawing.Point(8, 19);
            this.seriesLengthLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.seriesLengthLabel.Name = "seriesLengthLabel";
            this.seriesLengthLabel.Size = new System.Drawing.Size(72, 13);
            this.seriesLengthLabel.TabIndex = 1;
            this.seriesLengthLabel.Text = "Series Length";
            // 
            // seriesLength
            // 
            this.seriesLength.Location = new System.Drawing.Point(84, 17);
            this.seriesLength.Margin = new System.Windows.Forms.Padding(2);
            this.seriesLength.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.seriesLength.Name = "seriesLength";
            this.seriesLength.Size = new System.Drawing.Size(40, 20);
            this.seriesLength.TabIndex = 0;
            this.seriesLength.Value = new decimal(new int[] {
            180,
            0,
            0,
            0});
            // 
            // RightTop
            // 
            this.RightTop.Controls.Add(this.CameraConfigBox);
            this.RightTop.Controls.Add(this.commandsGroupBox);
            this.RightTop.Controls.Add(this.ReportBox);
            this.RightTop.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightTop.Location = new System.Drawing.Point(656, 0);
            this.RightTop.Name = "RightTop";
            this.RightTop.Size = new System.Drawing.Size(216, 312);
            this.RightTop.TabIndex = 7;
            // 
            // CameraConfigBox
            // 
            this.CameraConfigBox.AutoSize = true;
            this.CameraConfigBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CameraConfigBox.Controls.Add(this.panel5);
            this.CameraConfigBox.Controls.Add(this.ReadModes);
            this.CameraConfigBox.Controls.Add(this.panel3);
            this.CameraConfigBox.Controls.Add(this.panel1);
            this.CameraConfigBox.Controls.Add(this.panel4);
            this.CameraConfigBox.Controls.Add(this.panel2);
            this.CameraConfigBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.CameraConfigBox.Location = new System.Drawing.Point(0, 139);
            this.CameraConfigBox.Name = "CameraConfigBox";
            this.CameraConfigBox.Size = new System.Drawing.Size(216, 143);
            this.CameraConfigBox.TabIndex = 9;
            this.CameraConfigBox.TabStop = false;
            this.CameraConfigBox.Text = "Camera Configuration";
            // 
            // ReportBox
            // 
            this.ReportBox.AutoSize = true;
            this.ReportBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ReportBox.Controls.Add(this.CameraStatus);
            this.ReportBox.Controls.Add(this.label7);
            this.ReportBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.ReportBox.Location = new System.Drawing.Point(0, 0);
            this.ReportBox.Name = "ReportBox";
            this.ReportBox.Size = new System.Drawing.Size(216, 52);
            this.ReportBox.TabIndex = 9;
            this.ReportBox.TabStop = false;
            this.ReportBox.Text = "Report";
            // 
            // CameraStatus
            // 
            this.CameraStatus.Location = new System.Drawing.Point(88, 13);
            this.CameraStatus.Name = "CameraStatus";
            this.CameraStatus.ReadOnly = true;
            this.CameraStatus.Size = new System.Drawing.Size(100, 20);
            this.CameraStatus.TabIndex = 1;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Camera Status";
            // 
            // Top
            // 
            this.Top.Controls.Add(this.specGraph);
            this.Top.Controls.Add(this.GraphScroll);
            this.Top.Controls.Add(this.RightTop);
            this.Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.Top.Location = new System.Drawing.Point(0, 0);
            this.Top.Name = "Top";
            this.Top.Size = new System.Drawing.Size(872, 312);
            this.Top.TabIndex = 8;
            // 
            // GraphScroll
            // 
            this.GraphScroll.Dock = System.Windows.Forms.DockStyle.Right;
            this.GraphScroll.Location = new System.Drawing.Point(639, 0);
            this.GraphScroll.Name = "GraphScroll";
            this.GraphScroll.Size = new System.Drawing.Size(17, 312);
            this.GraphScroll.TabIndex = 8;
            this.GraphScroll.Scroll += new System.Windows.Forms.ScrollEventHandler(this.GraphScroll_Scroll);
            // 
            // DetectorTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 579);
            this.Controls.Add(this.kineticSeriesGroupBox);
            this.Controls.Add(this.imageBox);
            this.Controls.Add(this.Top);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DetectorTestForm";
            this.Text = "DetectorTester";
            ((System.ComponentModel.ISupportInitialize)(this.specGraph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.commandsGroupBox.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraTemp)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LastRow)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraGain)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FirstRow)).EndInit();
            this.ReadModes.ResumeLayout(false);
            this.ReadModes.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NScans)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VBin)).EndInit();
            this.kineticSeriesGroupBox.ResumeLayout(false);
            this.kineticSeriesGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seriesLength)).EndInit();
            this.RightTop.ResumeLayout(false);
            this.RightTop.PerformLayout();
            this.CameraConfigBox.ResumeLayout(false);
            this.CameraConfigBox.PerformLayout();
            this.ReportBox.ResumeLayout(false);
            this.ReportBox.PerformLayout();
            this.Top.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart specGraph;
        private System.Windows.Forms.PictureBox imageBox;
        private System.Windows.Forms.Button specButton;
        private System.Windows.Forms.Button darkButton;
        private System.Windows.Forms.GroupBox commandsGroupBox;
        private System.Windows.Forms.Button blankButton;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.GroupBox kineticSeriesGroupBox;
        private System.Windows.Forms.Label seriesLengthLabel;
        private System.Windows.Forms.NumericUpDown seriesLength;
        private System.Windows.Forms.RadioButton imageAcqButton;
        private System.Windows.Forms.RadioButton fvbButton;
        private System.Windows.Forms.CheckBox exciteCheck;
        private System.Windows.Forms.Panel RightTop;
        private System.Windows.Forms.Panel Top;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown VBin;
        private System.Windows.Forms.FlowLayoutPanel ReadModes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown LastRow;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown FirstRow;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown CameraGain;
        private System.Windows.Forms.VScrollBar GraphScroll;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.NumericUpDown CameraTemp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.NumericUpDown NScans;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox ReportBox;
        private System.Windows.Forms.TextBox CameraStatus;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox CameraConfigBox;
        private System.Windows.Forms.Button Abort;
        private System.Windows.Forms.Button Pause;
    }
}


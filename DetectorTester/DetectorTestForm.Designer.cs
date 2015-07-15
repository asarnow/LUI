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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DetectorTestForm));
            this.imageBox = new System.Windows.Forms.PictureBox();
            this.specButton = new System.Windows.Forms.Button();
            this.darkButton = new System.Windows.Forms.Button();
            this.commandsGroupBox = new System.Windows.Forms.GroupBox();
            this.Abort = new System.Windows.Forms.Button();
            this.Pause = new System.Windows.Forms.Button();
            this.Clear = new System.Windows.Forms.Button();
            this.saveButton = new System.Windows.Forms.Button();
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
            this.exciteCheck = new System.Windows.Forms.CheckBox();
            this.RightTop = new System.Windows.Forms.Panel();
            this.CameraConfigBox = new System.Windows.Forms.GroupBox();
            this.AcqMethods = new System.Windows.Forms.ComboBox();
            this.ReportBox = new System.Windows.Forms.GroupBox();
            this.ScanProgress = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.CameraStatus = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Top = new System.Windows.Forms.Panel();
            this.Graph = new LUI.controls.GraphControl();
            this.GraphScroll = new System.Windows.Forms.VScrollBar();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fIleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ScrollTip = new System.Windows.Forms.ToolTip(this.components);
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
            this.RightTop.SuspendLayout();
            this.CameraConfigBox.SuspendLayout();
            this.ReportBox.SuspendLayout();
            this.Top.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageBox
            // 
            this.imageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox.Location = new System.Drawing.Point(0, 336);
            this.imageBox.Margin = new System.Windows.Forms.Padding(2);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(872, 243);
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
            this.specButton.Click += new System.EventHandler(this.captureButton_Click);
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
            this.commandsGroupBox.Controls.Add(this.saveButton);
            this.commandsGroupBox.Controls.Add(this.specButton);
            this.commandsGroupBox.Controls.Add(this.darkButton);
            this.commandsGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.commandsGroupBox.Location = new System.Drawing.Point(0, 78);
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
            this.Pause.Click += new System.EventHandler(this.Pause_Click);
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(4, 45);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(62, 24);
            this.Clear.TabIndex = 6;
            this.Clear.Text = "Clear";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(70, 45);
            this.saveButton.Margin = new System.Windows.Forms.Padding(2);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(62, 24);
            this.saveButton.TabIndex = 5;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // panel5
            // 
            this.panel5.AutoSize = true;
            this.panel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel5.Controls.Add(this.CameraTemp);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Location = new System.Drawing.Point(83, 67);
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
            this.panel3.Location = new System.Drawing.Point(109, 119);
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
            this.panel4.Location = new System.Drawing.Point(6, 67);
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
            this.panel2.Location = new System.Drawing.Point(6, 119);
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
            this.ReadModes.Location = new System.Drawing.Point(6, 19);
            this.ReadModes.Name = "ReadModes";
            this.ReadModes.Size = new System.Drawing.Size(107, 21);
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
            this.imageAcqButton.CheckedChanged += new System.EventHandler(this.imageAcqButton_CheckedChanged);
            // 
            // panel6
            // 
            this.panel6.AutoSize = true;
            this.panel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel6.Controls.Add(this.NScans);
            this.panel6.Controls.Add(this.label6);
            this.panel6.Location = new System.Drawing.Point(6, 43);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(97, 21);
            this.panel6.TabIndex = 16;
            // 
            // NScans
            // 
            this.NScans.Location = new System.Drawing.Point(43, -1);
            this.NScans.Margin = new System.Windows.Forms.Padding(2);
            this.NScans.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.NScans.Name = "NScans";
            this.NScans.Size = new System.Drawing.Size(52, 20);
            this.NScans.TabIndex = 14;
            this.NScans.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
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
            this.panel1.Location = new System.Drawing.Point(6, 93);
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
            // exciteCheck
            // 
            this.exciteCheck.AutoSize = true;
            this.exciteCheck.Location = new System.Drawing.Point(108, 43);
            this.exciteCheck.Margin = new System.Windows.Forms.Padding(2);
            this.exciteCheck.Name = "exciteCheck";
            this.exciteCheck.Size = new System.Drawing.Size(94, 17);
            this.exciteCheck.TabIndex = 7;
            this.exciteCheck.Text = " Excite sample";
            this.exciteCheck.UseVisualStyleBackColor = true;
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
            this.CameraConfigBox.Controls.Add(this.exciteCheck);
            this.CameraConfigBox.Controls.Add(this.AcqMethods);
            this.CameraConfigBox.Controls.Add(this.panel5);
            this.CameraConfigBox.Controls.Add(this.ReadModes);
            this.CameraConfigBox.Controls.Add(this.panel6);
            this.CameraConfigBox.Controls.Add(this.panel3);
            this.CameraConfigBox.Controls.Add(this.panel1);
            this.CameraConfigBox.Controls.Add(this.panel4);
            this.CameraConfigBox.Controls.Add(this.panel2);
            this.CameraConfigBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.CameraConfigBox.Location = new System.Drawing.Point(0, 165);
            this.CameraConfigBox.Name = "CameraConfigBox";
            this.CameraConfigBox.Size = new System.Drawing.Size(216, 158);
            this.CameraConfigBox.TabIndex = 9;
            this.CameraConfigBox.TabStop = false;
            this.CameraConfigBox.Text = "Camera Configuration";
            // 
            // AcqMethods
            // 
            this.AcqMethods.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AcqMethods.FormattingEnabled = true;
            this.AcqMethods.Items.AddRange(new object[] {
            "GetAcquiredData",
            "GetMostRecentImage"});
            this.AcqMethods.Location = new System.Drawing.Point(116, 20);
            this.AcqMethods.Name = "AcqMethods";
            this.AcqMethods.Size = new System.Drawing.Size(86, 21);
            this.AcqMethods.TabIndex = 17;
            // 
            // ReportBox
            // 
            this.ReportBox.AutoSize = true;
            this.ReportBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ReportBox.Controls.Add(this.ScanProgress);
            this.ReportBox.Controls.Add(this.label8);
            this.ReportBox.Controls.Add(this.CameraStatus);
            this.ReportBox.Controls.Add(this.label7);
            this.ReportBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.ReportBox.Location = new System.Drawing.Point(0, 0);
            this.ReportBox.Name = "ReportBox";
            this.ReportBox.Size = new System.Drawing.Size(216, 78);
            this.ReportBox.TabIndex = 9;
            this.ReportBox.TabStop = false;
            this.ReportBox.Text = "Report";
            // 
            // ScanProgress
            // 
            this.ScanProgress.Location = new System.Drawing.Point(102, 39);
            this.ScanProgress.Name = "ScanProgress";
            this.ScanProgress.ReadOnly = true;
            this.ScanProgress.Size = new System.Drawing.Size(86, 20);
            this.ScanProgress.TabIndex = 3;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(90, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Scans Completed";
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
            this.Top.Controls.Add(this.Graph);
            this.Top.Controls.Add(this.GraphScroll);
            this.Top.Controls.Add(this.RightTop);
            this.Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.Top.Location = new System.Drawing.Point(0, 24);
            this.Top.Name = "Top";
            this.Top.Size = new System.Drawing.Size(872, 312);
            this.Top.TabIndex = 8;
            // 
            // Graph
            // 
            this.Graph.AxesColor = System.Drawing.Color.Blue;
            this.Graph.AxesFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Graph.AxesFontColor = System.Drawing.Color.Black;
            this.Graph.ColorOrder = ((System.Collections.Generic.List<System.Drawing.Color>)(resources.GetObject("Graph.ColorOrder")));
            this.Graph.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Graph.InitialScaleHeight = 0F;
            this.Graph.InitialXLeft = 0F;
            this.Graph.InitialXRight = 1023F;
            this.Graph.InitialYMax = float.PositiveInfinity;
            this.Graph.InitialYMin = float.NegativeInfinity;
            this.Graph.Location = new System.Drawing.Point(0, 0);
            this.Graph.Marker = "*";
            this.Graph.MarkerColor = System.Drawing.Color.Blue;
            this.Graph.MarkerFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Graph.Name = "Graph";
            this.Graph.NXTicks = 10;
            this.Graph.NYTicks = 10;
            this.Graph.PadBottom = 0.01F;
            this.Graph.PadLeft = 0.01F;
            this.Graph.PadRight = 0.01F;
            this.Graph.PadTop = 0.01F;
            this.Graph.ScaleHeight = 0F;
            this.Graph.Size = new System.Drawing.Size(639, 312);
            this.Graph.TabIndex = 9;
            this.Graph.XAxisHeight = 0.1F;
            this.Graph.XLabelFormat = "f0";
            this.Graph.XLeft = 0F;
            this.Graph.XRight = 1023F;
            this.Graph.YAxisWidth = 0.05F;
            this.Graph.YLabelFormat = "n3";
            this.Graph.YMax = float.PositiveInfinity;
            this.Graph.YMin = float.NegativeInfinity;
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
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fIleToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(872, 24);
            this.menuStrip1.TabIndex = 9;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fIleToolStripMenuItem
            // 
            this.fIleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveAsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fIleToolStripMenuItem.Name = "fIleToolStripMenuItem";
            this.fIleToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fIleToolStripMenuItem.Text = "File";
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.saveAsToolStripMenuItem.Text = "Save As...";
            this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.saveAsToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // DetectorTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(872, 579);
            this.Controls.Add(this.imageBox);
            this.Controls.Add(this.Top);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DetectorTestForm";
            this.Text = "DetectorTester";
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
            this.RightTop.ResumeLayout(false);
            this.RightTop.PerformLayout();
            this.CameraConfigBox.ResumeLayout(false);
            this.CameraConfigBox.PerformLayout();
            this.ReportBox.ResumeLayout(false);
            this.ReportBox.PerformLayout();
            this.Top.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox imageBox;
        private System.Windows.Forms.Button specButton;
        private System.Windows.Forms.Button darkButton;
        private System.Windows.Forms.GroupBox commandsGroupBox;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button Clear;
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
        private System.Windows.Forms.ComboBox AcqMethods;
        private System.Windows.Forms.TextBox ScanProgress;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fIleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private LUI.controls.GraphControl Graph;
        private System.Windows.Forms.ToolTip ScrollTip;
    }
}


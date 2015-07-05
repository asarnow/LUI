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
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.panel2 = new System.Windows.Forms.Panel();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.ReadModes = new System.Windows.Forms.FlowLayoutPanel();
            this.fvbButton = new System.Windows.Forms.RadioButton();
            this.imageAcqButton = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.VBin = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.clearButton = new System.Windows.Forms.Button();
            this.blankButton = new System.Windows.Forms.Button();
            this.kineticSeriesGroupBox = new System.Windows.Forms.GroupBox();
            this.abortButton = new System.Windows.Forms.Button();
            this.exciteCheck = new System.Windows.Forms.CheckBox();
            this.startButton = new System.Windows.Forms.Button();
            this.seriesLengthLabel = new System.Windows.Forms.Label();
            this.seriesLength = new System.Windows.Forms.NumericUpDown();
            this.RightTop = new System.Windows.Forms.Panel();
            this.Top = new System.Windows.Forms.Panel();
            this.GraphScroll = new System.Windows.Forms.VScrollBar();
            ((System.ComponentModel.ISupportInitialize)(this.specGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.commandsGroupBox.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.ReadModes.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VBin)).BeginInit();
            this.kineticSeriesGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seriesLength)).BeginInit();
            this.RightTop.SuspendLayout();
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
            this.specGraph.Size = new System.Drawing.Size(665, 312);
            this.specGraph.TabIndex = 0;
            this.specGraph.Text = "chart1";
            // 
            // imageBox
            // 
            this.imageBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imageBox.Location = new System.Drawing.Point(0, 312);
            this.imageBox.Margin = new System.Windows.Forms.Padding(2);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(831, 267);
            this.imageBox.TabIndex = 1;
            this.imageBox.TabStop = false;
            // 
            // specButton
            // 
            this.specButton.Location = new System.Drawing.Point(11, 25);
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
            this.darkButton.Location = new System.Drawing.Point(77, 25);
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
            this.commandsGroupBox.Controls.Add(this.panel3);
            this.commandsGroupBox.Controls.Add(this.panel4);
            this.commandsGroupBox.Controls.Add(this.panel2);
            this.commandsGroupBox.Controls.Add(this.ReadModes);
            this.commandsGroupBox.Controls.Add(this.panel1);
            this.commandsGroupBox.Controls.Add(this.clearButton);
            this.commandsGroupBox.Controls.Add(this.blankButton);
            this.commandsGroupBox.Controls.Add(this.specButton);
            this.commandsGroupBox.Controls.Add(this.darkButton);
            this.commandsGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.commandsGroupBox.Location = new System.Drawing.Point(0, 0);
            this.commandsGroupBox.Margin = new System.Windows.Forms.Padding(2);
            this.commandsGroupBox.Name = "commandsGroupBox";
            this.commandsGroupBox.Padding = new System.Windows.Forms.Padding(2);
            this.commandsGroupBox.Size = new System.Drawing.Size(149, 243);
            this.commandsGroupBox.TabIndex = 5;
            this.commandsGroupBox.TabStop = false;
            this.commandsGroupBox.Text = "Commands";
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.numericUpDown2);
            this.panel3.Location = new System.Drawing.Point(23, 197);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(116, 28);
            this.panel3.TabIndex = 11;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 8);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Last Row";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(74, 6);
            this.numericUpDown2.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown2.TabIndex = 14;
            // 
            // panel4
            // 
            this.panel4.AutoSize = true;
            this.panel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.numericUpDown3);
            this.panel4.Location = new System.Drawing.Point(11, 109);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(77, 22);
            this.panel4.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(2, 2);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 15;
            this.label4.Text = "Gain";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(35, 0);
            this.numericUpDown3.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown3.TabIndex = 14;
            // 
            // panel2
            // 
            this.panel2.AutoSize = true;
            this.panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel2.Controls.Add(this.numericUpDown1);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(24, 167);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(115, 24);
            this.panel2.TabIndex = 10;
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(73, 2);
            this.numericUpDown1.Margin = new System.Windows.Forms.Padding(2);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(40, 20);
            this.numericUpDown1.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 4);
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
            this.ReadModes.Location = new System.Drawing.Point(11, 82);
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
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.VBin);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(11, 137);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(128, 24);
            this.panel1.TabIndex = 9;
            // 
            // VBin
            // 
            this.VBin.Location = new System.Drawing.Point(86, 2);
            this.VBin.Margin = new System.Windows.Forms.Padding(2);
            this.VBin.Name = "VBin";
            this.VBin.Size = new System.Drawing.Size(40, 20);
            this.VBin.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 4);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Vertical Binning";
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(77, 53);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(62, 24);
            this.clearButton.TabIndex = 6;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // blankButton
            // 
            this.blankButton.Location = new System.Drawing.Point(11, 53);
            this.blankButton.Margin = new System.Windows.Forms.Padding(2);
            this.blankButton.Name = "blankButton";
            this.blankButton.Size = new System.Drawing.Size(62, 24);
            this.blankButton.TabIndex = 5;
            this.blankButton.Text = "Blank";
            this.blankButton.UseVisualStyleBackColor = true;
            this.blankButton.Click += new System.EventHandler(this.blankButton_Click);
            // 
            // kineticSeriesGroupBox
            // 
            this.kineticSeriesGroupBox.Controls.Add(this.abortButton);
            this.kineticSeriesGroupBox.Controls.Add(this.exciteCheck);
            this.kineticSeriesGroupBox.Controls.Add(this.startButton);
            this.kineticSeriesGroupBox.Controls.Add(this.seriesLengthLabel);
            this.kineticSeriesGroupBox.Controls.Add(this.seriesLength);
            this.kineticSeriesGroupBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.kineticSeriesGroupBox.Location = new System.Drawing.Point(0, 243);
            this.kineticSeriesGroupBox.Margin = new System.Windows.Forms.Padding(2);
            this.kineticSeriesGroupBox.Name = "kineticSeriesGroupBox";
            this.kineticSeriesGroupBox.Padding = new System.Windows.Forms.Padding(2);
            this.kineticSeriesGroupBox.Size = new System.Drawing.Size(149, 70);
            this.kineticSeriesGroupBox.TabIndex = 6;
            this.kineticSeriesGroupBox.TabStop = false;
            this.kineticSeriesGroupBox.Text = "Kinetic Series";
            // 
            // abortButton
            // 
            this.abortButton.Location = new System.Drawing.Point(8, 148);
            this.abortButton.Margin = new System.Windows.Forms.Padding(2);
            this.abortButton.Name = "abortButton";
            this.abortButton.Size = new System.Drawing.Size(62, 24);
            this.abortButton.TabIndex = 7;
            this.abortButton.Text = "Abort";
            this.abortButton.UseVisualStyleBackColor = true;
            this.abortButton.Click += new System.EventHandler(this.abortButton_Click);
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
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(76, 148);
            this.startButton.Margin = new System.Windows.Forms.Padding(2);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(62, 24);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
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
            this.RightTop.Controls.Add(this.kineticSeriesGroupBox);
            this.RightTop.Controls.Add(this.commandsGroupBox);
            this.RightTop.Dock = System.Windows.Forms.DockStyle.Right;
            this.RightTop.Location = new System.Drawing.Point(682, 0);
            this.RightTop.Name = "RightTop";
            this.RightTop.Size = new System.Drawing.Size(149, 312);
            this.RightTop.TabIndex = 7;
            // 
            // Top
            // 
            this.Top.Controls.Add(this.specGraph);
            this.Top.Controls.Add(this.GraphScroll);
            this.Top.Controls.Add(this.RightTop);
            this.Top.Dock = System.Windows.Forms.DockStyle.Top;
            this.Top.Location = new System.Drawing.Point(0, 0);
            this.Top.Name = "Top";
            this.Top.Size = new System.Drawing.Size(831, 312);
            this.Top.TabIndex = 8;
            // 
            // GraphScroll
            // 
            this.GraphScroll.Dock = System.Windows.Forms.DockStyle.Right;
            this.GraphScroll.Location = new System.Drawing.Point(665, 0);
            this.GraphScroll.Name = "GraphScroll";
            this.GraphScroll.Size = new System.Drawing.Size(17, 312);
            this.GraphScroll.TabIndex = 8;
            // 
            // DetectorTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 579);
            this.Controls.Add(this.imageBox);
            this.Controls.Add(this.Top);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DetectorTestForm";
            this.Text = "DetectorTester";
            ((System.ComponentModel.ISupportInitialize)(this.specGraph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.commandsGroupBox.ResumeLayout(false);
            this.commandsGroupBox.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.ReadModes.ResumeLayout(false);
            this.ReadModes.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VBin)).EndInit();
            this.kineticSeriesGroupBox.ResumeLayout(false);
            this.kineticSeriesGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seriesLength)).EndInit();
            this.RightTop.ResumeLayout(false);
            this.RightTop.PerformLayout();
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
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.GroupBox kineticSeriesGroupBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Label seriesLengthLabel;
        private System.Windows.Forms.NumericUpDown seriesLength;
        private System.Windows.Forms.RadioButton imageAcqButton;
        private System.Windows.Forms.RadioButton fvbButton;
        private System.Windows.Forms.CheckBox exciteCheck;
        private System.Windows.Forms.Button abortButton;
        private System.Windows.Forms.Panel RightTop;
        private System.Windows.Forms.Panel Top;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown VBin;
        private System.Windows.Forms.FlowLayoutPanel ReadModes;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.VScrollBar GraphScroll;
    }
}


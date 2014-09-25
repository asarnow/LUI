namespace DetectorTester
{
    partial class Form1
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
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.specGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.imageBox = new System.Windows.Forms.PictureBox();
            this.specButton = new System.Windows.Forms.Button();
            this.imageButton = new System.Windows.Forms.Button();
            this.darkButton = new System.Windows.Forms.Button();
            this.commandsGroupBox = new System.Windows.Forms.GroupBox();
            this.clearButton = new System.Windows.Forms.Button();
            this.blankButton = new System.Windows.Forms.Button();
            this.kineticSeriesGroupBox = new System.Windows.Forms.GroupBox();
            this.abortButton = new System.Windows.Forms.Button();
            this.acqModePanel = new System.Windows.Forms.Panel();
            this.imageAcqButton = new System.Windows.Forms.RadioButton();
            this.acqModeLabel = new System.Windows.Forms.Label();
            this.fvbButton = new System.Windows.Forms.RadioButton();
            this.exciteCheck = new System.Windows.Forms.CheckBox();
            this.startButton = new System.Windows.Forms.Button();
            this.seriesLengthLabel = new System.Windows.Forms.Label();
            this.seriesLength = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.specGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.commandsGroupBox.SuspendLayout();
            this.kineticSeriesGroupBox.SuspendLayout();
            this.acqModePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seriesLength)).BeginInit();
            this.SuspendLayout();
            // 
            // specGraph
            // 
            chartArea2.AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea2.AxisX.Maximum = 1023D;
            chartArea2.AxisX.Minimum = 0D;
            chartArea2.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea2.AxisY.IntervalOffsetType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea2.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea2.AxisY.Maximum = 65536D;
            chartArea2.AxisY.Minimum = 0D;
            chartArea2.Name = "specArea";
            this.specGraph.ChartAreas.Add(chartArea2);
            this.specGraph.Location = new System.Drawing.Point(12, 12);
            this.specGraph.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.specGraph.Name = "specGraph";
            series2.ChartArea = "specArea";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Name = "spec";
            this.specGraph.Series.Add(series2);
            this.specGraph.Size = new System.Drawing.Size(819, 366);
            this.specGraph.TabIndex = 0;
            this.specGraph.Text = "chart1";
            // 
            // imageBox
            // 
            this.imageBox.Location = new System.Drawing.Point(12, 384);
            this.imageBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(1024, 256);
            this.imageBox.TabIndex = 1;
            this.imageBox.TabStop = false;
            // 
            // specButton
            // 
            this.specButton.Location = new System.Drawing.Point(13, 66);
            this.specButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.specButton.Name = "specButton";
            this.specButton.Size = new System.Drawing.Size(83, 30);
            this.specButton.TabIndex = 2;
            this.specButton.Text = "Capture";
            this.specButton.UseVisualStyleBackColor = true;
            this.specButton.Click += new System.EventHandler(this.specButton_Click);
            // 
            // imageButton
            // 
            this.imageButton.Location = new System.Drawing.Point(13, 31);
            this.imageButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.imageButton.Name = "imageButton";
            this.imageButton.Size = new System.Drawing.Size(83, 30);
            this.imageButton.TabIndex = 3;
            this.imageButton.Text = "Image";
            this.imageButton.UseVisualStyleBackColor = true;
            this.imageButton.Click += new System.EventHandler(this.imageButton_Click);
            // 
            // darkButton
            // 
            this.darkButton.Location = new System.Drawing.Point(103, 31);
            this.darkButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.darkButton.Name = "darkButton";
            this.darkButton.Size = new System.Drawing.Size(83, 30);
            this.darkButton.TabIndex = 4;
            this.darkButton.Text = "Dark";
            this.darkButton.UseVisualStyleBackColor = true;
            this.darkButton.Click += new System.EventHandler(this.darkButton_Click);
            // 
            // commandsGroupBox
            // 
            this.commandsGroupBox.Controls.Add(this.clearButton);
            this.commandsGroupBox.Controls.Add(this.blankButton);
            this.commandsGroupBox.Controls.Add(this.imageButton);
            this.commandsGroupBox.Controls.Add(this.specButton);
            this.commandsGroupBox.Controls.Add(this.darkButton);
            this.commandsGroupBox.Location = new System.Drawing.Point(836, 12);
            this.commandsGroupBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.commandsGroupBox.Name = "commandsGroupBox";
            this.commandsGroupBox.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.commandsGroupBox.Size = new System.Drawing.Size(200, 142);
            this.commandsGroupBox.TabIndex = 5;
            this.commandsGroupBox.TabStop = false;
            this.commandsGroupBox.Text = "Commands";
            // 
            // clearButton
            // 
            this.clearButton.Location = new System.Drawing.Point(13, 102);
            this.clearButton.Margin = new System.Windows.Forms.Padding(4);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(83, 30);
            this.clearButton.TabIndex = 6;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
            // 
            // blankButton
            // 
            this.blankButton.Location = new System.Drawing.Point(103, 66);
            this.blankButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.blankButton.Name = "blankButton";
            this.blankButton.Size = new System.Drawing.Size(83, 30);
            this.blankButton.TabIndex = 5;
            this.blankButton.Text = "Blank";
            this.blankButton.UseVisualStyleBackColor = true;
            this.blankButton.Click += new System.EventHandler(this.blankButton_Click);
            // 
            // kineticSeriesGroupBox
            // 
            this.kineticSeriesGroupBox.Controls.Add(this.abortButton);
            this.kineticSeriesGroupBox.Controls.Add(this.acqModePanel);
            this.kineticSeriesGroupBox.Controls.Add(this.exciteCheck);
            this.kineticSeriesGroupBox.Controls.Add(this.startButton);
            this.kineticSeriesGroupBox.Controls.Add(this.seriesLengthLabel);
            this.kineticSeriesGroupBox.Controls.Add(this.seriesLength);
            this.kineticSeriesGroupBox.Location = new System.Drawing.Point(838, 160);
            this.kineticSeriesGroupBox.Name = "kineticSeriesGroupBox";
            this.kineticSeriesGroupBox.Size = new System.Drawing.Size(200, 218);
            this.kineticSeriesGroupBox.TabIndex = 6;
            this.kineticSeriesGroupBox.TabStop = false;
            this.kineticSeriesGroupBox.Text = "Kinetic Series";
            // 
            // abortButton
            // 
            this.abortButton.Location = new System.Drawing.Point(11, 182);
            this.abortButton.Name = "abortButton";
            this.abortButton.Size = new System.Drawing.Size(83, 30);
            this.abortButton.TabIndex = 7;
            this.abortButton.Text = "Abort";
            this.abortButton.UseVisualStyleBackColor = true;
            this.abortButton.Click += new System.EventHandler(this.abortButton_Click);
            // 
            // acqModePanel
            // 
            this.acqModePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.acqModePanel.Controls.Add(this.imageAcqButton);
            this.acqModePanel.Controls.Add(this.acqModeLabel);
            this.acqModePanel.Controls.Add(this.fvbButton);
            this.acqModePanel.Location = new System.Drawing.Point(6, 21);
            this.acqModePanel.Name = "acqModePanel";
            this.acqModePanel.Size = new System.Drawing.Size(103, 76);
            this.acqModePanel.TabIndex = 9;
            // 
            // imageAcqButton
            // 
            this.imageAcqButton.AutoSize = true;
            this.imageAcqButton.Location = new System.Drawing.Point(3, 47);
            this.imageAcqButton.Name = "imageAcqButton";
            this.imageAcqButton.Size = new System.Drawing.Size(67, 21);
            this.imageAcqButton.TabIndex = 8;
            this.imageAcqButton.TabStop = true;
            this.imageAcqButton.Text = "Image";
            this.imageAcqButton.UseVisualStyleBackColor = true;
            // 
            // acqModeLabel
            // 
            this.acqModeLabel.AutoSize = true;
            this.acqModeLabel.Location = new System.Drawing.Point(0, 0);
            this.acqModeLabel.Name = "acqModeLabel";
            this.acqModeLabel.Size = new System.Drawing.Size(71, 17);
            this.acqModeLabel.TabIndex = 0;
            this.acqModeLabel.Text = "Acq Mode";
            // 
            // fvbButton
            // 
            this.fvbButton.AutoSize = true;
            this.fvbButton.Location = new System.Drawing.Point(3, 20);
            this.fvbButton.Name = "fvbButton";
            this.fvbButton.Size = new System.Drawing.Size(55, 21);
            this.fvbButton.TabIndex = 7;
            this.fvbButton.TabStop = true;
            this.fvbButton.Text = "FVB";
            this.fvbButton.UseVisualStyleBackColor = true;
            // 
            // exciteCheck
            // 
            this.exciteCheck.AutoSize = true;
            this.exciteCheck.Location = new System.Drawing.Point(115, 21);
            this.exciteCheck.Name = "exciteCheck";
            this.exciteCheck.Size = new System.Drawing.Size(79, 38);
            this.exciteCheck.TabIndex = 7;
            this.exciteCheck.Text = " Excite\r\n sample";
            this.exciteCheck.UseVisualStyleBackColor = true;
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(101, 182);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(83, 30);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // seriesLengthLabel
            // 
            this.seriesLengthLabel.AutoSize = true;
            this.seriesLengthLabel.Location = new System.Drawing.Point(6, 102);
            this.seriesLengthLabel.Name = "seriesLengthLabel";
            this.seriesLengthLabel.Size = new System.Drawing.Size(96, 17);
            this.seriesLengthLabel.TabIndex = 1;
            this.seriesLengthLabel.Text = "Series Length";
            // 
            // seriesLength
            // 
            this.seriesLength.Location = new System.Drawing.Point(108, 100);
            this.seriesLength.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.seriesLength.Name = "seriesLength";
            this.seriesLength.Size = new System.Drawing.Size(54, 22);
            this.seriesLength.TabIndex = 0;
            this.seriesLength.Value = new decimal(new int[] {
            180,
            0,
            0,
            0});
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1043, 649);
            this.Controls.Add(this.kineticSeriesGroupBox);
            this.Controls.Add(this.commandsGroupBox);
            this.Controls.Add(this.imageBox);
            this.Controls.Add(this.specGraph);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "DetectorTester";
            ((System.ComponentModel.ISupportInitialize)(this.specGraph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.commandsGroupBox.ResumeLayout(false);
            this.kineticSeriesGroupBox.ResumeLayout(false);
            this.kineticSeriesGroupBox.PerformLayout();
            this.acqModePanel.ResumeLayout(false);
            this.acqModePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seriesLength)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart specGraph;
        private System.Windows.Forms.PictureBox imageBox;
        private System.Windows.Forms.Button specButton;
        private System.Windows.Forms.Button imageButton;
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
        private System.Windows.Forms.Panel acqModePanel;
        private System.Windows.Forms.Label acqModeLabel;
        private System.Windows.Forms.Button abortButton;
    }
}


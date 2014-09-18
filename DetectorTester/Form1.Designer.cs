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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.specGraph = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.imageBox = new System.Windows.Forms.PictureBox();
            this.specButton = new System.Windows.Forms.Button();
            this.imageButton = new System.Windows.Forms.Button();
            this.darkButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.blankButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.specGraph)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.groupBox1.SuspendLayout();
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
            this.specGraph.Location = new System.Drawing.Point(12, 12);
            this.specGraph.Name = "specGraph";
            series1.ChartArea = "specArea";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Name = "spec";
            this.specGraph.Series.Add(series1);
            this.specGraph.Size = new System.Drawing.Size(818, 366);
            this.specGraph.TabIndex = 0;
            this.specGraph.Text = "chart1";
            // 
            // imageBox
            // 
            this.imageBox.Location = new System.Drawing.Point(12, 384);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(1024, 256);
            this.imageBox.TabIndex = 1;
            this.imageBox.TabStop = false;
            // 
            // specButton
            // 
            this.specButton.Location = new System.Drawing.Point(14, 67);
            this.specButton.Name = "specButton";
            this.specButton.Size = new System.Drawing.Size(83, 30);
            this.specButton.TabIndex = 2;
            this.specButton.Text = "Capture";
            this.specButton.UseVisualStyleBackColor = true;
            this.specButton.Click += new System.EventHandler(this.specButton_Click);
            // 
            // imageButton
            // 
            this.imageButton.Location = new System.Drawing.Point(14, 31);
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
            this.darkButton.Name = "darkButton";
            this.darkButton.Size = new System.Drawing.Size(83, 30);
            this.darkButton.TabIndex = 4;
            this.darkButton.Text = "Dark";
            this.darkButton.UseVisualStyleBackColor = true;
            this.darkButton.Click += new System.EventHandler(this.darkButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.blankButton);
            this.groupBox1.Controls.Add(this.imageButton);
            this.groupBox1.Controls.Add(this.specButton);
            this.groupBox1.Controls.Add(this.darkButton);
            this.groupBox1.Location = new System.Drawing.Point(836, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 366);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Commands";
            // 
            // blankButton
            // 
            this.blankButton.Location = new System.Drawing.Point(103, 67);
            this.blankButton.Name = "blankButton";
            this.blankButton.Size = new System.Drawing.Size(83, 30);
            this.blankButton.TabIndex = 5;
            this.blankButton.Text = "Blank";
            this.blankButton.UseVisualStyleBackColor = true;
            this.blankButton.Click += new System.EventHandler(this.blankButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1043, 649);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.imageBox);
            this.Controls.Add(this.specGraph);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.specGraph)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart specGraph;
        private System.Windows.Forms.PictureBox imageBox;
        private System.Windows.Forms.Button specButton;
        private System.Windows.Forms.Button imageButton;
        private System.Windows.Forms.Button darkButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button blankButton;
    }
}


namespace DetectorTester
{
    partial class DelayGeneratorTestForm
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
            this.ConfigBox = new System.Windows.Forms.GroupBox();
            this.ConfigFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.GpibFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.SelectPrologix = new System.Windows.Forms.RadioButton();
            this.SelectNi = new System.Windows.Forms.RadioButton();
            this.GpibAddressFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.GpibAddress = new System.Windows.Forms.ComboBox();
            this.PrologixConfigGrid = new System.Windows.Forms.TableLayoutPanel();
            this.PrologixConfigFlowLeft = new System.Windows.Forms.FlowLayoutPanel();
            this.PadFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.Padding = new System.Windows.Forms.ComboBox();
            this.TimeoutFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.Timeout = new System.Windows.Forms.NumericUpDown();
            this.PrologixConfigFlowRight = new System.Windows.Forms.FlowLayoutPanel();
            this.BaudFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.BaudRate = new System.Windows.Forms.ComboBox();
            this.ComFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.ComPorts = new System.Windows.Forms.ComboBox();
            this.ConsoleBox = new System.Windows.Forms.GroupBox();
            this.Console = new CommandPrompt.CommandPrompt();
            this.Splitter = new System.Windows.Forms.SplitContainer();
            this.RightFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.TestButton = new System.Windows.Forms.Button();
            this.AbortButton = new System.Windows.Forms.Button();
            this.ConfigBox.SuspendLayout();
            this.ConfigFlow.SuspendLayout();
            this.GpibFlow.SuspendLayout();
            this.GpibAddressFlow.SuspendLayout();
            this.PrologixConfigGrid.SuspendLayout();
            this.PrologixConfigFlowLeft.SuspendLayout();
            this.PadFlow.SuspendLayout();
            this.TimeoutFlow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Timeout)).BeginInit();
            this.PrologixConfigFlowRight.SuspendLayout();
            this.BaudFlow.SuspendLayout();
            this.ComFlow.SuspendLayout();
            this.ConsoleBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Splitter)).BeginInit();
            this.Splitter.Panel1.SuspendLayout();
            this.Splitter.Panel2.SuspendLayout();
            this.Splitter.SuspendLayout();
            this.RightFlow.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConfigBox
            // 
            this.ConfigBox.AutoSize = true;
            this.ConfigBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ConfigBox.Controls.Add(this.ConfigFlow);
            this.ConfigBox.Location = new System.Drawing.Point(3, 3);
            this.ConfigBox.Name = "ConfigBox";
            this.ConfigBox.Size = new System.Drawing.Size(280, 159);
            this.ConfigBox.TabIndex = 0;
            this.ConfigBox.TabStop = false;
            this.ConfigBox.Text = "Configuration";
            // 
            // ConfigFlow
            // 
            this.ConfigFlow.AutoSize = true;
            this.ConfigFlow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ConfigFlow.Controls.Add(this.GpibFlow);
            this.ConfigFlow.Controls.Add(this.GpibAddressFlow);
            this.ConfigFlow.Controls.Add(this.PrologixConfigGrid);
            this.ConfigFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConfigFlow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.ConfigFlow.Location = new System.Drawing.Point(3, 16);
            this.ConfigFlow.Name = "ConfigFlow";
            this.ConfigFlow.Size = new System.Drawing.Size(274, 140);
            this.ConfigFlow.TabIndex = 1;
            // 
            // GpibFlow
            // 
            this.GpibFlow.AutoSize = true;
            this.GpibFlow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.GpibFlow.Controls.Add(this.SelectPrologix);
            this.GpibFlow.Controls.Add(this.SelectNi);
            this.GpibFlow.Location = new System.Drawing.Point(3, 3);
            this.GpibFlow.Name = "GpibFlow";
            this.GpibFlow.Size = new System.Drawing.Size(193, 23);
            this.GpibFlow.TabIndex = 3;
            // 
            // SelectPrologix
            // 
            this.SelectPrologix.AutoSize = true;
            this.SelectPrologix.Location = new System.Drawing.Point(3, 3);
            this.SelectPrologix.Name = "SelectPrologix";
            this.SelectPrologix.Size = new System.Drawing.Size(115, 17);
            this.SelectPrologix.TabIndex = 2;
            this.SelectPrologix.TabStop = true;
            this.SelectPrologix.Text = "Prologix USB-GPIB";
            this.SelectPrologix.UseVisualStyleBackColor = true;
            // 
            // SelectNi
            // 
            this.SelectNi.AutoSize = true;
            this.SelectNi.Location = new System.Drawing.Point(124, 3);
            this.SelectNi.Name = "SelectNi";
            this.SelectNi.Size = new System.Drawing.Size(66, 17);
            this.SelectNi.TabIndex = 1;
            this.SelectNi.TabStop = true;
            this.SelectNi.Text = "NI 488.2";
            this.SelectNi.UseVisualStyleBackColor = true;
            // 
            // GpibAddressFlow
            // 
            this.GpibAddressFlow.AutoSize = true;
            this.GpibAddressFlow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.GpibAddressFlow.Controls.Add(this.label3);
            this.GpibAddressFlow.Controls.Add(this.GpibAddress);
            this.GpibAddressFlow.Location = new System.Drawing.Point(3, 32);
            this.GpibAddressFlow.Name = "GpibAddressFlow";
            this.GpibAddressFlow.Size = new System.Drawing.Size(133, 27);
            this.GpibAddressFlow.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 0);
            this.label3.Name = "label3";
            this.label3.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label3.Size = new System.Drawing.Size(73, 19);
            this.label3.TabIndex = 0;
            this.label3.Text = "GPIB Address";
            // 
            // GpibAddress
            // 
            this.GpibAddress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.GpibAddress.FormattingEnabled = true;
            this.GpibAddress.Location = new System.Drawing.Point(82, 3);
            this.GpibAddress.Name = "GpibAddress";
            this.GpibAddress.Size = new System.Drawing.Size(48, 21);
            this.GpibAddress.TabIndex = 1;
            // 
            // PrologixConfigGrid
            // 
            this.PrologixConfigGrid.AutoSize = true;
            this.PrologixConfigGrid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PrologixConfigGrid.ColumnCount = 2;
            this.PrologixConfigGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PrologixConfigGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.PrologixConfigGrid.Controls.Add(this.PrologixConfigFlowLeft, 0, 0);
            this.PrologixConfigGrid.Controls.Add(this.PrologixConfigFlowRight, 1, 0);
            this.PrologixConfigGrid.Location = new System.Drawing.Point(3, 65);
            this.PrologixConfigGrid.Name = "PrologixConfigGrid";
            this.PrologixConfigGrid.RowCount = 1;
            this.PrologixConfigGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PrologixConfigGrid.Size = new System.Drawing.Size(268, 72);
            this.PrologixConfigGrid.TabIndex = 1;
            // 
            // PrologixConfigFlowLeft
            // 
            this.PrologixConfigFlowLeft.AutoSize = true;
            this.PrologixConfigFlowLeft.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PrologixConfigFlowLeft.Controls.Add(this.ComFlow);
            this.PrologixConfigFlowLeft.Controls.Add(this.BaudFlow);
            this.PrologixConfigFlowLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrologixConfigFlowLeft.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.PrologixConfigFlowLeft.Location = new System.Drawing.Point(3, 3);
            this.PrologixConfigFlowLeft.Name = "PrologixConfigFlowLeft";
            this.PrologixConfigFlowLeft.Size = new System.Drawing.Size(136, 66);
            this.PrologixConfigFlowLeft.TabIndex = 4;
            // 
            // PadFlow
            // 
            this.PadFlow.AutoSize = true;
            this.PadFlow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PadFlow.Controls.Add(this.label4);
            this.PadFlow.Controls.Add(this.Padding);
            this.PadFlow.Location = new System.Drawing.Point(3, 3);
            this.PadFlow.Name = "PadFlow";
            this.PadFlow.Size = new System.Drawing.Size(114, 27);
            this.PadFlow.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label4.Size = new System.Drawing.Size(46, 19);
            this.label4.TabIndex = 1;
            this.label4.Text = "Padding";
            // 
            // Padding
            // 
            this.Padding.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Padding.FormattingEnabled = true;
            this.Padding.Location = new System.Drawing.Point(55, 3);
            this.Padding.Name = "Padding";
            this.Padding.Size = new System.Drawing.Size(56, 21);
            this.Padding.TabIndex = 2;
            // 
            // TimeoutFlow
            // 
            this.TimeoutFlow.AutoSize = true;
            this.TimeoutFlow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.TimeoutFlow.Controls.Add(this.label5);
            this.TimeoutFlow.Controls.Add(this.Timeout);
            this.TimeoutFlow.Location = new System.Drawing.Point(3, 36);
            this.TimeoutFlow.Name = "TimeoutFlow";
            this.TimeoutFlow.Size = new System.Drawing.Size(103, 26);
            this.TimeoutFlow.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label5.Size = new System.Drawing.Size(45, 19);
            this.label5.TabIndex = 1;
            this.label5.Text = "Timeout";
            // 
            // Timeout
            // 
            this.Timeout.Location = new System.Drawing.Point(54, 3);
            this.Timeout.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.Timeout.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Timeout.Name = "Timeout";
            this.Timeout.Size = new System.Drawing.Size(46, 20);
            this.Timeout.TabIndex = 1;
            this.Timeout.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // PrologixConfigFlowRight
            // 
            this.PrologixConfigFlowRight.AutoSize = true;
            this.PrologixConfigFlowRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PrologixConfigFlowRight.Controls.Add(this.PadFlow);
            this.PrologixConfigFlowRight.Controls.Add(this.TimeoutFlow);
            this.PrologixConfigFlowRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PrologixConfigFlowRight.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.PrologixConfigFlowRight.Location = new System.Drawing.Point(145, 3);
            this.PrologixConfigFlowRight.Name = "PrologixConfigFlowRight";
            this.PrologixConfigFlowRight.Size = new System.Drawing.Size(120, 66);
            this.PrologixConfigFlowRight.TabIndex = 5;
            // 
            // BaudFlow
            // 
            this.BaudFlow.AutoSize = true;
            this.BaudFlow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BaudFlow.Controls.Add(this.label2);
            this.BaudFlow.Controls.Add(this.BaudRate);
            this.BaudFlow.Location = new System.Drawing.Point(3, 36);
            this.BaudFlow.Name = "BaudFlow";
            this.BaudFlow.Size = new System.Drawing.Size(130, 27);
            this.BaudFlow.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label2.Size = new System.Drawing.Size(58, 19);
            this.label2.TabIndex = 1;
            this.label2.Text = "Baud Rate";
            // 
            // BaudRate
            // 
            this.BaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.BaudRate.FormattingEnabled = true;
            this.BaudRate.Location = new System.Drawing.Point(67, 3);
            this.BaudRate.Name = "BaudRate";
            this.BaudRate.Size = new System.Drawing.Size(60, 21);
            this.BaudRate.TabIndex = 2;
            // 
            // ComFlow
            // 
            this.ComFlow.AutoSize = true;
            this.ComFlow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ComFlow.Controls.Add(this.label1);
            this.ComFlow.Controls.Add(this.ComPorts);
            this.ComFlow.Location = new System.Drawing.Point(3, 3);
            this.ComFlow.Name = "ComFlow";
            this.ComFlow.Size = new System.Drawing.Size(126, 27);
            this.ComFlow.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label1.Size = new System.Drawing.Size(53, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "COM Port";
            // 
            // ComPorts
            // 
            this.ComPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComPorts.FormattingEnabled = true;
            this.ComPorts.Location = new System.Drawing.Point(62, 3);
            this.ComPorts.Name = "ComPorts";
            this.ComPorts.Size = new System.Drawing.Size(61, 21);
            this.ComPorts.TabIndex = 1;
            // 
            // ConsoleBox
            // 
            this.ConsoleBox.Controls.Add(this.Console);
            this.ConsoleBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConsoleBox.Location = new System.Drawing.Point(0, 0);
            this.ConsoleBox.Name = "ConsoleBox";
            this.ConsoleBox.Size = new System.Drawing.Size(152, 262);
            this.ConsoleBox.TabIndex = 2;
            this.ConsoleBox.TabStop = false;
            this.ConsoleBox.Text = "Console";
            // 
            // Console
            // 
            this.Console.BackColor = System.Drawing.SystemColors.Window;
            this.Console.Delimiters = new char[] {
        ' '};
            this.Console.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Console.Location = new System.Drawing.Point(3, 16);
            this.Console.MessageColor = System.Drawing.SystemColors.ControlText;
            this.Console.MinimumSize = new System.Drawing.Size(0, 17);
            this.Console.Name = "Console";
            this.Console.PromptColor = System.Drawing.SystemColors.ControlText;
            this.Console.Size = new System.Drawing.Size(146, 251);
            this.Console.TabIndex = 0;
            // 
            // Splitter
            // 
            this.Splitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Splitter.Location = new System.Drawing.Point(0, 0);
            this.Splitter.Name = "Splitter";
            // 
            // Splitter.Panel1
            // 
            this.Splitter.Panel1.Controls.Add(this.ConsoleBox);
            // 
            // Splitter.Panel2
            // 
            this.Splitter.Panel2.Controls.Add(this.RightFlow);
            this.Splitter.Size = new System.Drawing.Size(460, 262);
            this.Splitter.SplitterDistance = 152;
            this.Splitter.TabIndex = 4;
            // 
            // RightFlow
            // 
            this.RightFlow.AutoSize = true;
            this.RightFlow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.RightFlow.Controls.Add(this.ConfigBox);
            this.RightFlow.Controls.Add(this.groupBox1);
            this.RightFlow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.RightFlow.Location = new System.Drawing.Point(3, 3);
            this.RightFlow.Name = "RightFlow";
            this.RightFlow.Size = new System.Drawing.Size(298, 259);
            this.RightFlow.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.AbortButton);
            this.groupBox1.Controls.Add(this.TestButton);
            this.groupBox1.Location = new System.Drawing.Point(3, 168);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(292, 88);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Commands";
            // 
            // TestButton
            // 
            this.TestButton.AutoSize = true;
            this.TestButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.TestButton.Location = new System.Drawing.Point(3, 19);
            this.TestButton.Name = "TestButton";
            this.TestButton.Size = new System.Drawing.Size(90, 23);
            this.TestButton.TabIndex = 0;
            this.TestButton.Text = "Test Sequence";
            this.TestButton.UseVisualStyleBackColor = true;
            this.TestButton.Click += new System.EventHandler(this.TestButton_Click);
            // 
            // AbortButton
            // 
            this.AbortButton.AutoSize = true;
            this.AbortButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.AbortButton.Location = new System.Drawing.Point(99, 19);
            this.AbortButton.Name = "AbortButton";
            this.AbortButton.Size = new System.Drawing.Size(94, 23);
            this.AbortButton.TabIndex = 1;
            this.AbortButton.Text = "Abort Sequence";
            this.AbortButton.UseVisualStyleBackColor = true;
            this.AbortButton.Click += new System.EventHandler(this.AbortButton_Click);
            // 
            // DelayGeneratorTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 262);
            this.Controls.Add(this.Splitter);
            this.Name = "DelayGeneratorTestForm";
            this.Text = "Delay Generator";
            this.ConfigBox.ResumeLayout(false);
            this.ConfigBox.PerformLayout();
            this.ConfigFlow.ResumeLayout(false);
            this.ConfigFlow.PerformLayout();
            this.GpibFlow.ResumeLayout(false);
            this.GpibFlow.PerformLayout();
            this.GpibAddressFlow.ResumeLayout(false);
            this.GpibAddressFlow.PerformLayout();
            this.PrologixConfigGrid.ResumeLayout(false);
            this.PrologixConfigGrid.PerformLayout();
            this.PrologixConfigFlowLeft.ResumeLayout(false);
            this.PrologixConfigFlowLeft.PerformLayout();
            this.PadFlow.ResumeLayout(false);
            this.PadFlow.PerformLayout();
            this.TimeoutFlow.ResumeLayout(false);
            this.TimeoutFlow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Timeout)).EndInit();
            this.PrologixConfigFlowRight.ResumeLayout(false);
            this.PrologixConfigFlowRight.PerformLayout();
            this.BaudFlow.ResumeLayout(false);
            this.BaudFlow.PerformLayout();
            this.ComFlow.ResumeLayout(false);
            this.ComFlow.PerformLayout();
            this.ConsoleBox.ResumeLayout(false);
            this.Splitter.Panel1.ResumeLayout(false);
            this.Splitter.Panel2.ResumeLayout(false);
            this.Splitter.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Splitter)).EndInit();
            this.Splitter.ResumeLayout(false);
            this.RightFlow.ResumeLayout(false);
            this.RightFlow.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox ConfigBox;
        private System.Windows.Forms.ComboBox ComPorts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton SelectNi;
        private System.Windows.Forms.RadioButton SelectPrologix;
        private System.Windows.Forms.FlowLayoutPanel GpibFlow;
        private System.Windows.Forms.FlowLayoutPanel ConfigFlow;
        private System.Windows.Forms.FlowLayoutPanel ComFlow;
        private System.Windows.Forms.GroupBox ConsoleBox;
        private System.Windows.Forms.FlowLayoutPanel PrologixConfigFlowLeft;
        private System.Windows.Forms.FlowLayoutPanel BaudFlow;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox BaudRate;
        private CommandPrompt.CommandPrompt Console;
        private System.Windows.Forms.SplitContainer Splitter;
        private System.Windows.Forms.FlowLayoutPanel RightFlow;
        private System.Windows.Forms.FlowLayoutPanel GpibAddressFlow;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox GpibAddress;
        private System.Windows.Forms.FlowLayoutPanel PadFlow;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox Padding;
        private System.Windows.Forms.FlowLayoutPanel TimeoutFlow;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown Timeout;
        private System.Windows.Forms.TableLayoutPanel PrologixConfigGrid;
        private System.Windows.Forms.FlowLayoutPanel PrologixConfigFlowRight;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button TestButton;
        private System.Windows.Forms.Button AbortButton;
    }
}
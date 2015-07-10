namespace DetectorTester
{
    partial class BeamFlagTestForm
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
            this.SendZero = new System.Windows.Forms.Button();
            this.RightFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.ConfigBox = new System.Windows.Forms.GroupBox();
            this.ConfigFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.ConfigGrid = new System.Windows.Forms.TableLayoutPanel();
            this.ConfigFlowLeft = new System.Windows.Forms.FlowLayoutPanel();
            this.ComFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.ComPorts = new System.Windows.Forms.ComboBox();
            this.BaudFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.BaudRate = new System.Windows.Forms.ComboBox();
            this.ConfigFlowRight = new System.Windows.Forms.FlowLayoutPanel();
            this.DelayFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.Delay = new System.Windows.Forms.NumericUpDown();
            this.DtrRtsFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.DtrEnable = new System.Windows.Forms.CheckBox();
            this.RtsEnable = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SendThree = new System.Windows.Forms.Button();
            this.SendTwo = new System.Windows.Forms.Button();
            this.SendOne = new System.Windows.Forms.Button();
            this.Splitter = new System.Windows.Forms.SplitContainer();
            this.ConsoleBox = new System.Windows.Forms.GroupBox();
            this.Console = new CommandPrompt.CommandPrompt();
            this.RightFlow.SuspendLayout();
            this.ConfigBox.SuspendLayout();
            this.ConfigFlow.SuspendLayout();
            this.ConfigGrid.SuspendLayout();
            this.ConfigFlowLeft.SuspendLayout();
            this.ComFlow.SuspendLayout();
            this.BaudFlow.SuspendLayout();
            this.ConfigFlowRight.SuspendLayout();
            this.DelayFlow.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Delay)).BeginInit();
            this.DtrRtsFlow.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Splitter)).BeginInit();
            this.Splitter.Panel1.SuspendLayout();
            this.Splitter.Panel2.SuspendLayout();
            this.Splitter.SuspendLayout();
            this.ConsoleBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // SendZero
            // 
            this.SendZero.AutoSize = true;
            this.SendZero.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SendZero.Location = new System.Drawing.Point(3, 19);
            this.SendZero.Name = "SendZero";
            this.SendZero.Size = new System.Drawing.Size(66, 23);
            this.SendZero.TabIndex = 0;
            this.SendZero.Text = "Send SO0";
            this.SendZero.UseVisualStyleBackColor = true;
            this.SendZero.Click += new System.EventHandler(this.SendZero_Click);
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
            this.RightFlow.Size = new System.Drawing.Size(281, 197);
            this.RightFlow.TabIndex = 0;
            // 
            // ConfigBox
            // 
            this.ConfigBox.AutoSize = true;
            this.ConfigBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ConfigBox.Controls.Add(this.ConfigFlow);
            this.ConfigBox.Location = new System.Drawing.Point(3, 3);
            this.ConfigBox.Name = "ConfigBox";
            this.ConfigBox.Size = new System.Drawing.Size(275, 97);
            this.ConfigBox.TabIndex = 0;
            this.ConfigBox.TabStop = false;
            this.ConfigBox.Text = "Configuration";
            // 
            // ConfigFlow
            // 
            this.ConfigFlow.AutoSize = true;
            this.ConfigFlow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ConfigFlow.Controls.Add(this.ConfigGrid);
            this.ConfigFlow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConfigFlow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.ConfigFlow.Location = new System.Drawing.Point(3, 16);
            this.ConfigFlow.Name = "ConfigFlow";
            this.ConfigFlow.Size = new System.Drawing.Size(269, 78);
            this.ConfigFlow.TabIndex = 1;
            // 
            // ConfigGrid
            // 
            this.ConfigGrid.AutoSize = true;
            this.ConfigGrid.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ConfigGrid.ColumnCount = 2;
            this.ConfigGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.ConfigGrid.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.ConfigGrid.Controls.Add(this.ConfigFlowLeft, 0, 0);
            this.ConfigGrid.Controls.Add(this.ConfigFlowRight, 1, 0);
            this.ConfigGrid.Location = new System.Drawing.Point(3, 3);
            this.ConfigGrid.Name = "ConfigGrid";
            this.ConfigGrid.RowCount = 1;
            this.ConfigGrid.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.ConfigGrid.Size = new System.Drawing.Size(263, 72);
            this.ConfigGrid.TabIndex = 1;
            // 
            // ConfigFlowLeft
            // 
            this.ConfigFlowLeft.AutoSize = true;
            this.ConfigFlowLeft.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ConfigFlowLeft.Controls.Add(this.ComFlow);
            this.ConfigFlowLeft.Controls.Add(this.BaudFlow);
            this.ConfigFlowLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConfigFlowLeft.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.ConfigFlowLeft.Location = new System.Drawing.Point(3, 3);
            this.ConfigFlowLeft.Name = "ConfigFlowLeft";
            this.ConfigFlowLeft.Size = new System.Drawing.Size(136, 66);
            this.ConfigFlowLeft.TabIndex = 4;
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
            // ConfigFlowRight
            // 
            this.ConfigFlowRight.AutoSize = true;
            this.ConfigFlowRight.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ConfigFlowRight.Controls.Add(this.DelayFlow);
            this.ConfigFlowRight.Controls.Add(this.DtrRtsFlow);
            this.ConfigFlowRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConfigFlowRight.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.ConfigFlowRight.Location = new System.Drawing.Point(145, 3);
            this.ConfigFlowRight.Name = "ConfigFlowRight";
            this.ConfigFlowRight.Size = new System.Drawing.Size(115, 66);
            this.ConfigFlowRight.TabIndex = 5;
            // 
            // DelayFlow
            // 
            this.DelayFlow.AutoSize = true;
            this.DelayFlow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DelayFlow.Controls.Add(this.label5);
            this.DelayFlow.Controls.Add(this.Delay);
            this.DelayFlow.Enabled = false;
            this.DelayFlow.Location = new System.Drawing.Point(3, 3);
            this.DelayFlow.Name = "DelayFlow";
            this.DelayFlow.Size = new System.Drawing.Size(92, 26);
            this.DelayFlow.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 0);
            this.label5.Name = "label5";
            this.label5.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.label5.Size = new System.Drawing.Size(34, 19);
            this.label5.TabIndex = 1;
            this.label5.Text = "Delay";
            // 
            // Delay
            // 
            this.Delay.Location = new System.Drawing.Point(43, 3);
            this.Delay.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.Delay.Name = "Delay";
            this.Delay.Size = new System.Drawing.Size(46, 20);
            this.Delay.TabIndex = 1;
            this.Delay.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            // 
            // DtrRtsFlow
            // 
            this.DtrRtsFlow.AutoSize = true;
            this.DtrRtsFlow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DtrRtsFlow.Controls.Add(this.DtrEnable);
            this.DtrRtsFlow.Controls.Add(this.RtsEnable);
            this.DtrRtsFlow.Location = new System.Drawing.Point(3, 35);
            this.DtrRtsFlow.Name = "DtrRtsFlow";
            this.DtrRtsFlow.Size = new System.Drawing.Size(109, 23);
            this.DtrRtsFlow.TabIndex = 2;
            // 
            // DtrEnable
            // 
            this.DtrEnable.AutoSize = true;
            this.DtrEnable.Location = new System.Drawing.Point(3, 3);
            this.DtrEnable.Name = "DtrEnable";
            this.DtrEnable.Size = new System.Drawing.Size(49, 17);
            this.DtrEnable.TabIndex = 5;
            this.DtrEnable.Text = "DTR";
            this.DtrEnable.UseVisualStyleBackColor = true;
            this.DtrEnable.CheckedChanged += new System.EventHandler(this.DtrEnable_CheckedChanged);
            // 
            // RtsEnable
            // 
            this.RtsEnable.AutoSize = true;
            this.RtsEnable.Location = new System.Drawing.Point(58, 3);
            this.RtsEnable.Name = "RtsEnable";
            this.RtsEnable.Size = new System.Drawing.Size(48, 17);
            this.RtsEnable.TabIndex = 6;
            this.RtsEnable.Text = "RTS";
            this.RtsEnable.UseVisualStyleBackColor = true;
            this.RtsEnable.CheckedChanged += new System.EventHandler(this.RtsEnable_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.SendThree);
            this.groupBox1.Controls.Add(this.SendTwo);
            this.groupBox1.Controls.Add(this.SendOne);
            this.groupBox1.Controls.Add(this.SendZero);
            this.groupBox1.Location = new System.Drawing.Point(3, 106);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(275, 88);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Commands";
            // 
            // SendThree
            // 
            this.SendThree.AutoSize = true;
            this.SendThree.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SendThree.Location = new System.Drawing.Point(75, 48);
            this.SendThree.Name = "SendThree";
            this.SendThree.Size = new System.Drawing.Size(66, 23);
            this.SendThree.TabIndex = 3;
            this.SendThree.Text = "Send SO3";
            this.SendThree.UseVisualStyleBackColor = true;
            this.SendThree.Click += new System.EventHandler(this.SendThree_Click);
            // 
            // SendTwo
            // 
            this.SendTwo.AutoSize = true;
            this.SendTwo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SendTwo.Location = new System.Drawing.Point(3, 48);
            this.SendTwo.Name = "SendTwo";
            this.SendTwo.Size = new System.Drawing.Size(66, 23);
            this.SendTwo.TabIndex = 2;
            this.SendTwo.Text = "Send SO2";
            this.SendTwo.UseVisualStyleBackColor = true;
            this.SendTwo.Click += new System.EventHandler(this.SendTwo_Click);
            // 
            // SendOne
            // 
            this.SendOne.AutoSize = true;
            this.SendOne.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.SendOne.Location = new System.Drawing.Point(75, 19);
            this.SendOne.Name = "SendOne";
            this.SendOne.Size = new System.Drawing.Size(66, 23);
            this.SendOne.TabIndex = 1;
            this.SendOne.Text = "Send SO1";
            this.SendOne.UseVisualStyleBackColor = true;
            this.SendOne.Click += new System.EventHandler(this.SendOne_Click);
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
            this.Splitter.Size = new System.Drawing.Size(433, 262);
            this.Splitter.SplitterDistance = 142;
            this.Splitter.TabIndex = 5;
            // 
            // ConsoleBox
            // 
            this.ConsoleBox.Controls.Add(this.Console);
            this.ConsoleBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConsoleBox.Location = new System.Drawing.Point(0, 0);
            this.ConsoleBox.Name = "ConsoleBox";
            this.ConsoleBox.Size = new System.Drawing.Size(142, 262);
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
            this.Console.Size = new System.Drawing.Size(136, 251);
            this.Console.TabIndex = 0;
            // 
            // BeamFlagTestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 262);
            this.Controls.Add(this.Splitter);
            this.Name = "BeamFlagTestForm";
            this.Text = "BeamFlagTestForm";
            this.RightFlow.ResumeLayout(false);
            this.RightFlow.PerformLayout();
            this.ConfigBox.ResumeLayout(false);
            this.ConfigBox.PerformLayout();
            this.ConfigFlow.ResumeLayout(false);
            this.ConfigFlow.PerformLayout();
            this.ConfigGrid.ResumeLayout(false);
            this.ConfigGrid.PerformLayout();
            this.ConfigFlowLeft.ResumeLayout(false);
            this.ConfigFlowLeft.PerformLayout();
            this.ComFlow.ResumeLayout(false);
            this.ComFlow.PerformLayout();
            this.BaudFlow.ResumeLayout(false);
            this.BaudFlow.PerformLayout();
            this.ConfigFlowRight.ResumeLayout(false);
            this.ConfigFlowRight.PerformLayout();
            this.DelayFlow.ResumeLayout(false);
            this.DelayFlow.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Delay)).EndInit();
            this.DtrRtsFlow.ResumeLayout(false);
            this.DtrRtsFlow.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Splitter.Panel1.ResumeLayout(false);
            this.Splitter.Panel2.ResumeLayout(false);
            this.Splitter.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Splitter)).EndInit();
            this.Splitter.ResumeLayout(false);
            this.ConsoleBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SendZero;
        private System.Windows.Forms.FlowLayoutPanel RightFlow;
        private System.Windows.Forms.GroupBox ConfigBox;
        private System.Windows.Forms.FlowLayoutPanel ConfigFlow;
        private System.Windows.Forms.TableLayoutPanel ConfigGrid;
        private System.Windows.Forms.FlowLayoutPanel ConfigFlowLeft;
        private System.Windows.Forms.FlowLayoutPanel ComFlow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ComPorts;
        private System.Windows.Forms.FlowLayoutPanel BaudFlow;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox BaudRate;
        private System.Windows.Forms.FlowLayoutPanel ConfigFlowRight;
        private System.Windows.Forms.FlowLayoutPanel DelayFlow;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown Delay;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.SplitContainer Splitter;
        private System.Windows.Forms.GroupBox ConsoleBox;
        private CommandPrompt.CommandPrompt Console;
        private System.Windows.Forms.CheckBox DtrEnable;
        private System.Windows.Forms.CheckBox RtsEnable;
        private System.Windows.Forms.FlowLayoutPanel DtrRtsFlow;
        private System.Windows.Forms.Button SendThree;
        private System.Windows.Forms.Button SendTwo;
        private System.Windows.Forms.Button SendOne;
    }
}
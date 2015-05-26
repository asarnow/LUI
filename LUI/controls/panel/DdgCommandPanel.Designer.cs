namespace LUI.controls
{
    partial class DdgCommandPanel
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
            LUI.controls.DisabledRichTextBox textBox6;
            LUI.controls.DisabledRichTextBox textBox5;
            LUI.controls.DisabledRichTextBox textBox4;
            LUI.controls.DisabledRichTextBox textBox3;
            LUI.controls.DisabledRichTextBox textBox2;
            LUI.controls.DisabledRichTextBox textBox1;
            this.DdgConfigBox = new System.Windows.Forms.GroupBox();
            this.DDGTable = new System.Windows.Forms.TableLayoutPanel();
            this.PrimaryDelayValueText = new System.Windows.Forms.TextBox();
            this.PrimaryDelayTriggers = new System.Windows.Forms.ComboBox();
            this.PrimaryDelayDelays = new System.Windows.Forms.ComboBox();
            this.PrimaryDelayDdgs = new System.Windows.Forms.ComboBox();
            textBox6 = new LUI.controls.DisabledRichTextBox();
            textBox5 = new LUI.controls.DisabledRichTextBox();
            textBox4 = new LUI.controls.DisabledRichTextBox();
            textBox3 = new LUI.controls.DisabledRichTextBox();
            textBox2 = new LUI.controls.DisabledRichTextBox();
            textBox1 = new LUI.controls.DisabledRichTextBox();
            this.DdgConfigBox.SuspendLayout();
            this.DDGTable.SuspendLayout();
            this.SuspendLayout();
            // 
            // DdgConfigBox
            // 
            this.DdgConfigBox.AutoSize = true;
            this.DdgConfigBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DdgConfigBox.Controls.Add(this.DDGTable);
            this.DdgConfigBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DdgConfigBox.Location = new System.Drawing.Point(0, 0);
            this.DdgConfigBox.Margin = new System.Windows.Forms.Padding(2);
            this.DdgConfigBox.Name = "DdgConfigBox";
            this.DdgConfigBox.Padding = new System.Windows.Forms.Padding(2);
            this.DdgConfigBox.Size = new System.Drawing.Size(362, 59);
            this.DdgConfigBox.TabIndex = 16;
            this.DdgConfigBox.TabStop = false;
            this.DdgConfigBox.Text = "DDG Configuration";
            // 
            // DDGTable
            // 
            this.DDGTable.AutoSize = true;
            this.DDGTable.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DDGTable.ColumnCount = 5;
            this.DDGTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DDGTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DDGTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DDGTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DDGTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.DDGTable.Controls.Add(this.PrimaryDelayValueText, 4, 1);
            this.DDGTable.Controls.Add(textBox6, 0, 1);
            this.DDGTable.Controls.Add(textBox5, 3, 0);
            this.DDGTable.Controls.Add(textBox4, 2, 0);
            this.DDGTable.Controls.Add(textBox3, 1, 0);
            this.DDGTable.Controls.Add(textBox2, 0, 0);
            this.DDGTable.Controls.Add(this.PrimaryDelayTriggers, 3, 1);
            this.DDGTable.Controls.Add(this.PrimaryDelayDelays, 2, 1);
            this.DDGTable.Controls.Add(this.PrimaryDelayDdgs, 1, 1);
            this.DDGTable.Controls.Add(textBox1, 4, 0);
            this.DDGTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DDGTable.Location = new System.Drawing.Point(2, 15);
            this.DDGTable.Margin = new System.Windows.Forms.Padding(2);
            this.DDGTable.Name = "DDGTable";
            this.DDGTable.RowCount = 2;
            this.DDGTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DDGTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DDGTable.Size = new System.Drawing.Size(358, 42);
            this.DDGTable.TabIndex = 13;
            this.DDGTable.Paint += new System.Windows.Forms.PaintEventHandler(this.DDGTable_Paint);
            // 
            // PrimaryDelayValueText
            // 
            this.PrimaryDelayValueText.Location = new System.Drawing.Point(295, 19);
            this.PrimaryDelayValueText.Margin = new System.Windows.Forms.Padding(2);
            this.PrimaryDelayValueText.Name = "PrimaryDelayValueText";
            this.PrimaryDelayValueText.Size = new System.Drawing.Size(61, 20);
            this.PrimaryDelayValueText.TabIndex = 9;
            // 
            // PrimaryDelayTriggers
            // 
            this.PrimaryDelayTriggers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PrimaryDelayTriggers.FormattingEnabled = true;
            this.PrimaryDelayTriggers.Location = new System.Drawing.Point(234, 19);
            this.PrimaryDelayTriggers.Margin = new System.Windows.Forms.Padding(2);
            this.PrimaryDelayTriggers.Name = "PrimaryDelayTriggers";
            this.PrimaryDelayTriggers.Size = new System.Drawing.Size(57, 21);
            this.PrimaryDelayTriggers.TabIndex = 0;
            // 
            // PrimaryDelayDelays
            // 
            this.PrimaryDelayDelays.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PrimaryDelayDelays.FormattingEnabled = true;
            this.PrimaryDelayDelays.Location = new System.Drawing.Point(173, 19);
            this.PrimaryDelayDelays.Margin = new System.Windows.Forms.Padding(2);
            this.PrimaryDelayDelays.Name = "PrimaryDelayDelays";
            this.PrimaryDelayDelays.Size = new System.Drawing.Size(57, 21);
            this.PrimaryDelayDelays.TabIndex = 1;
            // 
            // PrimaryDelayDdgs
            // 
            this.PrimaryDelayDdgs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PrimaryDelayDdgs.FormattingEnabled = true;
            this.PrimaryDelayDdgs.Location = new System.Drawing.Point(77, 19);
            this.PrimaryDelayDdgs.Margin = new System.Windows.Forms.Padding(2);
            this.PrimaryDelayDdgs.Name = "PrimaryDelayDdgs";
            this.PrimaryDelayDdgs.Size = new System.Drawing.Size(92, 21);
            this.PrimaryDelayDdgs.TabIndex = 2;
            // 
            // textBox6
            // 
            textBox6.BackColor = System.Drawing.SystemColors.Control;
            textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox6.Location = new System.Drawing.Point(2, 19);
            textBox6.Margin = new System.Windows.Forms.Padding(2);
            textBox6.Name = "textBox6";
            textBox6.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            textBox6.Size = new System.Drawing.Size(71, 21);
            textBox6.TabIndex = 8;
            textBox6.Text = "Primary Delay";
            // 
            // textBox5
            // 
            textBox5.BackColor = System.Drawing.SystemColors.Control;
            textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox5.Location = new System.Drawing.Point(234, 2);
            textBox5.Margin = new System.Windows.Forms.Padding(2);
            textBox5.Name = "textBox5";
            textBox5.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            textBox5.Size = new System.Drawing.Size(40, 13);
            textBox5.TabIndex = 7;
            textBox5.Text = "Trigger";
            // 
            // textBox4
            // 
            textBox4.BackColor = System.Drawing.SystemColors.Control;
            textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox4.Location = new System.Drawing.Point(173, 2);
            textBox4.Margin = new System.Windows.Forms.Padding(2);
            textBox4.Name = "textBox4";
            textBox4.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            textBox4.Size = new System.Drawing.Size(34, 13);
            textBox4.TabIndex = 6;
            textBox4.Text = "Delay";
            // 
            // textBox3
            // 
            textBox3.BackColor = System.Drawing.SystemColors.Control;
            textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox3.Location = new System.Drawing.Point(77, 2);
            textBox3.Margin = new System.Windows.Forms.Padding(2);
            textBox3.Name = "textBox3";
            textBox3.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            textBox3.Size = new System.Drawing.Size(31, 13);
            textBox3.TabIndex = 5;
            textBox3.Text = "DDG";
            // 
            // textBox2
            // 
            textBox2.BackColor = System.Drawing.SystemColors.Control;
            textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox2.Location = new System.Drawing.Point(2, 2);
            textBox2.Margin = new System.Windows.Forms.Padding(2);
            textBox2.Name = "textBox2";
            textBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            textBox2.Size = new System.Drawing.Size(48, 13);
            textBox2.TabIndex = 4;
            textBox2.Text = "Function";
            // 
            // textBox1
            // 
            textBox1.BackColor = System.Drawing.SystemColors.Control;
            textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox1.Location = new System.Drawing.Point(295, 2);
            textBox1.Margin = new System.Windows.Forms.Padding(2);
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            textBox1.Size = new System.Drawing.Size(48, 13);
            textBox1.TabIndex = 3;
            textBox1.Text = "Value (s)";
            // 
            // DdgCommandPanel
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.DdgConfigBox);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "DdgCommandPanel";
            this.Size = new System.Drawing.Size(362, 59);
            this.DdgConfigBox.ResumeLayout(false);
            this.DdgConfigBox.PerformLayout();
            this.DDGTable.ResumeLayout(false);
            this.DDGTable.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox DdgConfigBox;
        private System.Windows.Forms.TableLayoutPanel DDGTable;
        private System.Windows.Forms.TextBox PrimaryDelayValueText;
        private System.Windows.Forms.ComboBox PrimaryDelayTriggers;
        private System.Windows.Forms.ComboBox PrimaryDelayDelays;
        private System.Windows.Forms.ComboBox PrimaryDelayDdgs;
    }
}

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
            this.DdgConfigBox.Location = new System.Drawing.Point(0, 0);
            this.DdgConfigBox.Name = "DdgConfigBox";
            this.DdgConfigBox.Size = new System.Drawing.Size(483, 74);
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
            this.DDGTable.Location = new System.Drawing.Point(3, 18);
            this.DDGTable.Name = "DDGTable";
            this.DDGTable.RowCount = 2;
            this.DDGTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DDGTable.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.DDGTable.Size = new System.Drawing.Size(477, 53);
            this.DDGTable.TabIndex = 13;
            this.DDGTable.Paint += new System.Windows.Forms.PaintEventHandler(this.DDGTable_Paint);
            // 
            // PrimaryDelayValueText
            // 
            this.PrimaryDelayValueText.Location = new System.Drawing.Point(394, 26);
            this.PrimaryDelayValueText.Name = "PrimaryDelayValueText";
            this.PrimaryDelayValueText.Size = new System.Drawing.Size(80, 22);
            this.PrimaryDelayValueText.TabIndex = 9;
            // 
            // PrimaryDelayTriggers
            // 
            this.PrimaryDelayTriggers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PrimaryDelayTriggers.FormattingEnabled = true;
            this.PrimaryDelayTriggers.Location = new System.Drawing.Point(313, 26);
            this.PrimaryDelayTriggers.Name = "PrimaryDelayTriggers";
            this.PrimaryDelayTriggers.Size = new System.Drawing.Size(75, 24);
            this.PrimaryDelayTriggers.TabIndex = 0;
            // 
            // PrimaryDelayDelays
            // 
            this.PrimaryDelayDelays.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PrimaryDelayDelays.FormattingEnabled = true;
            this.PrimaryDelayDelays.Location = new System.Drawing.Point(232, 26);
            this.PrimaryDelayDelays.Name = "PrimaryDelayDelays";
            this.PrimaryDelayDelays.Size = new System.Drawing.Size(75, 24);
            this.PrimaryDelayDelays.TabIndex = 1;
            // 
            // PrimaryDelayDdgs
            // 
            this.PrimaryDelayDdgs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PrimaryDelayDdgs.FormattingEnabled = true;
            this.PrimaryDelayDdgs.Location = new System.Drawing.Point(105, 26);
            this.PrimaryDelayDdgs.Name = "PrimaryDelayDdgs";
            this.PrimaryDelayDdgs.Size = new System.Drawing.Size(121, 24);
            this.PrimaryDelayDdgs.TabIndex = 2;
            // 
            // textBox6
            // 
            textBox6.AutoSize = true;
            textBox6.BackColor = System.Drawing.SystemColors.Control;
            textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox6.Location = new System.Drawing.Point(3, 26);
            textBox6.Name = "textBox6";
            textBox6.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            textBox6.Size = new System.Drawing.Size(96, 17);
            textBox6.TabIndex = 8;
            textBox6.Text = "Primary Delay";
            // 
            // textBox5
            // 
            textBox5.AutoSize = true;
            textBox5.BackColor = System.Drawing.SystemColors.Control;
            textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox5.Location = new System.Drawing.Point(313, 3);
            textBox5.Name = "textBox5";
            textBox5.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            textBox5.Size = new System.Drawing.Size(54, 17);
            textBox5.TabIndex = 7;
            textBox5.Text = "Trigger";
            // 
            // textBox4
            // 
            textBox4.AutoSize = true;
            textBox4.BackColor = System.Drawing.SystemColors.Control;
            textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox4.Location = new System.Drawing.Point(232, 3);
            textBox4.Name = "textBox4";
            textBox4.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            textBox4.Size = new System.Drawing.Size(44, 17);
            textBox4.TabIndex = 6;
            textBox4.Text = "Delay";
            // 
            // textBox3
            // 
            textBox3.AutoSize = true;
            textBox3.BackColor = System.Drawing.SystemColors.Control;
            textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox3.Location = new System.Drawing.Point(105, 3);
            textBox3.Name = "textBox3";
            textBox3.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            textBox3.Size = new System.Drawing.Size(39, 17);
            textBox3.TabIndex = 5;
            textBox3.Text = "DDG";
            // 
            // textBox2
            // 
            textBox2.AutoSize = true;
            textBox2.BackColor = System.Drawing.SystemColors.Control;
            textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox2.Location = new System.Drawing.Point(3, 3);
            textBox2.Name = "textBox2";
            textBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            textBox2.Size = new System.Drawing.Size(62, 17);
            textBox2.TabIndex = 4;
            textBox2.Text = "Function";
            // 
            // textBox1
            // 
            textBox1.AutoSize = true;
            textBox1.BackColor = System.Drawing.SystemColors.Control;
            textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox1.Location = new System.Drawing.Point(394, 3);
            textBox1.Name = "textBox1";
            textBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            textBox1.Size = new System.Drawing.Size(65, 17);
            textBox1.TabIndex = 3;
            textBox1.Text = "Value (s)";
            // 
            // DdgCommandPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.DdgConfigBox);
            this.Name = "DdgCommandPanel";
            this.Size = new System.Drawing.Size(486, 77);
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

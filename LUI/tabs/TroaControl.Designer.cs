namespace LUI.tabs
{
    partial class TroaControl
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
            System.Windows.Forms.Label CameraStatusLabel;
            LUI.controls.DisabledRichTextBox textBox1;
            LUI.controls.DisabledRichTextBox textBox2;
            LUI.controls.DisabledRichTextBox textBox3;
            LUI.controls.DisabledRichTextBox textBox4;
            LUI.controls.DisabledRichTextBox textBox5;
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            this.LoadTimes = new System.Windows.Forms.Button();
            this.CameraStatus = new System.Windows.Forms.TextBox();
            this.TimesView = new System.Windows.Forms.DataGridView();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaveData = new System.Windows.Forms.Button();
            this.DDGTable = new System.Windows.Forms.TableLayoutPanel();
            this.PrimaryDelayValue = new System.Windows.Forms.TextBox();
            this.textBox6 = new LUI.controls.DisabledRichTextBox();
            this.PrimaryDelayTriggers = new System.Windows.Forms.ComboBox();
            this.PrimaryDelayDelays = new System.Windows.Forms.ComboBox();
            this.PrimaryDelayDdgs = new System.Windows.Forms.ComboBox();
            this.DdgConfigBox = new System.Windows.Forms.GroupBox();
            CameraStatusLabel = new System.Windows.Forms.Label();
            textBox1 = new LUI.controls.DisabledRichTextBox();
            textBox2 = new LUI.controls.DisabledRichTextBox();
            textBox3 = new LUI.controls.DisabledRichTextBox();
            textBox4 = new LUI.controls.DisabledRichTextBox();
            textBox5 = new LUI.controls.DisabledRichTextBox();
            this.ParentPanel.SuspendLayout();
            this.StatusBox.SuspendLayout();
            this.CommandsBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NScan)).BeginInit();
            this.ChildArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimesView)).BeginInit();
            this.DDGTable.SuspendLayout();
            this.DdgConfigBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // StatusBox
            // 
            this.StatusBox.Controls.Add(CameraStatusLabel);
            this.StatusBox.Controls.Add(this.CameraStatus);
            this.StatusBox.Controls.SetChildIndex(this.StatusProgress, 0);
            this.StatusBox.Controls.SetChildIndex(this.ProgressLabel, 0);
            this.StatusBox.Controls.SetChildIndex(this.CameraStatus, 0);
            this.StatusBox.Controls.SetChildIndex(CameraStatusLabel, 0);
            // 
            // NScan
            // 
            this.NScan.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
            // 
            // ChildArea
            // 
            this.ChildArea.Controls.Add(this.DdgConfigBox);
            this.ChildArea.Controls.Add(this.SaveData);
            this.ChildArea.Controls.Add(this.TimesView);
            this.ChildArea.Controls.Add(this.LoadTimes);
            // 
            // Graph
            // 
            this.Graph.InitialScaleHeight = 2F;
            this.Graph.InitialYMax = 1F;
            this.Graph.InitialYMin = -1F;
            this.Graph.ScaleHeight = 2F;
            this.Graph.XLeft = 1F;
            this.Graph.XRight = 1024F;
            this.Graph.YMax = 1F;
            this.Graph.YMin = -1F;
            // 
            // CameraStatusLabel
            // 
            CameraStatusLabel.AutoSize = true;
            CameraStatusLabel.Location = new System.Drawing.Point(22, 208);
            CameraStatusLabel.Name = "CameraStatusLabel";
            CameraStatusLabel.Size = new System.Drawing.Size(101, 17);
            CameraStatusLabel.TabIndex = 9;
            CameraStatusLabel.Text = "Camera Status";
            CameraStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox1.Location = new System.Drawing.Point(490, 3);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            textBox1.Size = new System.Drawing.Size(100, 15);
            textBox1.TabIndex = 3;
            textBox1.Text = "Value (s)";
            // 
            // textBox2
            // 
            textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox2.Location = new System.Drawing.Point(3, 3);
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            textBox2.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            textBox2.Size = new System.Drawing.Size(100, 15);
            textBox2.TabIndex = 4;
            textBox2.Text = "Function";
            // 
            // textBox3
            // 
            textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox3.Location = new System.Drawing.Point(109, 3);
            textBox3.Name = "textBox3";
            textBox3.ReadOnly = true;
            textBox3.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            textBox3.Size = new System.Drawing.Size(100, 15);
            textBox3.TabIndex = 5;
            textBox3.Text = "DDG";
            // 
            // textBox4
            // 
            textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox4.Location = new System.Drawing.Point(236, 3);
            textBox4.Name = "textBox4";
            textBox4.ReadOnly = true;
            textBox4.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            textBox4.Size = new System.Drawing.Size(100, 15);
            textBox4.TabIndex = 6;
            textBox4.Text = "Delay";
            // 
            // textBox5
            // 
            textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox5.Location = new System.Drawing.Point(363, 3);
            textBox5.Name = "textBox5";
            textBox5.ReadOnly = true;
            textBox5.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            textBox5.Size = new System.Drawing.Size(100, 15);
            textBox5.TabIndex = 7;
            textBox5.Text = "Trigger";
            // 
            // LoadTimes
            // 
            this.LoadTimes.Location = new System.Drawing.Point(959, 7);
            this.LoadTimes.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.LoadTimes.Name = "LoadTimes";
            this.LoadTimes.Size = new System.Drawing.Size(136, 34);
            this.LoadTimes.TabIndex = 10;
            this.LoadTimes.Text = "Load Times";
            this.LoadTimes.UseVisualStyleBackColor = true;
            this.LoadTimes.Click += new System.EventHandler(this.LoadTimes_Click);
            // 
            // CameraStatus
            // 
            this.CameraStatus.Location = new System.Drawing.Point(7, 228);
            this.CameraStatus.Name = "CameraStatus";
            this.CameraStatus.ReadOnly = true;
            this.CameraStatus.Size = new System.Drawing.Size(132, 22);
            this.CameraStatus.TabIndex = 8;
            this.CameraStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TimesView
            // 
            this.TimesView.AllowUserToOrderColumns = true;
            this.TimesView.AllowUserToResizeColumns = false;
            this.TimesView.AllowUserToResizeRows = false;
            this.TimesView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.TimesView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.TimesView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TimesView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Value});
            this.TimesView.Location = new System.Drawing.Point(959, 46);
            this.TimesView.MultiSelect = false;
            this.TimesView.Name = "TimesView";
            this.TimesView.RowHeadersVisible = false;
            this.TimesView.RowTemplate.Height = 24;
            this.TimesView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.TimesView.ShowEditingIcon = false;
            this.TimesView.Size = new System.Drawing.Size(136, 150);
            this.TimesView.TabIndex = 11;
            // 
            // Value
            // 
            this.Value.DataPropertyName = "Value";
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Value.DefaultCellStyle = dataGridViewCellStyle5;
            this.Value.HeaderText = "Delay (s)";
            this.Value.Name = "Value";
            this.Value.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // SaveData
            // 
            this.SaveData.Location = new System.Drawing.Point(959, 201);
            this.SaveData.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.SaveData.Name = "SaveData";
            this.SaveData.Size = new System.Drawing.Size(136, 34);
            this.SaveData.TabIndex = 12;
            this.SaveData.Text = "Save Data";
            this.SaveData.UseVisualStyleBackColor = true;
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
            this.DDGTable.Controls.Add(this.PrimaryDelayValue, 4, 1);
            this.DDGTable.Controls.Add(this.textBox6, 0, 1);
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
            this.DDGTable.Size = new System.Drawing.Size(593, 51);
            this.DDGTable.TabIndex = 13;
            // 
            // PrimaryDelayValue
            // 
            this.PrimaryDelayValue.Location = new System.Drawing.Point(490, 24);
            this.PrimaryDelayValue.Name = "PrimaryDelayValue";
            this.PrimaryDelayValue.Size = new System.Drawing.Size(100, 22);
            this.PrimaryDelayValue.TabIndex = 9;
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.SystemColors.Control;
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox6.Location = new System.Drawing.Point(3, 24);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.textBox6.Size = new System.Drawing.Size(100, 15);
            this.textBox6.TabIndex = 8;
            this.textBox6.Text = "Primary Delay";
            // 
            // PrimaryDelayTriggers
            // 
            this.PrimaryDelayTriggers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PrimaryDelayTriggers.FormattingEnabled = true;
            this.PrimaryDelayTriggers.Location = new System.Drawing.Point(363, 24);
            this.PrimaryDelayTriggers.Name = "PrimaryDelayTriggers";
            this.PrimaryDelayTriggers.Size = new System.Drawing.Size(121, 24);
            this.PrimaryDelayTriggers.TabIndex = 0;
            // 
            // PrimaryDelayDelays
            // 
            this.PrimaryDelayDelays.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PrimaryDelayDelays.FormattingEnabled = true;
            this.PrimaryDelayDelays.Location = new System.Drawing.Point(236, 24);
            this.PrimaryDelayDelays.Name = "PrimaryDelayDelays";
            this.PrimaryDelayDelays.Size = new System.Drawing.Size(121, 24);
            this.PrimaryDelayDelays.TabIndex = 1;
            // 
            // PrimaryDelayDdgs
            // 
            this.PrimaryDelayDdgs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.PrimaryDelayDdgs.FormattingEnabled = true;
            this.PrimaryDelayDdgs.Location = new System.Drawing.Point(109, 24);
            this.PrimaryDelayDdgs.Name = "PrimaryDelayDdgs";
            this.PrimaryDelayDdgs.Size = new System.Drawing.Size(121, 24);
            this.PrimaryDelayDdgs.TabIndex = 2;
            // 
            // DdgConfigBox
            // 
            this.DdgConfigBox.AutoSize = true;
            this.DdgConfigBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DdgConfigBox.Controls.Add(this.DDGTable);
            this.DdgConfigBox.Location = new System.Drawing.Point(4, 3);
            this.DdgConfigBox.Name = "DdgConfigBox";
            this.DdgConfigBox.Size = new System.Drawing.Size(599, 72);
            this.DdgConfigBox.TabIndex = 14;
            this.DdgConfigBox.TabStop = false;
            this.DdgConfigBox.Text = "DDG Configuration";
            // 
            // TroaControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Name = "TroaControl";
            this.ParentPanel.ResumeLayout(false);
            this.ParentPanel.PerformLayout();
            this.StatusBox.ResumeLayout(false);
            this.StatusBox.PerformLayout();
            this.CommandsBox.ResumeLayout(false);
            this.CommandsBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NScan)).EndInit();
            this.ChildArea.ResumeLayout(false);
            this.ChildArea.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraGain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimesView)).EndInit();
            this.DDGTable.ResumeLayout(false);
            this.DDGTable.PerformLayout();
            this.DdgConfigBox.ResumeLayout(false);
            this.DdgConfigBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button LoadTimes;
        private System.Windows.Forms.TextBox CameraStatus;
        private System.Windows.Forms.DataGridView TimesView;
        private System.Windows.Forms.Button SaveData;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private System.Windows.Forms.TableLayoutPanel DDGTable;
        private System.Windows.Forms.TextBox PrimaryDelayValue;
        LUI.controls.DisabledRichTextBox textBox6;
        private System.Windows.Forms.ComboBox PrimaryDelayTriggers;
        private System.Windows.Forms.ComboBox PrimaryDelayDelays;
        private System.Windows.Forms.ComboBox PrimaryDelayDdgs;
        private System.Windows.Forms.GroupBox DdgConfigBox;

    }
}

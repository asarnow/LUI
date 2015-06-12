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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.LoadTimes = new System.Windows.Forms.Button();
            this.CameraStatus = new System.Windows.Forms.TextBox();
            this.TimesView = new System.Windows.Forms.DataGridView();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaveData = new System.Windows.Forms.Button();
            this.DdgConfigBox = new LUI.controls.DdgCommandPanel();
            this.PumpBox = new LUI.controls.ObjectCommandPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PumpNever = new System.Windows.Forms.RadioButton();
            this.PumpTs = new System.Windows.Forms.RadioButton();
            this.PumpAlways = new System.Windows.Forms.RadioButton();
            this.Discard = new System.Windows.Forms.CheckBox();
            CameraStatusLabel = new System.Windows.Forms.Label();
            this.StatusBox.SuspendLayout();
            this.CommandsBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NScan)).BeginInit();
            this.CommonObjectPanel.SuspendLayout();
            this.LeftChildArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraGain)).BeginInit();
            this.RightChildArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TimesView)).BeginInit();
            this.PumpBox.Flow.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // NScan
            // 
            this.NScan.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
            // 
            // Graph
            // 
            this.Graph.InitialScaleHeight = 2F;
            this.Graph.InitialYMax = 1F;
            this.Graph.InitialYMin = -1F;
            this.Graph.ScaleHeight = 2F;
            this.Graph.Size = new System.Drawing.Size(1176, 584);
            this.Graph.XLeft = 1F;
            this.Graph.XRight = 1024F;
            this.Graph.YMax = 1F;
            this.Graph.YMin = -1F;
            // 
            // LeftChildArea
            // 
            this.LeftChildArea.Controls.Add(this.DdgConfigBox);
            this.LeftChildArea.Controls.Add(this.SaveData);
            this.LeftChildArea.Controls.Add(this.TimesView);
            this.LeftChildArea.Controls.Add(this.LoadTimes);
            this.LeftChildArea.Location = new System.Drawing.Point(0, 584);
            this.LeftChildArea.Size = new System.Drawing.Size(1176, 237);
            // 
            // RightChildArea
            // 
            this.RightChildArea.Controls.Add(this.PumpBox);
            // 
            // CameraStatusLabel
            // 
            CameraStatusLabel.AutoSize = true;
            CameraStatusLabel.Location = new System.Drawing.Point(22, 208);
            CameraStatusLabel.Name = "CameraStatusLabel";
            CameraStatusLabel.Size = new System.Drawing.Size(76, 13);
            CameraStatusLabel.TabIndex = 9;
            CameraStatusLabel.Text = "Camera Status";
            CameraStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.CameraStatus.Size = new System.Drawing.Size(132, 20);
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
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Value.DefaultCellStyle = dataGridViewCellStyle2;
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
            // DdgConfigBox
            // 
            this.DdgConfigBox.AutoSize = true;
            this.DdgConfigBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.DdgConfigBox.Location = new System.Drawing.Point(3, 3);
            this.DdgConfigBox.Margin = new System.Windows.Forms.Padding(2);
            this.DdgConfigBox.Name = "DdgConfigBox";
            this.DdgConfigBox.Size = new System.Drawing.Size(362, 59);
            this.DdgConfigBox.TabIndex = 15;
            // 
            // PumpBox
            // 
            this.PumpBox.AutoSize = true;
            this.PumpBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PumpBox.Dock = System.Windows.Forms.DockStyle.Top;
            // 
            // PumpBox.Flow
            // 
            this.PumpBox.Flow.AutoSize = true;
            this.PumpBox.Flow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PumpBox.Flow.Controls.Add(this.panel1);
            this.PumpBox.Flow.Controls.Add(this.Discard);
            this.PumpBox.Flow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PumpBox.Flow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.PumpBox.Flow.Location = new System.Drawing.Point(3, 16);
            this.PumpBox.Flow.Name = "Flow";
            this.PumpBox.Flow.Size = new System.Drawing.Size(294, 92);
            this.PumpBox.Flow.TabIndex = 0;
            this.PumpBox.Location = new System.Drawing.Point(0, 0);
            this.PumpBox.Name = "PumpBox";
            this.PumpBox.SelectedObject = null;
            this.PumpBox.Size = new System.Drawing.Size(300, 111);
            this.PumpBox.TabIndex = 0;
            this.PumpBox.Text = "Syringe Pump";
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.PumpAlways);
            this.panel1.Controls.Add(this.PumpTs);
            this.panel1.Controls.Add(this.PumpNever);
            this.panel1.Location = new System.Drawing.Point(3, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(198, 30);
            this.panel1.TabIndex = 1;
            // 
            // PumpNever
            // 
            this.PumpNever.AutoSize = true;
            this.PumpNever.Checked = true;
            this.PumpNever.Location = new System.Drawing.Point(8, 10);
            this.PumpNever.Name = "PumpNever";
            this.PumpNever.Size = new System.Drawing.Size(54, 17);
            this.PumpNever.TabIndex = 0;
            this.PumpNever.TabStop = true;
            this.PumpNever.Text = "Never";
            this.PumpNever.UseVisualStyleBackColor = true;
            // 
            // PumpTs
            // 
            this.PumpTs.AutoSize = true;
            this.PumpTs.Location = new System.Drawing.Point(68, 10);
            this.PumpTs.Name = "PumpTs";
            this.PumpTs.Size = new System.Drawing.Size(63, 17);
            this.PumpTs.TabIndex = 1;
            this.PumpTs.TabStop = true;
            this.PumpTs.Text = "TS Only";
            this.PumpTs.UseVisualStyleBackColor = true;
            // 
            // PumpAlways
            // 
            this.PumpAlways.AutoSize = true;
            this.PumpAlways.Location = new System.Drawing.Point(137, 10);
            this.PumpAlways.Name = "PumpAlways";
            this.PumpAlways.Size = new System.Drawing.Size(58, 17);
            this.PumpAlways.TabIndex = 2;
            this.PumpAlways.TabStop = true;
            this.PumpAlways.Text = "Always";
            this.PumpAlways.UseVisualStyleBackColor = true;
            // 
            // Discard
            // 
            this.Discard.AutoSize = true;
            this.Discard.Location = new System.Drawing.Point(3, 72);
            this.Discard.Name = "Discard";
            this.Discard.Size = new System.Drawing.Size(84, 17);
            this.Discard.TabIndex = 2;
            this.Discard.Text = "Discard First";
            this.Discard.UseVisualStyleBackColor = true;
            // 
            // TroaControl
            // 
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Name = "TroaControl";
            this.StatusBox.ResumeLayout(false);
            this.StatusBox.PerformLayout();
            this.CommandsBox.ResumeLayout(false);
            this.CommandsBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NScan)).EndInit();
            this.CommonObjectPanel.ResumeLayout(false);
            this.CommonObjectPanel.PerformLayout();
            this.LeftChildArea.ResumeLayout(false);
            this.LeftChildArea.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraGain)).EndInit();
            this.RightChildArea.ResumeLayout(false);
            this.RightChildArea.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TimesView)).EndInit();
            this.PumpBox.Flow.ResumeLayout(false);
            this.PumpBox.Flow.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button LoadTimes;
        private System.Windows.Forms.TextBox CameraStatus;
        private System.Windows.Forms.DataGridView TimesView;
        private System.Windows.Forms.Button SaveData;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private controls.DdgCommandPanel DdgConfigBox;
        private controls.ObjectCommandPanel PumpBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton PumpAlways;
        private System.Windows.Forms.RadioButton PumpTs;
        private System.Windows.Forms.RadioButton PumpNever;
        private System.Windows.Forms.CheckBox Discard;

    }
}

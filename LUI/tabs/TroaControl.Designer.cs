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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.LoadTimes = new System.Windows.Forms.Button();
            this.CameraStatus = new System.Windows.Forms.TextBox();
            this.TimesView = new System.Windows.Forms.DataGridView();
            this.Value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SaveData = new System.Windows.Forms.Button();
            this.DdgConfigBox = new LUI.controls.DdgCommandPanel();
            CameraStatusLabel = new System.Windows.Forms.Label();
            this.ParentPanel.SuspendLayout();
            this.StatusBox.SuspendLayout();
            this.CommandsBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NScan)).BeginInit();
            this.ChildArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimesView)).BeginInit();
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
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.Value.DefaultCellStyle = dataGridViewCellStyle1;
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
            this.DdgConfigBox.Name = "DdgConfigBox";
            this.DdgConfigBox.Size = new System.Drawing.Size(602, 75);
            this.DdgConfigBox.TabIndex = 15;
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
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button LoadTimes;
        private System.Windows.Forms.TextBox CameraStatus;
        private System.Windows.Forms.DataGridView TimesView;
        private System.Windows.Forms.Button SaveData;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value;
        private controls.DdgCommandPanel DdgConfigBox;

    }
}

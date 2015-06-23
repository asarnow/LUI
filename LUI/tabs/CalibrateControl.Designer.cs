namespace LUI.tabs
{
    partial class CalibrateControl
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
            this.RSquaredLabel = new LUI.controls.DisabledRichTextBox();
            this.InterceptLabel = new System.Windows.Forms.Label();
            this.SlopeLabel = new System.Windows.Forms.Label();
            this.SaveCal = new System.Windows.Forms.Button();
            this.Slope = new System.Windows.Forms.TextBox();
            this.Intercept = new System.Windows.Forms.TextBox();
            this.RSquared = new System.Windows.Forms.TextBox();
            this.RunCal = new System.Windows.Forms.Button();
            this.CalibrationListView = new System.Windows.Forms.DataGridView();
            this.Channel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Wavelength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RemoveCalItem = new System.Windows.Forms.Button();
            this.FlipGraph = new System.Windows.Forms.Button();
            this.ClearBlank = new System.Windows.Forms.Button();
            this.StatusBox.SuspendLayout();
            this.CommandsBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NScan)).BeginInit();
            this.CommonObjectPanel.SuspendLayout();
            this.LeftChildArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CameraGain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.CalibrationListView)).BeginInit();
            this.SuspendLayout();
            // 
            // CommandsBox
            // 
            this.CommandsBox.Controls.Add(this.ClearBlank);
            this.CommandsBox.Controls.SetChildIndex(this.Abort, 0);
            this.CommandsBox.Controls.SetChildIndex(this.Collect, 0);
            this.CommandsBox.Controls.SetChildIndex(this.NScan, 0);
            this.CommandsBox.Controls.SetChildIndex(this.Clear, 0);
            this.CommandsBox.Controls.SetChildIndex(this.Pause, 0);
            this.CommandsBox.Controls.SetChildIndex(this.ClearBlank, 0);
            // 
            // Graph
            // 
            this.Graph.Size = new System.Drawing.Size(1176, 582);
            this.Graph.XLeft = 1F;
            this.Graph.XRight = 1024F;
            // 
            // LeftChildArea
            // 
            this.LeftChildArea.Controls.Add(this.FlipGraph);
            this.LeftChildArea.Controls.Add(this.RSquaredLabel);
            this.LeftChildArea.Controls.Add(this.InterceptLabel);
            this.LeftChildArea.Controls.Add(this.SlopeLabel);
            this.LeftChildArea.Controls.Add(this.SaveCal);
            this.LeftChildArea.Controls.Add(this.Slope);
            this.LeftChildArea.Controls.Add(this.Intercept);
            this.LeftChildArea.Controls.Add(this.RSquared);
            this.LeftChildArea.Controls.Add(this.RunCal);
            this.LeftChildArea.Controls.Add(this.CalibrationListView);
            this.LeftChildArea.Controls.Add(this.RemoveCalItem);
            this.LeftChildArea.Location = new System.Drawing.Point(0, 582);
            this.LeftChildArea.Size = new System.Drawing.Size(1176, 239);
            this.LeftChildArea.TabIndex = 7;
            // 
            // RSquaredLabel
            // 
            this.RSquaredLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RSquaredLabel.Location = new System.Drawing.Point(843, 16);
            this.RSquaredLabel.Margin = new System.Windows.Forms.Padding(4);
            this.RSquaredLabel.Name = "RSquaredLabel";
            this.RSquaredLabel.ReadOnly = true;
            this.RSquaredLabel.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.RSquaredLabel.Size = new System.Drawing.Size(45, 17);
            this.RSquaredLabel.TabIndex = 17;
            this.RSquaredLabel.Text = "R2";
            this.RSquaredLabel.WordWrap = false;
            // 
            // InterceptLabel
            // 
            this.InterceptLabel.AutoSize = true;
            this.InterceptLabel.Location = new System.Drawing.Point(839, 71);
            this.InterceptLabel.Name = "InterceptLabel";
            this.InterceptLabel.Size = new System.Drawing.Size(49, 13);
            this.InterceptLabel.TabIndex = 15;
            this.InterceptLabel.Text = "Intercept";
            // 
            // SlopeLabel
            // 
            this.SlopeLabel.AutoSize = true;
            this.SlopeLabel.Location = new System.Drawing.Point(839, 44);
            this.SlopeLabel.Name = "SlopeLabel";
            this.SlopeLabel.Size = new System.Drawing.Size(34, 13);
            this.SlopeLabel.TabIndex = 14;
            this.SlopeLabel.Text = "Slope";
            // 
            // SaveCal
            // 
            this.SaveCal.Location = new System.Drawing.Point(637, 55);
            this.SaveCal.Margin = new System.Windows.Forms.Padding(4);
            this.SaveCal.Name = "SaveCal";
            this.SaveCal.Size = new System.Drawing.Size(91, 34);
            this.SaveCal.TabIndex = 12;
            this.SaveCal.Text = "Save";
            this.SaveCal.UseVisualStyleBackColor = true;
            this.SaveCal.Click += new System.EventHandler(this.SaveCal_Click);
            // 
            // Slope
            // 
            this.Slope.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Slope.Location = new System.Drawing.Point(735, 42);
            this.Slope.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Slope.Name = "Slope";
            this.Slope.Size = new System.Drawing.Size(101, 20);
            this.Slope.TabIndex = 11;
            // 
            // Intercept
            // 
            this.Intercept.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Intercept.Location = new System.Drawing.Point(735, 70);
            this.Intercept.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Intercept.Name = "Intercept";
            this.Intercept.Size = new System.Drawing.Size(101, 20);
            this.Intercept.TabIndex = 10;
            // 
            // RSquared
            // 
            this.RSquared.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RSquared.Location = new System.Drawing.Point(735, 14);
            this.RSquared.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.RSquared.Name = "RSquared";
            this.RSquared.Size = new System.Drawing.Size(101, 20);
            this.RSquared.TabIndex = 9;
            // 
            // RunCal
            // 
            this.RunCal.Location = new System.Drawing.Point(637, 14);
            this.RunCal.Margin = new System.Windows.Forms.Padding(4);
            this.RunCal.Name = "RunCal";
            this.RunCal.Size = new System.Drawing.Size(91, 34);
            this.RunCal.TabIndex = 8;
            this.RunCal.Text = "Calibrate";
            this.RunCal.UseVisualStyleBackColor = true;
            this.RunCal.Click += new System.EventHandler(this.RunCal_Click);
            // 
            // CalibrationListView
            // 
            this.CalibrationListView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.CalibrationListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.CalibrationListView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Channel,
            this.Wavelength});
            this.CalibrationListView.Location = new System.Drawing.Point(4, 4);
            this.CalibrationListView.Margin = new System.Windows.Forms.Padding(4);
            this.CalibrationListView.Name = "CalibrationListView";
            this.CalibrationListView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.CalibrationListView.Size = new System.Drawing.Size(329, 231);
            this.CalibrationListView.TabIndex = 7;
            // 
            // Channel
            // 
            this.Channel.DataPropertyName = "Channel";
            this.Channel.HeaderText = "Channel";
            this.Channel.Name = "Channel";
            this.Channel.ReadOnly = true;
            // 
            // Wavelength
            // 
            this.Wavelength.DataPropertyName = "Wavelength";
            this.Wavelength.HeaderText = "Wavelength";
            this.Wavelength.Name = "Wavelength";
            // 
            // RemoveCalItem
            // 
            this.RemoveCalItem.Location = new System.Drawing.Point(341, 46);
            this.RemoveCalItem.Margin = new System.Windows.Forms.Padding(4);
            this.RemoveCalItem.Name = "RemoveCalItem";
            this.RemoveCalItem.Size = new System.Drawing.Size(91, 34);
            this.RemoveCalItem.TabIndex = 5;
            this.RemoveCalItem.Text = "Remove";
            this.RemoveCalItem.UseVisualStyleBackColor = true;
            this.RemoveCalItem.Click += new System.EventHandler(this.RemoveCalItem_Click);
            // 
            // FlipGraph
            // 
            this.FlipGraph.Location = new System.Drawing.Point(1078, 6);
            this.FlipGraph.Margin = new System.Windows.Forms.Padding(4);
            this.FlipGraph.Name = "FlipGraph";
            this.FlipGraph.Size = new System.Drawing.Size(91, 34);
            this.FlipGraph.TabIndex = 18;
            this.FlipGraph.Text = "Flip Graph X";
            this.FlipGraph.UseVisualStyleBackColor = true;
            this.FlipGraph.Click += new System.EventHandler(this.FlipGraph_Click);
            // 
            // ClearBlank
            // 
            this.ClearBlank.Location = new System.Drawing.Point(8, 106);
            this.ClearBlank.Name = "ClearBlank";
            this.ClearBlank.Size = new System.Drawing.Size(91, 34);
            this.ClearBlank.TabIndex = 7;
            this.ClearBlank.Text = "Clear Blank";
            this.ClearBlank.UseVisualStyleBackColor = true;
            // 
            // CalibrateControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.Name = "CalibrateControl";
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
            ((System.ComponentModel.ISupportInitialize)(this.CalibrationListView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button RemoveCalItem;
        private System.Windows.Forms.DataGridView CalibrationListView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Channel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Wavelength;
        private System.Windows.Forms.TextBox RSquared;
        private System.Windows.Forms.Button RunCal;
        private System.Windows.Forms.Button SaveCal;
        private System.Windows.Forms.TextBox Slope;
        private System.Windows.Forms.TextBox Intercept;
        private System.Windows.Forms.Label InterceptLabel;
        private System.Windows.Forms.Label SlopeLabel;
        private controls.DisabledRichTextBox RSquaredLabel;
        private System.Windows.Forms.Button FlipGraph;
        private System.Windows.Forms.Button ClearBlank;
    }
}

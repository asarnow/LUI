namespace LUI.controls
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalibrateControl));
            this.TableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.StatusBox = new System.Windows.Forms.GroupBox();
            this.ProgressLabel = new System.Windows.Forms.Label();
            this.StatusProgress = new System.Windows.Forms.ProgressBar();
            this.CommandsBox = new System.Windows.Forms.GroupBox();
            this.Clear = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Averages = new System.Windows.Forms.NumericUpDown();
            this.Collect = new System.Windows.Forms.Button();
            this.Abort = new System.Windows.Forms.Button();
            this.BeamFlagBox = new System.Windows.Forms.GroupBox();
            this.CloseLamp = new System.Windows.Forms.Button();
            this.OpenLamp = new System.Windows.Forms.Button();
            this.CloseLaser = new System.Windows.Forms.Button();
            this.OpenLaser = new System.Windows.Forms.Button();
            this.CalPanel = new System.Windows.Forms.Panel();
            this.RSquaredLabel = new System.Windows.Forms.RichTextBox();
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
            this.Graph = new LUI.controls.GraphControl();
            this.TableLayout.SuspendLayout();
            this.StatusBox.SuspendLayout();
            this.CommandsBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Averages)).BeginInit();
            this.BeamFlagBox.SuspendLayout();
            this.CalPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CalibrationListView)).BeginInit();
            this.SuspendLayout();
            // 
            // TableLayout
            // 
            this.TableLayout.ColumnCount = 4;
            this.TableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.TableLayout.Controls.Add(this.StatusBox, 3, 0);
            this.TableLayout.Controls.Add(this.CommandsBox, 3, 1);
            this.TableLayout.Controls.Add(this.BeamFlagBox, 3, 2);
            this.TableLayout.Controls.Add(this.CalPanel, 0, 2);
            this.TableLayout.Controls.Add(this.Graph, 0, 0);
            this.TableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TableLayout.Location = new System.Drawing.Point(0, 0);
            this.TableLayout.Margin = new System.Windows.Forms.Padding(4);
            this.TableLayout.Name = "TableLayout";
            this.TableLayout.RowCount = 4;
            this.TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.TableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.TableLayout.Size = new System.Drawing.Size(1476, 821);
            this.TableLayout.TabIndex = 1;
            // 
            // StatusBox
            // 
            this.StatusBox.Controls.Add(this.ProgressLabel);
            this.StatusBox.Controls.Add(this.StatusProgress);
            this.StatusBox.Location = new System.Drawing.Point(1111, 4);
            this.StatusBox.Margin = new System.Windows.Forms.Padding(4);
            this.StatusBox.Name = "StatusBox";
            this.StatusBox.Padding = new System.Windows.Forms.Padding(4);
            this.StatusBox.Size = new System.Drawing.Size(361, 257);
            this.StatusBox.TabIndex = 2;
            this.StatusBox.TabStop = false;
            this.StatusBox.Text = "Status";
            // 
            // ProgressLabel
            // 
            this.ProgressLabel.AutoSize = true;
            this.ProgressLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProgressLabel.Location = new System.Drawing.Point(65, 100);
            this.ProgressLabel.Name = "ProgressLabel";
            this.ProgressLabel.Size = new System.Drawing.Size(39, 20);
            this.ProgressLabel.TabIndex = 7;
            this.ProgressLabel.Text = "Idle";
            // 
            // StatusProgress
            // 
            this.StatusProgress.Location = new System.Drawing.Point(69, 123);
            this.StatusProgress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.StatusProgress.Name = "StatusProgress";
            this.StatusProgress.Size = new System.Drawing.Size(227, 34);
            this.StatusProgress.TabIndex = 6;
            // 
            // CommandsBox
            // 
            this.CommandsBox.Controls.Add(this.Clear);
            this.CommandsBox.Controls.Add(this.label1);
            this.CommandsBox.Controls.Add(this.Averages);
            this.CommandsBox.Controls.Add(this.Collect);
            this.CommandsBox.Controls.Add(this.Abort);
            this.CommandsBox.Location = new System.Drawing.Point(1111, 269);
            this.CommandsBox.Margin = new System.Windows.Forms.Padding(4);
            this.CommandsBox.Name = "CommandsBox";
            this.CommandsBox.Padding = new System.Windows.Forms.Padding(4);
            this.CommandsBox.Size = new System.Drawing.Size(361, 257);
            this.CommandsBox.TabIndex = 3;
            this.CommandsBox.TabStop = false;
            this.CommandsBox.Text = "Commands";
            // 
            // Clear
            // 
            this.Clear.Location = new System.Drawing.Point(7, 149);
            this.Clear.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Clear.Name = "Clear";
            this.Clear.Size = new System.Drawing.Size(136, 34);
            this.Clear.TabIndex = 1;
            this.Clear.Text = "Clear Graph";
            this.Clear.UseVisualStyleBackColor = true;
            this.Clear.Click += new System.EventHandler(this.Clear_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(207, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Scans";
            // 
            // Averages
            // 
            this.Averages.Location = new System.Drawing.Point(151, 30);
            this.Averages.Margin = new System.Windows.Forms.Padding(4);
            this.Averages.Maximum = new decimal(new int[] {
            127,
            0,
            0,
            0});
            this.Averages.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.Averages.Name = "Averages";
            this.Averages.Size = new System.Drawing.Size(48, 22);
            this.Averages.TabIndex = 3;
            this.Averages.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // Collect
            // 
            this.Collect.Location = new System.Drawing.Point(8, 23);
            this.Collect.Margin = new System.Windows.Forms.Padding(4);
            this.Collect.Name = "Collect";
            this.Collect.Size = new System.Drawing.Size(136, 34);
            this.Collect.TabIndex = 2;
            this.Collect.Text = "Collect UV/vis";
            this.Collect.UseVisualStyleBackColor = true;
            this.Collect.Click += new System.EventHandler(this.Collect_Click);
            // 
            // Abort
            // 
            this.Abort.Location = new System.Drawing.Point(8, 65);
            this.Abort.Margin = new System.Windows.Forms.Padding(4);
            this.Abort.Name = "Abort";
            this.Abort.Size = new System.Drawing.Size(91, 34);
            this.Abort.TabIndex = 1;
            this.Abort.Text = "Abort";
            this.Abort.UseVisualStyleBackColor = true;
            // 
            // BeamFlagBox
            // 
            this.BeamFlagBox.Controls.Add(this.CloseLamp);
            this.BeamFlagBox.Controls.Add(this.OpenLamp);
            this.BeamFlagBox.Controls.Add(this.CloseLaser);
            this.BeamFlagBox.Controls.Add(this.OpenLaser);
            this.BeamFlagBox.Location = new System.Drawing.Point(1111, 534);
            this.BeamFlagBox.Margin = new System.Windows.Forms.Padding(4);
            this.BeamFlagBox.Name = "BeamFlagBox";
            this.BeamFlagBox.Padding = new System.Windows.Forms.Padding(4);
            this.BeamFlagBox.Size = new System.Drawing.Size(361, 107);
            this.BeamFlagBox.TabIndex = 5;
            this.BeamFlagBox.TabStop = false;
            this.BeamFlagBox.Text = "Beam Flags";
            // 
            // CloseLamp
            // 
            this.CloseLamp.Location = new System.Drawing.Point(152, 65);
            this.CloseLamp.Margin = new System.Windows.Forms.Padding(4);
            this.CloseLamp.Name = "CloseLamp";
            this.CloseLamp.Size = new System.Drawing.Size(136, 34);
            this.CloseLamp.TabIndex = 7;
            this.CloseLamp.Text = "Close Lamp";
            this.CloseLamp.UseVisualStyleBackColor = true;
            // 
            // OpenLamp
            // 
            this.OpenLamp.Location = new System.Drawing.Point(152, 23);
            this.OpenLamp.Margin = new System.Windows.Forms.Padding(4);
            this.OpenLamp.Name = "OpenLamp";
            this.OpenLamp.Size = new System.Drawing.Size(136, 34);
            this.OpenLamp.TabIndex = 6;
            this.OpenLamp.Text = "Open Lamp";
            this.OpenLamp.UseVisualStyleBackColor = true;
            // 
            // CloseLaser
            // 
            this.CloseLaser.Location = new System.Drawing.Point(8, 65);
            this.CloseLaser.Margin = new System.Windows.Forms.Padding(4);
            this.CloseLaser.Name = "CloseLaser";
            this.CloseLaser.Size = new System.Drawing.Size(136, 34);
            this.CloseLaser.TabIndex = 5;
            this.CloseLaser.Text = "Close Laser";
            this.CloseLaser.UseVisualStyleBackColor = true;
            // 
            // OpenLaser
            // 
            this.OpenLaser.Location = new System.Drawing.Point(8, 23);
            this.OpenLaser.Margin = new System.Windows.Forms.Padding(4);
            this.OpenLaser.Name = "OpenLaser";
            this.OpenLaser.Size = new System.Drawing.Size(136, 34);
            this.OpenLaser.TabIndex = 4;
            this.OpenLaser.Text = "Open Laser";
            this.OpenLaser.UseVisualStyleBackColor = true;
            // 
            // CalPanel
            // 
            this.TableLayout.SetColumnSpan(this.CalPanel, 3);
            this.CalPanel.Controls.Add(this.RSquaredLabel);
            this.CalPanel.Controls.Add(this.InterceptLabel);
            this.CalPanel.Controls.Add(this.SlopeLabel);
            this.CalPanel.Controls.Add(this.SaveCal);
            this.CalPanel.Controls.Add(this.Slope);
            this.CalPanel.Controls.Add(this.Intercept);
            this.CalPanel.Controls.Add(this.RSquared);
            this.CalPanel.Controls.Add(this.RunCal);
            this.CalPanel.Controls.Add(this.CalibrationListView);
            this.CalPanel.Controls.Add(this.RemoveCalItem);
            this.CalPanel.Location = new System.Drawing.Point(3, 532);
            this.CalPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.CalPanel.Name = "CalPanel";
            this.CalPanel.Size = new System.Drawing.Size(1099, 258);
            this.CalPanel.TabIndex = 7;
            // 
            // RSquaredLabel
            // 
            this.RSquaredLabel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RSquaredLabel.Location = new System.Drawing.Point(877, 40);
            this.RSquaredLabel.Multiline = false;
            this.RSquaredLabel.Name = "RSquaredLabel";
            this.RSquaredLabel.ReadOnly = true;
            this.RSquaredLabel.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.RSquaredLabel.ShortcutsEnabled = false;
            this.RSquaredLabel.Size = new System.Drawing.Size(44, 23);
            this.RSquaredLabel.TabIndex = 16;
            this.RSquaredLabel.Text = "R2";
            // 
            // InterceptLabel
            // 
            this.InterceptLabel.AutoSize = true;
            this.InterceptLabel.Location = new System.Drawing.Point(874, 99);
            this.InterceptLabel.Name = "InterceptLabel";
            this.InterceptLabel.Size = new System.Drawing.Size(63, 17);
            this.InterceptLabel.TabIndex = 15;
            this.InterceptLabel.Text = "Intercept";
            // 
            // SlopeLabel
            // 
            this.SlopeLabel.AutoSize = true;
            this.SlopeLabel.Location = new System.Drawing.Point(874, 71);
            this.SlopeLabel.Name = "SlopeLabel";
            this.SlopeLabel.Size = new System.Drawing.Size(44, 17);
            this.SlopeLabel.TabIndex = 14;
            this.SlopeLabel.Text = "Slope";
            // 
            // SaveCal
            // 
            this.SaveCal.Location = new System.Drawing.Point(570, 131);
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
            this.Slope.Location = new System.Drawing.Point(771, 69);
            this.Slope.Name = "Slope";
            this.Slope.Size = new System.Drawing.Size(100, 22);
            this.Slope.TabIndex = 11;
            // 
            // Intercept
            // 
            this.Intercept.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Intercept.Location = new System.Drawing.Point(771, 97);
            this.Intercept.Name = "Intercept";
            this.Intercept.Size = new System.Drawing.Size(100, 22);
            this.Intercept.TabIndex = 10;
            // 
            // RSquared
            // 
            this.RSquared.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.RSquared.Location = new System.Drawing.Point(771, 41);
            this.RSquared.Name = "RSquared";
            this.RSquared.Size = new System.Drawing.Size(100, 22);
            this.RSquared.TabIndex = 9;
            // 
            // RunCal
            // 
            this.RunCal.Location = new System.Drawing.Point(637, 35);
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
            this.CalibrationListView.Size = new System.Drawing.Size(329, 251);
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
            // Graph
            // 
            this.Graph.AxesColor = System.Drawing.Color.Blue;
            this.Graph.AxesFont = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Graph.AxesFontColor = System.Drawing.Color.Black;
            this.Graph.ColorOrder = ((System.Collections.Generic.List<System.Drawing.Color>)(resources.GetObject("Graph.ColorOrder")));
            this.TableLayout.SetColumnSpan(this.Graph, 3);
            this.Graph.InitialScaleHeight = 0F;
            this.Graph.InitialXMax = 1023F;
            this.Graph.InitialXMin = 0F;
            this.Graph.InitialYMax = float.NegativeInfinity;
            this.Graph.InitialYMin = float.PositiveInfinity;
            this.Graph.Location = new System.Drawing.Point(3, 2);
            this.Graph.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Graph.Marker = "*";
            this.Graph.MarkerColor = System.Drawing.Color.Blue;
            this.Graph.MarkerFont = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Graph.Name = "Graph";
            this.Graph.NXTicks = 10;
            this.Graph.NYTicks = 10;
            this.Graph.PadBottom = 0.01F;
            this.Graph.PadLeft = 0.01F;
            this.Graph.PadRight = 0.01F;
            this.Graph.PadTop = 0.01F;
            this.TableLayout.SetRowSpan(this.Graph, 2);
            this.Graph.ScaleHeight = 0F;
            this.Graph.Size = new System.Drawing.Size(1099, 524);
            this.Graph.TabIndex = 8;
            this.Graph.XAxisHeight = 0.1F;
            this.Graph.XLabelFormat = "f0";
            this.Graph.XMax = 1023F;
            this.Graph.XMin = 0F;
            this.Graph.YAxisWidth = 0.05F;
            this.Graph.YLabelFormat = "n3";
            this.Graph.YMax = float.NegativeInfinity;
            this.Graph.YMin = float.PositiveInfinity;
            // 
            // CalibrateControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.TableLayout);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CalibrateControl";
            this.Size = new System.Drawing.Size(1476, 821);
            this.TableLayout.ResumeLayout(false);
            this.StatusBox.ResumeLayout(false);
            this.StatusBox.PerformLayout();
            this.CommandsBox.ResumeLayout(false);
            this.CommandsBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Averages)).EndInit();
            this.BeamFlagBox.ResumeLayout(false);
            this.CalPanel.ResumeLayout(false);
            this.CalPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CalibrationListView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel TableLayout;
        private System.Windows.Forms.GroupBox StatusBox;
        private System.Windows.Forms.GroupBox CommandsBox;
        private System.Windows.Forms.Button Clear;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown Averages;
        private System.Windows.Forms.Button Collect;
        private System.Windows.Forms.Button Abort;
        private System.Windows.Forms.GroupBox BeamFlagBox;
        private System.Windows.Forms.Button CloseLamp;
        private System.Windows.Forms.Button OpenLamp;
        private System.Windows.Forms.Button CloseLaser;
        private System.Windows.Forms.Button OpenLaser;
        private System.Windows.Forms.Button RemoveCalItem;
        private System.Windows.Forms.Panel CalPanel;
        private System.Windows.Forms.Label ProgressLabel;
        private System.Windows.Forms.ProgressBar StatusProgress;
        private controls.GraphControl Graph;
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
        private System.Windows.Forms.RichTextBox RSquaredLabel;
    }
}

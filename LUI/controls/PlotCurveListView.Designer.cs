namespace LUI.controls
{
    partial class PlotCurveListView
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
            this.DataGrid = new System.Windows.Forms.DataGridView();
            this.CurveName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurveColor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CurveVisible = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.CurveSave = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // DataGrid
            // 
            this.DataGrid.AllowUserToAddRows = false;
            this.DataGrid.AllowUserToDeleteRows = false;
            this.DataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CurveName,
            this.CurveColor,
            this.CurveVisible,
            this.CurveSave});
            this.DataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DataGrid.Location = new System.Drawing.Point(0, 0);
            this.DataGrid.MultiSelect = false;
            this.DataGrid.Name = "DataGrid";
            this.DataGrid.Size = new System.Drawing.Size(456, 150);
            this.DataGrid.TabIndex = 0;
            // 
            // CurveName
            // 
            this.CurveName.HeaderText = "Name";
            this.CurveName.Name = "CurveName";
            // 
            // CurveColor
            // 
            this.CurveColor.HeaderText = "Color";
            this.CurveColor.Name = "CurveColor";
            this.CurveColor.ReadOnly = true;
            this.CurveColor.Width = 60;
            // 
            // CurveVisible
            // 
            this.CurveVisible.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CurveVisible.HeaderText = "Visible";
            this.CurveVisible.Name = "CurveVisible";
            this.CurveVisible.Width = 43;
            // 
            // CurveSave
            // 
            this.CurveSave.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.CurveSave.HeaderText = "Save";
            this.CurveSave.Name = "CurveSave";
            this.CurveSave.Width = 38;
            // 
            // PlotCurveListView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DataGrid);
            this.Name = "PlotCurveListView";
            this.Size = new System.Drawing.Size(456, 150);
            ((System.ComponentModel.ISupportInitialize)(this.DataGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView DataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurveName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CurveColor;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CurveVisible;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CurveSave;
    }
}

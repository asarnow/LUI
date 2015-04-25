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
            this.RoleListView = new System.Windows.Forms.DataGridView();
            this.Role = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DDG = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Delay = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.Trigger = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.DelayValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LoadTimes = new System.Windows.Forms.Button();
            this.ParentPanel.SuspendLayout();
            this.StatusBox.SuspendLayout();
            this.CommandsBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NScan)).BeginInit();
            this.BeamFlagBox.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RoleListView)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.LoadTimes);
            this.panel1.Controls.Add(this.RoleListView);
            this.panel1.Controls.SetChildIndex(this.RoleListView, 0);
            this.panel1.Controls.SetChildIndex(this.LoadTimes, 0);
            // 
            // Graph
            // 
            this.Graph.XLeft = 1F;
            this.Graph.XRight = 1024F;
            // 
            // RoleListView
            // 
            this.RoleListView.AllowUserToAddRows = false;
            this.RoleListView.AllowUserToDeleteRows = false;
            this.RoleListView.AllowUserToResizeColumns = false;
            this.RoleListView.AllowUserToResizeRows = false;
            this.RoleListView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.RoleListView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.RoleListView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RoleListView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Role,
            this.DDG,
            this.Delay,
            this.Trigger,
            this.DelayValue});
            this.RoleListView.Location = new System.Drawing.Point(3, 6);
            this.RoleListView.MultiSelect = false;
            this.RoleListView.Name = "RoleListView";
            this.RoleListView.RowHeadersVisible = false;
            this.RoleListView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.RoleListView.ShowEditingIcon = false;
            this.RoleListView.Size = new System.Drawing.Size(508, 99);
            this.RoleListView.TabIndex = 9;
            // 
            // Role
            // 
            this.Role.DataPropertyName = "Role";
            this.Role.HeaderText = "Function";
            this.Role.Name = "Role";
            this.Role.ReadOnly = true;
            this.Role.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // DDG
            // 
            this.DDG.DataPropertyName = "DDG";
            this.DDG.HeaderText = "DDG";
            this.DDG.Name = "DDG";
            // 
            // Delay
            // 
            this.Delay.DataPropertyName = "Delay";
            this.Delay.HeaderText = "Delay";
            this.Delay.Name = "Delay";
            this.Delay.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Delay.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // Trigger
            // 
            this.Trigger.DataPropertyName = "Trigger";
            this.Trigger.HeaderText = "Trigger";
            this.Trigger.Name = "Trigger";
            this.Trigger.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Trigger.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // DelayValue
            // 
            this.DelayValue.DataPropertyName = "DelayValue";
            this.DelayValue.HeaderText = "Value (μs)";
            this.DelayValue.Name = "DelayValue";
            this.DelayValue.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DelayValue.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // LoadTimes
            // 
            this.LoadTimes.Location = new System.Drawing.Point(719, 6);
            this.LoadTimes.Margin = new System.Windows.Forms.Padding(2);
            this.LoadTimes.Name = "LoadTimes";
            this.LoadTimes.Size = new System.Drawing.Size(102, 28);
            this.LoadTimes.TabIndex = 10;
            this.LoadTimes.Text = "Load Times";
            this.LoadTimes.UseVisualStyleBackColor = true;
            this.LoadTimes.Click += new System.EventHandler(this.LoadTimes_Click);
            // 
            // TroaControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "TroaControl";
            this.ParentPanel.ResumeLayout(false);
            this.StatusBox.ResumeLayout(false);
            this.StatusBox.PerformLayout();
            this.CommandsBox.ResumeLayout(false);
            this.CommandsBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NScan)).EndInit();
            this.BeamFlagBox.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RoleListView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView RoleListView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Role;
        private System.Windows.Forms.DataGridViewComboBoxColumn DDG;
        private System.Windows.Forms.DataGridViewComboBoxColumn Delay;
        private System.Windows.Forms.DataGridViewComboBoxColumn Trigger;
        private System.Windows.Forms.DataGridViewTextBoxColumn DelayValue;
        private System.Windows.Forms.Button LoadTimes;

    }
}

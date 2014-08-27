namespace LUI
{
    partial class OptionsForm
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
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("General", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Instruments", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Logging");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Camera");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Delay Generation");
            this.Close = new System.Windows.Forms.Button();
            this.Apply = new System.Windows.Forms.Button();
            this.OK = new System.Windows.Forms.Button();
            this.OptionsListView = new System.Windows.Forms.ListView();
            this.OptionsPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // Close
            // 
            this.Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Close.Location = new System.Drawing.Point(503, 273);
            this.Close.Name = "Close";
            this.Close.Size = new System.Drawing.Size(75, 23);
            this.Close.TabIndex = 0;
            this.Close.Text = "Close";
            this.Close.UseVisualStyleBackColor = true;
            this.Close.Click += new System.EventHandler(this.Close_Click);
            // 
            // Apply
            // 
            this.Apply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Apply.Location = new System.Drawing.Point(422, 273);
            this.Apply.Name = "Apply";
            this.Apply.Size = new System.Drawing.Size(75, 23);
            this.Apply.TabIndex = 1;
            this.Apply.Text = "Apply";
            this.Apply.UseVisualStyleBackColor = true;
            // 
            // OK
            // 
            this.OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OK.Location = new System.Drawing.Point(341, 273);
            this.OK.Name = "OK";
            this.OK.Size = new System.Drawing.Size(75, 23);
            this.OK.TabIndex = 2;
            this.OK.Text = "OK";
            this.OK.UseVisualStyleBackColor = true;
            // 
            // OptionsListView
            // 
            listViewGroup1.Header = "General";
            listViewGroup1.Name = "GeneralOptions";
            listViewGroup2.Header = "Instruments";
            listViewGroup2.Name = "InstrumentsOptions";
            this.OptionsListView.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1,
            listViewGroup2});
            listViewItem1.Group = listViewGroup1;
            listViewItem2.Group = listViewGroup2;
            listViewItem3.Group = listViewGroup2;
            this.OptionsListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.OptionsListView.Location = new System.Drawing.Point(12, 12);
            this.OptionsListView.MultiSelect = false;
            this.OptionsListView.Name = "OptionsListView";
            this.OptionsListView.Size = new System.Drawing.Size(121, 255);
            this.OptionsListView.TabIndex = 3;
            this.OptionsListView.UseCompatibleStateImageBehavior = false;
            this.OptionsListView.View = System.Windows.Forms.View.SmallIcon;
            this.OptionsListView.SelectedIndexChanged += new System.EventHandler(this.OptionsListView_SelectedIndexChanged);
            // 
            // OptionsPanel
            // 
            this.OptionsPanel.Location = new System.Drawing.Point(139, 12);
            this.OptionsPanel.Name = "OptionsPanel";
            this.OptionsPanel.Size = new System.Drawing.Size(439, 255);
            this.OptionsPanel.TabIndex = 4;
            // 
            // OptionsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 308);
            this.Controls.Add(this.OptionsPanel);
            this.Controls.Add(this.OptionsListView);
            this.Controls.Add(this.OK);
            this.Controls.Add(this.Apply);
            this.Controls.Add(this.Close);
            this.Name = "OptionsForm";
            this.Text = "OptionsForm";
            this.Load += new System.EventHandler(this.OptionsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Close;
        private System.Windows.Forms.Button Apply;
        private System.Windows.Forms.Button OK;
        private System.Windows.Forms.ListView OptionsListView;
        private System.Windows.Forms.Panel OptionsPanel;
    }
}
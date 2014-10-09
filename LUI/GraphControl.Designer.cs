namespace LUI
{
    partial class GraphControl
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
            this.Image = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Image)).BeginInit();
            this.SuspendLayout();
            // 
            // Image
            // 
            this.Image.Location = new System.Drawing.Point(0, 0);
            this.Image.Name = "Image";
            this.Image.Size = new System.Drawing.Size(100, 50);
            this.Image.TabIndex = 0;
            this.Image.TabStop = false;
            // 
            // GraphControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Image);
            this.Name = "GraphControl";
            ((System.ComponentModel.ISupportInitialize)(this.Image)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Image;
    }
}

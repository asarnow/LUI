namespace PumpToggle
{
    partial class Form1
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
            this.comPort = new System.Windows.Forms.TextBox();
            this.toggleButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rtsCheck = new System.Windows.Forms.CheckBox();
            this.dtrCheck = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // comPort
            // 
            this.comPort.Location = new System.Drawing.Point(112, 59);
            this.comPort.Margin = new System.Windows.Forms.Padding(4);
            this.comPort.Name = "comPort";
            this.comPort.Size = new System.Drawing.Size(20, 22);
            this.comPort.TabIndex = 0;
            this.comPort.Text = "1";
            this.comPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.comPort.TextChanged += new System.EventHandler(this.comPort_TextChanged);
            // 
            // toggleButton
            // 
            this.toggleButton.Location = new System.Drawing.Point(141, 57);
            this.toggleButton.Margin = new System.Windows.Forms.Padding(4);
            this.toggleButton.Name = "toggleButton";
            this.toggleButton.Size = new System.Drawing.Size(87, 28);
            this.toggleButton.TabIndex = 1;
            this.toggleButton.Text = "Open";
            this.toggleButton.UseVisualStyleBackColor = true;
            this.toggleButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 61);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "COM Port";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rtsCheck
            // 
            this.rtsCheck.AutoSize = true;
            this.rtsCheck.Location = new System.Drawing.Point(12, 12);
            this.rtsCheck.Name = "rtsCheck";
            this.rtsCheck.Size = new System.Drawing.Size(106, 21);
            this.rtsCheck.TabIndex = 3;
            this.rtsCheck.Text = "Enable RTS";
            this.rtsCheck.UseVisualStyleBackColor = true;
            // 
            // dtrCheck
            // 
            this.dtrCheck.AutoSize = true;
            this.dtrCheck.Location = new System.Drawing.Point(146, 12);
            this.dtrCheck.Name = "dtrCheck";
            this.dtrCheck.Size = new System.Drawing.Size(107, 21);
            this.dtrCheck.TabIndex = 4;
            this.dtrCheck.Text = "Enable DTR";
            this.dtrCheck.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(265, 100);
            this.Controls.Add(this.dtrCheck);
            this.Controls.Add(this.rtsCheck);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.toggleButton);
            this.Controls.Add(this.comPort);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "PumpToggle";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox comPort;
        private System.Windows.Forms.Button toggleButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox rtsCheck;
        private System.Windows.Forms.CheckBox dtrCheck;
    }
}


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
            this.xonxoffCheck = new System.Windows.Forms.CheckBox();
            this.rtsHandshakeCheck = new System.Windows.Forms.CheckBox();
            this.HandshakePanelLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // comPort
            // 
            this.comPort.Location = new System.Drawing.Point(74, 103);
            this.comPort.Name = "comPort";
            this.comPort.Size = new System.Drawing.Size(16, 20);
            this.comPort.TabIndex = 0;
            this.comPort.Text = "1";
            this.comPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.comPort.TextChanged += new System.EventHandler(this.comPort_TextChanged);
            // 
            // toggleButton
            // 
            this.toggleButton.Location = new System.Drawing.Point(93, 101);
            this.toggleButton.Name = "toggleButton";
            this.toggleButton.Size = new System.Drawing.Size(65, 23);
            this.toggleButton.TabIndex = 1;
            this.toggleButton.Text = "Open";
            this.toggleButton.UseVisualStyleBackColor = true;
            this.toggleButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "COM Port";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rtsCheck
            // 
            this.rtsCheck.AutoSize = true;
            this.rtsCheck.Location = new System.Drawing.Point(9, 10);
            this.rtsCheck.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rtsCheck.Name = "rtsCheck";
            this.rtsCheck.Size = new System.Drawing.Size(84, 17);
            this.rtsCheck.TabIndex = 3;
            this.rtsCheck.Text = "Enable RTS";
            this.rtsCheck.UseVisualStyleBackColor = true;
            this.rtsCheck.CheckedChanged += new System.EventHandler(this.rtsCheck_CheckedChanged);
            // 
            // dtrCheck
            // 
            this.dtrCheck.AutoSize = true;
            this.dtrCheck.Location = new System.Drawing.Point(93, 10);
            this.dtrCheck.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.dtrCheck.Name = "dtrCheck";
            this.dtrCheck.Size = new System.Drawing.Size(85, 17);
            this.dtrCheck.TabIndex = 4;
            this.dtrCheck.Text = "Enable DTR";
            this.dtrCheck.UseVisualStyleBackColor = true;
            this.dtrCheck.CheckedChanged += new System.EventHandler(this.dtrCheck_CheckedChanged);
            // 
            // xonxoffCheck
            // 
            this.xonxoffCheck.AutoSize = true;
            this.xonxoffCheck.Location = new System.Drawing.Point(9, 59);
            this.xonxoffCheck.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.xonxoffCheck.Name = "xonxoffCheck";
            this.xonxoffCheck.Size = new System.Drawing.Size(68, 17);
            this.xonxoffCheck.TabIndex = 5;
            this.xonxoffCheck.Text = "XOnXOff";
            this.xonxoffCheck.UseVisualStyleBackColor = true;
            this.xonxoffCheck.CheckedChanged += new System.EventHandler(this.xonxoffCheck_CheckedChanged);
            // 
            // rtsHandshakeCheck
            // 
            this.rtsHandshakeCheck.AutoSize = true;
            this.rtsHandshakeCheck.Location = new System.Drawing.Point(93, 59);
            this.rtsHandshakeCheck.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rtsHandshakeCheck.Name = "rtsHandshakeCheck";
            this.rtsHandshakeCheck.Size = new System.Drawing.Size(48, 17);
            this.rtsHandshakeCheck.TabIndex = 6;
            this.rtsHandshakeCheck.Text = "RTS";
            this.rtsHandshakeCheck.UseVisualStyleBackColor = true;
            this.rtsHandshakeCheck.CheckedChanged += new System.EventHandler(this.rtsHandshakeCheck_CheckedChanged);
            // 
            // HandshakePanelLabel
            // 
            this.HandshakePanelLabel.AutoSize = true;
            this.HandshakePanelLabel.Location = new System.Drawing.Point(5, 43);
            this.HandshakePanelLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.HandshakePanelLabel.Name = "HandshakePanelLabel";
            this.HandshakePanelLabel.Size = new System.Drawing.Size(70, 13);
            this.HandshakePanelLabel.TabIndex = 8;
            this.HandshakePanelLabel.Text = "Handshaking";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(203, 131);
            this.Controls.Add(this.rtsHandshakeCheck);
            this.Controls.Add(this.HandshakePanelLabel);
            this.Controls.Add(this.xonxoffCheck);
            this.Controls.Add(this.toggleButton);
            this.Controls.Add(this.comPort);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dtrCheck);
            this.Controls.Add(this.rtsCheck);
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
        private System.Windows.Forms.CheckBox xonxoffCheck;
        private System.Windows.Forms.CheckBox rtsHandshakeCheck;
        private System.Windows.Forms.Label HandshakePanelLabel;
    }
}


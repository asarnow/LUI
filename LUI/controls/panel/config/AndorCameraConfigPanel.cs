using lasercom.camera;
using System;
using System.Windows.Forms;

namespace LUI.controls
{
    class AndorCameraConfigPanel : CameraConfigPanel
    {
        protected LabeledControl<TextBox> Dir;
        protected LabeledControl<NumericUpDown> InitialGain;

        public override Type Target
        {
            get { return typeof(AndorCamera); }
        }

        public AndorCameraConfigPanel()
            : base()
        {
            Dir = new LabeledControl<TextBox>(new TextBox(), "Andor INI Dir:");
            Dir.Control.TextChanged += OnOptionsChanged;
            Dir.Control.TextChanged += (s, e) => Dir.Control.AutoResize();
            Dir.Control.MinimumSize = new System.Drawing.Size(40, 0);
            Dir.Control.Text = "./";
            var Browse = new Button();
            Browse.Text = "...";
            Browse.AutoSize = true;
            Browse.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Browse.Click += BrowseDir_Click;
            Dir.Controls.Add(Browse);
            this.Controls.Add(Dir);

            InitialGain = new LabeledControl<NumericUpDown>(new NumericUpDown(), "Initial Gain:");
            InitialGain.Control.Minimum = 0;
            InitialGain.Control.Increment = 1;
            InitialGain.Control.Maximum = 1024;
            InitialGain.Control.Value = AndorCamera.DefaultMCPGain;
            InitialGain.Control.ValueChanged += (s, e) => OnOptionsChanged(s, e);
            this.Controls.Add(InitialGain);
        }

        private void BrowseDir_Click(object sender, EventArgs e)
        {
            var orig = Dir.Control.Text;
            var user = GuiUtil.SimpleFolderNameDialog();
            Dir.Control.Text = user != "" ? user : orig;
        }

        public override void CopyTo(CameraParameters other)
        {
            base.CopyTo(other);
            other.Dir = Dir.Control.Text;
            other.InitialGain = (int)InitialGain.Control.Value;
        }

        public override void CopyFrom(CameraParameters other)
        {
            base.CopyFrom(other);
            Dir.Control.Text = other.Dir;
            InitialGain.Control.Value = other.InitialGain;
        }

    }
}

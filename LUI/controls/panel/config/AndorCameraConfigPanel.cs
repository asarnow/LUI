using lasercom.camera;
using lasercom.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Dir.Control.Text = "./";
            Dir.Control.TextChanged += (s, e) => OnOptionsChanged(s,e);
            this.Controls.Add(Dir);

            InitialGain = new LabeledControl<NumericUpDown>(new NumericUpDown(), "Initial Gain:");
            InitialGain.Control.Minimum = 0;
            InitialGain.Control.Increment = 1;
            InitialGain.Control.Maximum = 1024;
            InitialGain.Control.Value = AndorCamera.DefaultMCPGain;
            InitialGain.Control.ValueChanged += (s, e) => OnOptionsChanged(s, e);
            this.Controls.Add(InitialGain);
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

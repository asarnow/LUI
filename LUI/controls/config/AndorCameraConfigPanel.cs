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
    class AndorCameraConfigPanel : LuiObjectConfigPanel
    {
        protected LabeledControl<TextBox> Dir;

        public override Type Target
        {
            get { return typeof(AndorCamera); }
        }

        public AndorCameraConfigPanel()
            : base()
        {
            Dir = new LabeledControl<TextBox>(new TextBox(), "Andor INI Dir:");
            Dir.Control.Text = "./";
            Dir.Control.TextChanged += (s, e) => ConfigChanged(s, e);
            this.Controls.Add(Dir);
        }

        public override void CopyFrom(LuiObjectParameters p)
        {
            CameraParameters q = (CameraParameters)p;
            q.Dir = Dir.Control.Text;
        }

        public override void CopyTo(LuiObjectParameters p)
        {
            CameraParameters q = (CameraParameters)p;
            Dir.Control.Text = q.Dir;
        }
    }
}

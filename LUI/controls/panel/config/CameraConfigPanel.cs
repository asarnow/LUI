using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using lasercom.camera;

namespace LUI.controls
{
    public abstract class CameraConfigPanel : LuiObjectConfigPanel<CameraParameters>
    {
        LabeledControl<TextBox> CalFile;

        public CameraConfigPanel() : base()
        {
            CalFile = new LabeledControl<TextBox>(new TextBox(), "Calibration file:");
            CalFile.Control.Text = "";
            CalFile.Control.TextChanged += (s, e) => OnOptionsChanged(s, e);
            this.Controls.Add(CalFile);
        }

        public override void CopyFrom(CameraParameters other)
        {
            other.CalFile = CalFile.Control.Text;
        }

        public override void CopyTo(CameraParameters other)
        {
            CalFile.Control.Text = other.CalFile;
        }
    }
}

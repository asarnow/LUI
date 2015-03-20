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
    class CameraTempControlledConfigPanel : AndorCameraConfigPanel
    {
        LabeledControl<NumericUpDown> Temperature;

        public override Type Target
        {
            get { return typeof(CameraTempControlled); }
        }

        public CameraTempControlledConfigPanel()
            : base()
        {
            Temperature = new LabeledControl<NumericUpDown>(new NumericUpDown(), "Temperature (C):");
            Temperature.Control.Increment = 1;
            Temperature.Control.Value = lasercom.Constants.DefaultTemperature;
            Temperature.Control.ValueChanged += (s, e) => ConfigChanged(s, e);
            this.Controls.Add(Temperature);
        }

        public override void CopyFrom(LuiObjectParameters p)
        {
            base.CopyFrom(p);
            CameraParameters q = (CameraParameters)p;
            Temperature.Control.Value = q.Temperature;
        }

        public override void CopyTo(LuiObjectParameters p)
        {
            base.CopyTo(p);
            CameraParameters q = (CameraParameters)p;
            q.Temperature = (int)Temperature.Control.Value;
        }
    }
}

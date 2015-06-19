using System;
using System.Windows.Forms;
using lasercom.camera;

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
            Temperature.Control.Minimum = -30;
            Temperature.Control.Maximum = 25;
            Temperature.Control.Value = lasercom.Constants.DefaultTemperature;
            Temperature.Control.ValueChanged += (s, e) => OnOptionsChanged(s,e);
            this.Controls.Add(Temperature);
        }

        public override void CopyFrom(CameraParameters other)
        {
            base.CopyFrom(other);
            Temperature.Control.Value = other.Temperature;
        }

        public override void CopyTo(CameraParameters other)
        {
            base.CopyTo(other);
            other.Temperature = (int)Temperature.Control.Value;
        }
    }
}

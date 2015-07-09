using System;
using System.Windows.Forms;
using lasercom.control;

namespace LUI.controls
{
    class BeamFlagsConfigPanel : LuiObjectConfigPanel<BeamFlagsParameters>
    {
        LabeledControl<ComboBox> COMPort;
        LabeledControl<NumericUpDown> Delay;

        override public Type Target
        {
            get
            {
                return typeof(BeamFlags);
            }
        }

        public BeamFlagsConfigPanel()
            : base()
        {
            COMPort = new LabeledControl<ComboBox>(new ComboBox(), "COM Port:");
            lasercom.Util.EnumerateSerialPorts().ForEach(x => COMPort.Control.Items.Add(x));
            COMPort.Control.SelectedIndexChanged += OnOptionsChanged;
            this.Controls.Add(COMPort);

            Delay = new LabeledControl<NumericUpDown>(new NumericUpDown(), "Delay after open (ms):");
            Delay.Control.Minimum = 0;
            Delay.Control.Maximum = 800;
            Delay.Control.Value = BeamFlags.DefaultDelay;
            Delay.Control.ValueChanged += OnOptionsChanged;
            this.Controls.Add(Delay);
        }
        

        override public void CopyTo(BeamFlagsParameters other)
        {
            other.PortName = (string)COMPort.Control.SelectedItem;
            other.Delay = (int)Delay.Control.Value;
        }

        override public void CopyFrom(BeamFlagsParameters other)
        {
            COMPort.Control.SelectedItem = other.PortName;
            Delay.Control.Value = other.Delay;
        }
    }
}

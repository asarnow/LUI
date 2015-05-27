using lasercom.control;
using System;
using System.Windows.Forms;

namespace LUI.controls
{
    class HarvardPumpConfigPanel : LuiObjectConfigPanel<PumpParameters>
    {

        LabeledControl<ComboBox> COMPort;

        public override Type Target
        {
            get { return typeof(HarvardPump); }
        }

       public HarvardPumpConfigPanel()
            : base()
        {
            COMPort = new LabeledControl<ComboBox>(new ComboBox(), "COM Port:");
            lasercom.Util.EnumerateSerialPorts().ForEach(x => COMPort.Control.Items.Add(x));
            COMPort.Control.SelectedIndexChanged += (s, e) => OnOptionsChanged(s, e);
            this.Controls.Add(COMPort);
        }

        public override void CopyTo(PumpParameters other)
        {
            other.PortName = (string)COMPort.Control.SelectedItem;
        }

        public override void CopyFrom(PumpParameters other)
        {
            COMPort.Control.SelectedItem = other.PortName;
        }

    }
}

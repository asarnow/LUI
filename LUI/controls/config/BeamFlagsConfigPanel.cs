using lasercom.control;
using lasercom.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LUI.controls
{
    class BeamFlagsConfigPanel : LuiObjectConfigPanel
    {
        LabeledControl<ComboBox> COMPort;

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
            COMPort.Control.SelectedIndexChanged += (s, e) => ConfigChanged(s, e);
            //this.Controls.Add(ProviderName);
            this.Controls.Add(COMPort);
        }

        override public void CopyTo(LuiObjectParameters q)
        {
            BeamFlagsParameters p = (BeamFlagsParameters)q;
            p.PortName = (string)COMPort.Control.SelectedItem;
        }

        override public void CopyFrom(LuiObjectParameters q)
        {
            BeamFlagsParameters p = (BeamFlagsParameters)q;
            COMPort.Control.SelectedItem = p.PortName;
        }
    }
}

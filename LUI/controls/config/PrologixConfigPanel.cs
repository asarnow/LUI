using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using lasercom.gpib;
using lasercom.objects;

namespace LUI.controls
{
    class PrologixConfigPanel : LuiObjectConfigPanel<GpibProviderParameters>
    {
        LabeledControl<ComboBox> PrologixCOMPort;
        LabeledControl<NumericUpDown> PrologixTimeout;

        override public Type Target
        {
            get
            {
                return typeof(PrologixGpibProvider);
            }
        }
        
        public PrologixConfigPanel()
            : base()
        {
            PrologixCOMPort = new LabeledControl<ComboBox>(new ComboBox(), "COM Port:");
            lasercom.Util.EnumerateSerialPorts().ForEach(x => PrologixCOMPort.Control.Items.Add(x));
            PrologixCOMPort.Control.SelectedIndexChanged += (s, e) => OnOptionsChanged(e);
            PrologixTimeout = new LabeledControl<NumericUpDown>(new NumericUpDown(), "Timeout (ms):");
            PrologixTimeout.Control.Increment = 1;
            PrologixTimeout.Control.ValueChanged += (s, e) => OnOptionsChanged(e);
            this.Controls.Add(PrologixCOMPort);
            this.Controls.Add(PrologixTimeout);
        }

        override public void CopyTo(GpibProviderParameters other)
        {
            other.PortName = (string)PrologixCOMPort.Control.SelectedItem;
            other.Timeout = (int)PrologixTimeout.Control.Value;
        }

        override public void CopyFrom(GpibProviderParameters other)
        {
            PrologixCOMPort.Control.SelectedItem = other.PortName;
            PrologixTimeout.Control.Value = other.Timeout;
        }
    }
}

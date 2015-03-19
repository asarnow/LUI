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
    class PrologixConfigPanel : LuiObjectConfigPanel
    {
        LabeledControl<ComboBox> PrologixCOMPort;
        LabeledControl<NumericUpDown> PrologixTimeout;
        //LabeledControl<TextBox> ProviderName;

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
            //ProviderName = new LabeledControl<TextBox>(new TextBox(), "Name:");
            //ProviderName.Control.TextChanged += (s, e) => ConfigChanged(s, e);
            PrologixCOMPort = new LabeledControl<ComboBox>(new ComboBox(), "COM Port:");
            lasercom.Util.EnumerateSerialPorts().ForEach(x => PrologixCOMPort.Control.Items.Add(x));
            PrologixCOMPort.Control.SelectedIndexChanged += (s, e) => ConfigChanged(s, e);
            PrologixTimeout = new LabeledControl<NumericUpDown>(new NumericUpDown(), "Timeout (ms):");
            PrologixTimeout.Control.Increment = 1;
            PrologixTimeout.Control.ValueChanged += (s, e) => ConfigChanged(s, e);
            //this.Controls.Add(ProviderName);
            this.Controls.Add(PrologixCOMPort);
            this.Controls.Add(PrologixTimeout);
        }

        override public void CopyTo(LuiObjectParameters q)
        {
            GpibProviderParameters p = (GpibProviderParameters)q;
            //p.Name = ProviderName.Control.Text;
            p.PortName = (string)PrologixCOMPort.Control.SelectedItem;
            p.Timeout = (int)PrologixTimeout.Control.Value;
        }

        override public void CopyFrom(LuiObjectParameters q)
        {
            GpibProviderParameters p = (GpibProviderParameters)q;
            //ProviderName.Control.Text = p.Name;
            PrologixCOMPort.Control.SelectedItem = p.PortName;
            PrologixTimeout.Control.Value = p.Timeout;
        }
    }
}

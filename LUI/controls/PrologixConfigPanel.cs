using lasercom.gpib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LUI.controls
{
    class PrologixConfigPanel : GPIBProviderConfigPanel
    {
        LabeledControl<ComboBox> PrologixCOMPort;
        LabeledControl<NumericUpDown> PrologixTimeout;
        LabeledControl<TextBox> ProviderName;

        override public Type ParameterType
        {
            get
            {
                return typeof(PrologixGPIBProviderParameters);
            }
        }
        
        public PrologixConfigPanel()
            : base()
        {
            ProviderName = new LabeledControl<TextBox>(new TextBox(), "Name:");
            ProviderName.Control.TextChanged += (s, e) => ConfigChanged(s, e);
            PrologixCOMPort = new LabeledControl<ComboBox>(new ComboBox(), "COM Port:");
            lasercom.Util.EnumerateSerialPorts().ForEach(x => PrologixCOMPort.Control.Items.Add(x));
            PrologixCOMPort.Control.SelectedIndexChanged += (s, e) => ConfigChanged(s, e);
            PrologixTimeout = new LabeledControl<NumericUpDown>(new NumericUpDown(), "Timeout (ms):");
            PrologixTimeout.Control.Increment = 1;
            PrologixTimeout.Control.ValueChanged += (s, e) => ConfigChanged(s, e);
            this.Controls.Add(ProviderName);
            this.Controls.Add(PrologixCOMPort);
            this.Controls.Add(PrologixTimeout);
        }

        public void CopyTo(PrologixGPIBProviderParameters p)
        {
            p.Name = ProviderName.Control.Text;
            p.PortName = (string)PrologixCOMPort.Control.SelectedItem;
            p.Timeout = (int)PrologixTimeout.Control.Value;
        }

        public void CopyFrom(PrologixGPIBProviderParameters p)
        {
            ProviderName.Control.Text = p.Name;
            PrologixCOMPort.Control.SelectedItem = p.PortName;
            PrologixTimeout.Control.Value = p.Timeout;
        }
    }
}

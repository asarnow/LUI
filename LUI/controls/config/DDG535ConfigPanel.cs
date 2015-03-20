using lasercom.ddg;
using lasercom.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LUI.controls
{
    class DDG535ConfigPanel : LuiObjectConfigPanel
    {

        LabeledControl<ComboBox> GpibAddress;

        public override Type Target
        {
            get { return typeof(DDG535); }
        }

        public DDG535ConfigPanel()
            : base()
        {
            GpibAddress = new LabeledControl<ComboBox>(new ComboBox(), "GPIB Address:");
            for (byte b = byte.MinValue; b < byte.MaxValue; b++) GpibAddress.Control.Items.Add(b);
            GpibAddress.Control.SelectedIndexChanged += (s, e) => ConfigChanged(s, e);
            this.Controls.Add(GpibAddress);
        }

        public override void CopyFrom(LuiObjectParameters p)
        {
            DelayGeneratorParameters q = (DelayGeneratorParameters)p;
            GpibAddress.Control.SelectedItem = q.GPIBAddress;
        }

        public override void CopyTo(LuiObjectParameters p)
        {
            DelayGeneratorParameters q = (DelayGeneratorParameters)p;
            q.GPIBAddress = (byte)GpibAddress.Control.SelectedItem;
        }

    }
}

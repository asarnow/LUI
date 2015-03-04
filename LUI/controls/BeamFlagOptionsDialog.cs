using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using lasercom;

namespace LUI.controls
{
    public partial class BeamFlagOptionsDialog : LUIOptionsDialog
    {

        public BeamFlagOptionsDialog(Size Size, bool Visibility)
        {
            InitializeComponent();
            this.Size = Size;
            this.Visible = Visibility;
            Init();
        }

        public BeamFlagOptionsDialog(Size Size) : this(Size, true) {}

        public BeamFlagOptionsDialog()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            List<string> ports = Util.EnumerateSerialPorts();
            LabeledControl<ComboBox> Port = new LabeledControl<ComboBox>(new ComboBox(), "Serial Port");
            Port.Control.DropDownStyle = ComboBoxStyle.DropDownList;
            Port.Control.Items.AddRange(ports.ToArray());

            Controls.Add(Port);
        }
    }
}

using lasercom.gpib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using lasercom.objects;

namespace LUI.controls
{
    class NIConfigPanel : LuiObjectConfigPanel
    {
        LabeledControl<ComboBox> NIBoardNumber;
        //LabeledControl<TextBox> ProviderName;

        override public Type Target
        {
            get
            {
                return typeof(NIGpibProvider);
            }
        }

        public NIConfigPanel()
            : base()
        {
            //ProviderName = new LabeledControl<TextBox>(new TextBox(), "Name:");
            //ProviderName.Control.TextChanged += (s, e) => ConfigChanged(s, e);
            //ProviderName.Anchor = AnchorStyles.Left;
            NIBoardNumber = new LabeledControl<ComboBox>(new ComboBox(), "Board:");
            NIBoardNumber.Control.DropDownStyle = ComboBoxStyle.DropDownList;
            NIBoardNumber.Control.Items.Add(0);
            NIBoardNumber.Control.SelectedIndexChanged += (s, e) => ConfigChanged(s, e);
            //NIBoardNumber.Anchor = AnchorStyles.Bottom;
            //this.Controls.Add(ProviderName);
            this.Controls.Add(NIBoardNumber);
        }

        override public void CopyTo(LuiObjectParameters q)
        {
            GpibProviderParameters p = (GpibProviderParameters)q;
            p.BoardNumber = (int)NIBoardNumber.Control.SelectedItem;
            //p.Name = ProviderName.Control.Text;
        }

        override public void CopyFrom(LuiObjectParameters q)
        {
            GpibProviderParameters p = (GpibProviderParameters)q;
            NIBoardNumber.Control.SelectedItem = p.BoardNumber;
            //ProviderName.Control.Text = p.Name;
        }
    }
}

using System;
using System.Windows.Forms;
using lasercom.gpib;

namespace LUI.controls
{
    class NIConfigPanel : LuiObjectConfigPanel<GpibProviderParameters>
    {
        LabeledControl<ComboBox> NIBoardNumber;

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
            NIBoardNumber = new LabeledControl<ComboBox>(new ComboBox(), "Board:");
            NIBoardNumber.Control.DropDownStyle = ComboBoxStyle.DropDownList;
            NIBoardNumber.Control.Items.Add(0);
            NIBoardNumber.Control.SelectedIndexChanged += (s, e) => OnOptionsChanged(s,e);
            this.Controls.Add(NIBoardNumber);
        }

        override public void CopyTo(GpibProviderParameters other)
        {
            other.BoardNumber = (int)NIBoardNumber.Control.SelectedItem;;
        }

        override public void CopyFrom(GpibProviderParameters other)
        {
            NIBoardNumber.Control.SelectedItem = other.BoardNumber;
        }
    }
}

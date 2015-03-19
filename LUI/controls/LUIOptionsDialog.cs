using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LUI.controls
{
    public abstract class LuiOptionsDialog : UserControl
    {
        public LuiOptionsDialog()
        {
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        }

        public LuiOptionsDialog(Size Size, bool Visibility) : this()
        {
            this.Size = Size;
            this.Visible = Visibility;
        }

        public LuiOptionsDialog(Size Size)
            : this(Size, true)
        {

        }

        public abstract void OnApply();
    }
}

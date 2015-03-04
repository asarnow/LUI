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
    public abstract partial class LUIOptionsDialog : UserControl
    {
        public LUIOptionsDialog()
        {
            InitializeComponent();
        }

        public LUIOptionsDialog(Size Size, bool Visibility) : this()
        {
            this.Size = Size;
            this.Visible = Visibility;
        }

        public LUIOptionsDialog(Size Size)
            : this(Size, true)
        {

        }

        public abstract void OnApply();
    }
}

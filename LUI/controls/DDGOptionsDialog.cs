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
    public partial class DDGOptionsDialog : LUIOptionsDialog
    {
        public DDGOptionsDialog()
        {
            InitializeComponent();
            Init();
        }

         public DDGOptionsDialog(Size Size, bool Visibility)
        {
            InitializeComponent();
            this.Size = Size;
            this.Visible = Visibility;
            Init();
        }

        public DDGOptionsDialog(Size Size) : this(Size, true) {}

        private void Init()
        {
            SuspendLayout();

            ResumeLayout(false);
        }

        public override void OnApply()
        {
            throw new NotImplementedException();
        }
    }
}

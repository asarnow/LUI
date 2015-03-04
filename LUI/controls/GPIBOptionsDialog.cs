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
    public partial class GPIBOptionsDialog : LUIOptionsDialog
    {
        public GPIBOptionsDialog(Size Size, bool Visibility)
        {
            InitializeComponent();
            this.Size = Size;
            this.Visible = Visibility;
            Init();
        }

        public GPIBOptionsDialog(Size Size) : this(Size, true) {}

        public GPIBOptionsDialog()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            SuspendLayout();
            ListView GPIBProviders = new ListView();
            ResumeLayout(false);
        }

        public override void OnApply()
        {
            throw new NotImplementedException();
        }
    }
}

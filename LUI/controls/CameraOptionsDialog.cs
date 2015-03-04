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
    public partial class CameraOptionsDialog : LUIOptionsDialog
    {

        public CameraOptionsDialog(Size Size, bool Visibility)
        {
            InitializeComponent();
            this.Size = Size;
            this.Visible = Visibility;
            Init();
        }

        public CameraOptionsDialog(Size Size) : this(Size, true) {}

        public CameraOptionsDialog()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {

        }

        public override void OnApply()
        {
            throw new NotImplementedException();
        }
    }
}

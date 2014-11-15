using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LUI
{
    public partial class ParentForm : Form
    {
        Commander Commander;

        public ParentForm(Commander commander)
        {
            InitializeComponent();
            Commander = commander;
            TROSControl TROSControl = new TROSControl(Commander);
            TROSPage.Controls.Add(TROSControl);
        }

        private static void MakeEmebeddable(Form Form)
        {
            Form.TopLevel = false;
            Form.Visible = true;
            Form.FormBorderStyle = FormBorderStyle.None;
            Form.Dock = DockStyle.Fill;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            Commander.Camera.Close();
        }

    }
}

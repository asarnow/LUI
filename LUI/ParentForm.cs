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

        TROSForm TROSForm;

        public ParentForm(Commander Commander)
        {
            InitializeComponent();
            TROSForm = new TROSForm(Commander);
            MakeEmebeddable(TROSForm);
            TROSPage.Controls.Add(TROSForm);
        }

        private static void MakeEmebeddable(Form Form)
        {
            Form.TopLevel = false;
            Form.Visible = true;
            Form.FormBorderStyle = FormBorderStyle.None;
            Form.Dock = DockStyle.Fill;
        }
    }
}

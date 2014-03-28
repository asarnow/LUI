using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace lasercom
{
    public partial class OptionsForm : Form
    {
        public OptionsForm()
        {
            InitializeComponent();
        }

        private void OptionsForm_Load(object sender, EventArgs e)
        {

        }

        private void Close_Click(object sender, EventArgs e)
        {

        }

        private void OptionsListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem selectedItem = OptionsListView.SelectedItems[0];
        }

    }
}

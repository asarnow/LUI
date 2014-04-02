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
    public partial class ControlForm : Form
    {
        private Commander commander;

        public ControlForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void loadTimesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select a times file";
            openFileDialog.Filter = "Text Files (.txt)|*.txt|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                commander.setDelays(openFileDialog.FileName);

            }
        }

        private void Collect_Click(object sender, EventArgs e)
        {
            commander.collect((int)Averages.Value);
        }
    }
}

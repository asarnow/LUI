using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace LUI
{
    public partial class ControlForm : Form
    {
        private Commander Commander;

        public ControlForm(Commander commander)
        {
            Commander = commander;
            InitializeComponent();
            ChartArea MainChart = SpecGraph.ChartAreas.FindByName("MainChart");
            MainChart.AxisX.Minimum = 0;
            MainChart.AxisX.Maximum = Commander.Camera.Width - 1;
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
                Commander.setDelays(openFileDialog.FileName);

            }
        }

        private void Collect_Click(object sender, EventArgs e)
        {
            Commander.collect((int)Averages.Value);
        }
    }
}

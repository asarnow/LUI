using System;
using System.Windows.Forms;

namespace DetectorTester
{
    public partial class TestSelect : Form
    {
        public TestSelect()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form Detector = new DetectorTestForm();
            this.Hide();
            Detector.FormClosed += Detector_FormClosed;
            Detector.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form DG = new DelayGeneratorTestForm();
            this.Hide();
            DG.FormClosed += DG_FormClosed;
            DG.Show();
        }

        void Detector_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        void DG_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }
    }
}

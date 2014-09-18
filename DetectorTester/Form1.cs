using LUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DetectorTester
{
    public partial class Form1 : Form
    {
        private Commander Commander;

        public Form1(Commander commander)
        {
            Commander = commander;
            InitializeComponent();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LUI
{
    public partial class GraphControl : UserControl
    {
        public GraphControl()
        {
            InitializeComponent();
        }

        public void PlotData(int[] X, int[] Y)
        {
            int MinY = Y.Min();
            int MaxY = Y.Max();
            
        }
    }
}

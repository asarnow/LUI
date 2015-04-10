using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using LUI.config;

namespace LUI.tabs
{
    public partial class TroaControl : LUI.tabs.LuiTab
    {
        public TroaControl(LuiConfig Config) : base(Config)
        {
            InitializeComponent();
        }

        protected override void Graph_Click(object sender, MouseEventArgs e)
        {
            base.Graph_Click(sender, e);
        }

        protected override void Collect_Click(object sender, EventArgs e)
        {
            base.Collect_Click(sender, e);
        }

        protected override void DoWork(object sender, DoWorkEventArgs e)
        {
            base.DoWork(sender, e);
        }

        protected override void WorkProgress(object sender, ProgressChangedEventArgs e)
        {
            base.WorkProgress(sender, e);
        }

        protected override void WorkComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            base.WorkComplete(sender, e);
        }
    }
}

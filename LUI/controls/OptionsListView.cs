﻿using System.Drawing;
using System.Windows.Forms;

namespace LUI.controls
{
    class OptionsListView : ListView
    {
        protected override void WndProc(ref Message m)
        {
            // Swallow mouse messages that are not in the client area.
            if (m.Msg >= 0x201 && m.Msg <= 0x209) // Range contains L/R button up, down and dblclk
            {
                Point pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);
                var hit = this.HitTest(pos);
                switch (hit.Location)
                {
                    case ListViewHitTestLocations.AboveClientArea:
                    case ListViewHitTestLocations.BelowClientArea:
                    case ListViewHitTestLocations.LeftOfClientArea:
                    case ListViewHitTestLocations.RightOfClientArea:
                    case ListViewHitTestLocations.None:
                        return;
                }
            }
            base.WndProc(ref m);
        }
    }
}

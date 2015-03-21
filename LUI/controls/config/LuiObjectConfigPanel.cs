using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using lasercom.gpib;
using lasercom.objects;

namespace LUI.controls
{
    public abstract class LuiObjectConfigPanel<T> : FlowLayoutPanel where T : LuiObjectParameters<T>
    {
        public EventHandler ConfigChanged;
        public abstract Type Target
        {
            get;
        }

        public LuiObjectConfigPanel()
        {

        }

        public abstract void CopyTo(T other);
        public abstract void CopyFrom(T other);
    }
}

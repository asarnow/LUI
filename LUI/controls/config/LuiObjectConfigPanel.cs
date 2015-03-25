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
        public event EventHandler OptionsChanged;
        public abstract Type Target
        {
            get;
        }

        public LuiObjectConfigPanel()
        {

        }

        protected virtual void OnOptionsChanged(EventArgs e)
        {
            EventHandler handler = OptionsChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public abstract void CopyTo(T other);
        public abstract void CopyFrom(T other);
    }
}

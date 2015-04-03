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
        /// <summary>
        /// If TriggerEvents is false, OnOptionsChanged will not call the event handler.
        /// </summary>
        public bool TriggerEvents { get; set; }

        public event EventHandler OptionsChanged;
        public abstract Type Target
        {
            get;
        }

        public LuiObjectConfigPanel()
        {
            TriggerEvents = true;
        }

        protected virtual void OnOptionsChanged(object sender, EventArgs e)
        {
            var handler = OptionsChanged;
            if (TriggerEvents && handler != null) handler(this, e);
        }

        public abstract void CopyTo(T other);
        public abstract void CopyFrom(T other);
    }
}

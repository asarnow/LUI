using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using lasercom.gpib;
using Extensions;
using lasercom.objects;

namespace LUI.controls
{
    public abstract class LuiObjectConfigPanel<T> : FlowLayoutPanel where T : LuiObjectParameters<T>
    {
        /// <summary>
        /// If false, OnOptionsChanged will not call the event handler.
        /// </summary>
        public bool TriggerEvents { get; set; }

        /// <summary>
        /// Indicates a child control has had its value changed.
        /// </summary>
        public event EventHandler OptionsChanged;

        /// <summary>
        /// Returns the type of LuiObject configured by the panel.
        /// </summary>
        public abstract Type Target
        {
            get;
        }

        public LuiObjectConfigPanel()
        {
            TriggerEvents = true;
        }

        /// <summary>
        /// Safely trigger OptionsChanged, unless TriggerEvents is false.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnOptionsChanged(object sender, EventArgs e)
        {
            if (TriggerEvents) OptionsChanged.Raise(sender, e);
        }

        /// <summary>
        /// Copies panel content to LuiParameters.
        /// </summary>
        /// <param name="other"></param>
        public abstract void CopyTo(T other);

        /// <summary>
        /// Copes panel content from LuiParameters.
        /// </summary>
        /// <param name="other"></param>
        public abstract void CopyFrom(T other);
    }
}

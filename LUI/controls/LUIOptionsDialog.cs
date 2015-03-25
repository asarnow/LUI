using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LUI.config;
using System.Runtime.CompilerServices;

namespace LUI.controls
{
    public abstract class LuiOptionsDialog : UserControl
    {
        public event EventHandler OptionsChanged;

        protected event EventHandler ConfigChanged;

        private LuiConfig _Config;
        public virtual LuiConfig Config
        {
            get
            {
                return _Config;
            }
            set
            {
                _Config = value;
                EventHandler handler = ConfigChanged;
                if (handler != null)
                {
                    handler(this, EventArgs.Empty);
                }
            }
        }

        public LuiOptionsDialog()
        {
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        }

        public LuiOptionsDialog(Size Size, bool Visibility) : this()
        {
            this.Size = Size;
            this.Visible = Visibility;
        }

        public LuiOptionsDialog(Size Size)
            : this(Size, true)
        {

        }

        protected virtual void OnOptionsChanged(EventArgs e)
        {
            EventHandler handler = OptionsChanged;
            if (handler != null) handler(this, e);
        }

        public abstract void OnApply(object sender, EventArgs e);
    }
}

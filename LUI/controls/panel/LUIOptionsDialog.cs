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
using Extensions;

namespace LUI.controls
{
    /// <summary>
    /// Base class for controls used as options dialogs.
    /// </summary>
    public abstract class LuiOptionsDialog : UserControl
    {
        /// <summary>
        /// Indicates backing config reference changed.
        /// </summary>
        public event EventHandler ConfigChanged;
        /// <summary>
        /// Indicates entire dialog content updated to match a config. May lead to OptionsChanged.
        /// </summary>
        public event EventHandler ConfigMatched;
        /// <summary>
        /// // Indicates piece of dialog content changed.
        /// </summary>
        public event EventHandler OptionsChanged;
        

        private LuiConfig _Config;
        /// <summary>
        /// Gets or sets the LuiConfig object used to read and write configuration options.
        /// Set triggers ConfigChanged.
        /// </summary>
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
                if (handler != null) handler(this, EventArgs.Empty);
            }
        }

        public LuiOptionsDialog()
        {
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
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

        /// <summary>
        /// Copies dialog content from a LuiConfig and triggers ConfigMatched.
        /// </summary>
        /// <param name="config"></param>
        public void MatchConfig(LuiConfig config)
        {
            Update(config);
            EventHandler handler = ConfigMatched;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        /// <summary>
        /// Safely triggers OptionsChanged.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void OnOptionsChanged(object sender, EventArgs e)
        {
            OptionsChanged.Raise(this, e);
        }

        /// <summary>
        /// Copy dialog content from the passed LuiConfig.
        /// </summary>
        /// <param name="config"></param>
        public abstract void Update(LuiConfig config);

        /// <summary>
        /// Defines action taken when LuiConfig used to read/write options is set.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public abstract void HandleConfigChanged(object sender, EventArgs e);

        /// <summary>
        /// Defines action taken when dialog options are applied.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public abstract void HandleApply(object sender, EventArgs e);
    }
}

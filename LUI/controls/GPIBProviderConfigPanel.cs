using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LUI.controls
{
    abstract class GPIBProviderConfigPanel : FlowLayoutPanel
    {
        public EventHandler ConfigChanged;
        public abstract Type ParameterType
        {
            get;
        }
    }
}

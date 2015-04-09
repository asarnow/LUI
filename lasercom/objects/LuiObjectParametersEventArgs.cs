using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lasercom.objects
{
    public class LuiObjectParametersEventArgs : EventArgs
    {
        public LuiObjectParameters Argument { get; set; }
        public LuiObjectParametersEventArgs(LuiObjectParameters p)
            : base()
        {
            Argument = p;
        }
    }
}

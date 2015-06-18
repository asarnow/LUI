using System;

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

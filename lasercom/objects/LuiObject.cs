using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lasercom.objects
{
    /// <summary>
    /// Base class for instrument-specific abstract classes.
    /// </summary>
    public abstract class LuiObject : ILuiObject
    {
        abstract protected void Dispose(bool disposing);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

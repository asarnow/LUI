using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lasercom.control
{
    /// <summary>
    /// Dummy beam flags object implemented using no-ops.
    /// </summary>
    public class DummyBeamFlags:AbstractBeamFlags
    {
        public DummyBeamFlags()
        {
            
        }

        protected override void Dispose(bool disposing)
        {
            // Do nothing.
        }
    }
}

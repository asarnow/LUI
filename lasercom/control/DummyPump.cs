using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lasercom.control
{
    class DummyPump : AbstractPump
    {
        public DummyPump()
        {

        }

        protected override void Dispose(bool disposing)
        {
            // Nothing to dispose.
        }
    }
}

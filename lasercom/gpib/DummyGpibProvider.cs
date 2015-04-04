using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lasercom.gpib
{
    public class DummyGpibProvider : AbstractGpibProvider
    {
        public override void LoggedWrite(byte address, string command)
        {
            
        }

        public override string LoggedQuery(byte address, string command)
        {
            return "";
        }

        protected override void Dispose(bool disposing)
        {
            
        }
    }
}

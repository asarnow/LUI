using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace lasercom.gpib
{
    abstract public class GpibProvider : IGpibProvider
    {
        protected static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        abstract public void LoggedWrite(byte address, string command);

        abstract public string LoggedQuery(byte address, string command);

        abstract protected void Dispose(bool disposing);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

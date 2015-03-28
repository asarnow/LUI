using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using lasercom.objects;

namespace lasercom.gpib
{
    public abstract class AbstractGpibProvider : LuiObject, IGpibProvider
    {
        protected static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        abstract public void LoggedWrite(byte address, string command);

        abstract public string LoggedQuery(byte address, string command);

    }
}

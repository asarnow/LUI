using lasercom.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lasercom.gpib
{
    /// <summary>
    /// Defines the operations supported by a GPIB provider.
    /// </summary>
    public interface IGpibProvider
    {
        void LoggedWrite(byte address, string command);
        string LoggedQuery(byte address, string command);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LUI.gpib
{
    public interface IGPIBProvider
    {
        void LoggedWrite(byte address, string command);
        string LoggedQuery(byte address, string command);
    }
}

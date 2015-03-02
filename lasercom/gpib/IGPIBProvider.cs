using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LUI.gpib
{
    public interface IGPIBProvider
    {
        void LoggedWrite(string command);
        string LoggedQuery(string command);
    }
}

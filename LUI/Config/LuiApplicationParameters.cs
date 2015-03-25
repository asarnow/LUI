using log4net.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LUI.config
{
    public class LuiApplicationParameters
    {
        public string LogLevel { get; set; }

        public string LogFile { get; set; }

        public string ConfigFile { get; set; }
    }
}

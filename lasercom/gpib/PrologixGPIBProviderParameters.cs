using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lasercom.gpib
{
    class PrologixGPIBProviderParameters : GPIBProviderParameters
    {
        public string PortName { get; set; }
        public int Timeout { get; set; }

        public PrologixGPIBProviderParameters(string ProviderType, string PortName, int Timeout)
            : base(ProviderType)
        {
            this.PortName = PortName;
            this.Timeout = Timeout;
        }

        public PrologixGPIBProviderParameters(string PortName, int Timeout)
            : this("PrologixGPIBProvider", PortName, Timeout)
        {

        }

        public object[] ToConstructorArray()
        {
            return new object[] { PortName, Timeout };
        }
    }
}

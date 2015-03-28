using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lasercom.gpib
{
    /// <summary>
    /// Instantiate concrete GPIB providers from parameters.
    /// </summary>
    public class GpibProviderFactory
    {

        public static IGpibProvider CreateGPIBProvider(GpibProviderParameters p)
        {
            return (IGpibProvider)System.Activator.CreateInstance(p.Type, p.ConstructorArray);
        }

        public static GpibProviderParameters CreateGPIBProviderParameters(GpibProviderParameters p)
        {
            GpibProviderParameters q = new GpibProviderParameters();
            q.Type = p.Type;
            q.Name = p.Name;
            q.PortName = p.PortName;
            q.Timeout = p.Timeout;
            q.BoardNumber = p.BoardNumber;
            return q;
        }

    }
}

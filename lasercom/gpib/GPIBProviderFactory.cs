using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lasercom.gpib
{
    class GPIBProviderFactory
    {

        static IGPIBProvider CreateGPIBProvider(GPIBProviderParameters p)
        {
            return (IGPIBProvider)System.Activator.CreateInstance(p.ProviderType, p.ToConstructorArray());
        }

        

    }
}

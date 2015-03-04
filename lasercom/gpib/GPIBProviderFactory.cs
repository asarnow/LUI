using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LUI.gpib
{
    class GPIBProviderFactory
    {

        static IGPIBProvider CreateGPIBProvider(GPIBProviderParameters p)
        {
            return (IGPIBProvider)System.Activator.CreateInstance(Type.GetType(p.ProviderType), p.ToConstructorArray());
        }

        

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lasercom.ddg
{
    public class DelayGeneratorFactory
    {
        public static IDigitalDelayGenerator CreateDelayGenerator(DelayGeneratorParameters p)
        {
            return (IDigitalDelayGenerator)Activator.CreateInstance(p.Type, p.ConstructorArray);
        }
    }
}

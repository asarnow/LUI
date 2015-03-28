using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lasercom.ddg
{
    /// <summary>
    /// Instantiate concrete DDG objects from parameters.
    /// </summary>
    public class DelayGeneratorFactory
    {
        public static IDigitalDelayGenerator CreateDelayGenerator(DelayGeneratorParameters p)
        {
            return (IDigitalDelayGenerator)Activator.CreateInstance(p.Type, p.ConstructorArray);
        }
    }
}

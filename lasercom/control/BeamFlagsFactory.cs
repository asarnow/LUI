using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lasercom.control
{
    class BeamFlagsFactory
    {
        public static AbstractBeamFlags CreateBeamFlags(BeamFlagsParameters p)
        {
            return (AbstractBeamFlags)Activator.CreateInstance(p.Type, p.ConstructorArray);
        }
    }
}

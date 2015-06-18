using System;

namespace lasercom.control
{
    /// <summary>
    /// Instantiate concrete beam flag objects from parameters.
    /// </summary>
    class BeamFlagsFactory
    {
        public static AbstractBeamFlags CreateBeamFlags(BeamFlagsParameters p)
        {
            return (AbstractBeamFlags)Activator.CreateInstance(p.Type, p.ConstructorArray);
        }
    }
}

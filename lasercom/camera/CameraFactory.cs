using System;

namespace lasercom.camera
{
    class CameraFactory
    {
        public static ICamera CreateCamera(CameraParameters p)
        {
            return (ICamera)Activator.CreateInstance(p.Type, p.ConstructorArray);
        }
    }
}

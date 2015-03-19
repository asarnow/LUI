using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

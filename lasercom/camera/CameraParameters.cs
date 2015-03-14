using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lasercom;

namespace lasercom.camera
{
    class CameraParameters
    {
        Type CameraType;

        public CameraParameters(ICamera Camera)
        {
            CameraType = typeof(Camera);
        }
    }
}

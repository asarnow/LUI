using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lasercom;

namespace lasercom.camera
{
    public class CameraParameters
    {
        Type CameraType;

        public CameraParameters(ICamera Camera)
        {
            CameraType = Camera.GetType();
        }
    }
}

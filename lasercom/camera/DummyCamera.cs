using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace lasercom.camera
{
    class DummyCamera : ICamera
    {
        public int Width { get; set; }
        public int Height { get; set; }

        public int[] FullResolutionImage()
        {
            return null;
        }

        public int[] CountsFvb()
        {
            return null;
        }

        public int[] Acquire()
        {
            return null;
        }

        public void Close()
        {

        }
    }
}

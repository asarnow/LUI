using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LUI.camera
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

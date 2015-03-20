using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace lasercom.camera
{
    public class DummyCamera : ICamera
    {
        public uint Width { get; set; }
        public uint Height { get; set; }

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


        public uint AcqSize
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int AcquisitionMode
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int TriggerMode
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int DDGTriggerMode
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int ReadMode
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool HasIntensifier
        {
            get { throw new NotImplementedException(); }
        }

        public int IntensifierGain
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int MinIntensifierGain
        {
            get { throw new NotImplementedException(); }
        }

        public int MaxIntensifierGain
        {
            get { throw new NotImplementedException(); }
        }

        public uint Acquire(int[] DataBuffer)
        {
            throw new NotImplementedException();
        }
    }
}

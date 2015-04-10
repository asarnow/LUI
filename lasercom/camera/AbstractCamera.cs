using lasercom.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lasercom.camera
{
    public abstract class AbstractCamera:LuiObject, ICamera
    {
        public abstract uint Width
        {
            get;
        }

        public abstract uint Height
        {
            get;
        }

        public abstract uint AcqSize
        {
            get;
        }

        public abstract int AcquisitionMode
        {
            get;
            set;
        }

        public abstract int TriggerMode
        {
            get;
            set;
        }

        public abstract int DDGTriggerMode
        {
            get;
            set;
        }

        public abstract int ReadMode
        {
            get;
            set;
        }

        public abstract bool HasIntensifier
        {
            get;
        }

        public abstract int IntensifierGain
        {
            get;
            set;
        }

        public abstract int MinIntensifierGain
        {
            get;
        }

        public abstract int MaxIntensifierGain
        {
            get;
        }
        
        double[] _Calibration;
        public double[] Calibration
        {
            get
            {
                return _Calibration;
            }
            set
            {
                _Calibration = value;
            }
        }

        public abstract int[] CountsFvb();

        public abstract int[] FullResolutionImage();

        public abstract int[] Acquire();

        public abstract uint Acquire(int[] DataBuffer);

    }
}

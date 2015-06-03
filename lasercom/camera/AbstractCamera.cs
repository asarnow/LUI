using lasercom.io;
using lasercom.objects;
using System;
using System.IO;
using System.Linq;

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
            protected set;
        }

        public abstract int MaxIntensifierGain
        {
            get;
            protected set;
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

        public abstract string DecodeStatus(uint status);

        protected void LoadCalibration(string CalFile)
        {
            if (CalFile == null || CalFile == "")
            {
                Calibration = Array.ConvertAll(Enumerable.Range(1, (int)Width).ToArray<int>(), x => (double)x);
            }
            else
            {
                try
                {
                    Calibration = FileIO.ReadVector<double>(CalFile);
                }
                catch (IOException ex)
                {
                    Log.Error(ex);
                    Calibration = Array.ConvertAll(Enumerable.Range(1, (int)Width).ToArray<int>(), x => (double)x);
                    throw;
                }
            }
        }
    }
}

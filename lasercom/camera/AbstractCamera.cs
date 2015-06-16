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

        public bool CalibrationAscending
        {
            get
            {
                return Calibration[Calibration.Length - 1] > Calibration[0];
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
                Calibration = Enumerable.Range(0, (int)Width).Select(x=>(double)x).ToArray();
            }
            else
            {
                try
                {
                    Calibration = FileIO.ReadTimesFile(CalFile).ToArray();
                }
                catch (IOException ex)
                {
                    Log.Error(ex);
                    Calibration = Enumerable.Range(0, (int)Width).Select(x => (double)x).ToArray();
                    throw;
                }
            }
        }
    }
}

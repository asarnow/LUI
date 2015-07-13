using lasercom.io;
using lasercom.objects;
using System.IO;
using System.Linq;

namespace lasercom.camera
{
    public abstract class AbstractCamera:LuiObject<CameraParameters>, ICamera
    {
        public abstract int Width
        {
            get;
        }

        public abstract int Height
        {
            get;
        }

        public abstract int AcqSize
        {
            get;
        }

        public abstract int AcqWidth
        {
            get;
        }

        public abstract int AcqHeight
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

        public abstract ImageArea Image { get; set; }

        public abstract int[] CountsFvb();

        public abstract int[] FullResolutionImage();

        public abstract int[] Acquire();

        public abstract uint Acquire(int[] DataBuffer);

        public abstract string DecodeStatus(uint status);
        
        public override void Update(CameraParameters p)
        {
            LoadCalibration(p.CalFile);
            ReadMode = p.ReadMode;
            Image = p.Image;
            if (HasIntensifier) IntensifierGain = p.InitialGain;
        }

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
                    Calibration = FileIO.ReadVector<double>(CalFile);
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

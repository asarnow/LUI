using System;
#if x64
using ATMCD64CS;
#else
using ATMCD32CS;
using System.Diagnostics;
using System.Linq;
#endif

namespace lasercom.camera
{
    public class DummyAndorCamera : AndorCamera
    {
        new AndorSDK AndorSdk = null;

        private int _ReadMode;
        new public int ReadMode
        {
            get { return _ReadMode; }
            set
            {
                _ReadMode = value;
            }
        }

        private int _AcquisitionMode;
        new public int AcquisitionMode
        {
            get { return _AcquisitionMode; }
            set
            {
                _AcquisitionMode = value;
            }
        }

        private int _TriggerMode;
        new public int TriggerMode
        {
            get { return _TriggerMode; }
            set
            {
                _TriggerMode = value;
            }
        }

        private int _TriggerInvert;
        new public int TriggerInvert
        {
            get { return _TriggerInvert; }
            set
            {
                _TriggerInvert = value;
            }
        }

        private float _TriggerLevel;
        new public float TriggerLevel
        {
            get { return _TriggerLevel; }
            set
            {
                _TriggerLevel = value;
            }
        }

        private int _DDGTriggerMode;
        new public int DDGTriggerMode
        {
            get { return _DDGTriggerMode; }
            set
            {
                _DDGTriggerMode = value;
            }
        }

        private int _GateMode;
        new public int GateMode
        {
            get { return _GateMode; }
            set
            {
                _GateMode = value;
            }
        }

        new public bool HasIntensifier
        {
            get
            {
                return false;
            }
        }

        private int _MCPGating;
        new public int MCPGating
        {
            get { return _MCPGating; }
            set
            {
                _MCPGating = value;
            }
        }

        private int _MCPGain;
        new public int IntensifierGain
        {
            get { return _MCPGain; }
            set
            {
                _MCPGain = value;
            }
        }

        private int _NumberAccumulations;
        new public int NumberAccumulations
        {
            get { return _NumberAccumulations; }
            set
            {
                _NumberAccumulations = value;
            }
        }

        private ImageArea _Image;
        new public ImageArea Image
        {
            get { return _Image; }
            set
            {
                _Image = value;
            }
        }

        private uint _Height;
        public override uint Height
        {
            get
            {
                return _Height;
            }
        }

        private uint _Width;
        public override uint Width
        {
            get
            {
                return _Width;
            }
        }

        public DummyAndorCamera() : base()
        {
            _Width = 1024;
            _Height = 256;
            Calibration = Array.ConvertAll(Enumerable.Range(1, (int)Width).ToArray<int>(), x => (double)x);
            // else load CalFile (or deal with CalFile only in factory)
        }

        public override int[] FullResolutionImage()
        {
            return null;
        }

        public override int[] CountsFvb()
        {
            return null;
        }

        public override int[] Acquire()
        {
            string caller = (new StackFrame(1)).GetMethod().Name;
            int line = (new StackFrame(2)).GetFileLineNumber();
            int[] data = null;
            switch (caller)
            {
                case "Dark":
                    data = Data.Uniform((int)Width, 1000);
                    break;
                case "Flash":
                    if (line > 200)
                        data = Data.Gaussian((int)Width, 32000, Width * 1 / 3, Width / 10);
                    else
                        data = Data.Uniform((int)Width, 2000);
                    break;
                case "Trans":
                    data = Data.Gaussian((int)Width, 32000, Width * 2 / 3, Width / 10);
                    break;
            }
            return data;
        }

        public override uint Acquire(int[] DataBuffer)
        {
            string caller = (new StackFrame(1)).GetMethod().Name;
            int line = (new StackFrame(2)).GetFileLineNumber();
            int[] data = null;
            switch (caller)
            {
                case "Dark":
                    data = Data.Uniform((int)Width, 1000);
                    break;
                case "Flash":
                    if (line > 200)
                        data = Data.Gaussian((int)Width, 32000, Width * 1/3, Width / 10);
                    else
                        data = Data.Uniform((int)Width, 2000);
                    break;
                case "Trans":
                    data = Data.Gaussian((int)Width, 32000, Width * 2/3, Width / 10);
                    break;
            }
            data.CopyTo(DataBuffer, 0);
            return AndorSDK.DRV_SUCCESS;
        }

        public override void Close()
        {
        }
    }
}

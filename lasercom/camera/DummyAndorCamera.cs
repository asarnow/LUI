using System;
#if x64
using ATMCD64CS;
#else
using ATMCD32CS;
#endif

namespace lasercom.camera
{
    class DummyAndorCamera : AndorCamera
    {
        AndorSDK AndorSdk = null;

        private int _ReadMode;
        public int ReadMode
        {
            get { return _ReadMode; }
            set
            {
                _ReadMode = value;
            }
        }

        private int _AcquisitionMode;
        public int AcquisitionMode
        {
            get { return _AcquisitionMode; }
            set
            {
                _AcquisitionMode = value;
            }
        }

        private int _TriggerMode;
        public int TriggerMode
        {
            get { return _TriggerMode; }
            set
            {
                _TriggerMode = value;
            }
        }

        private int _TriggerInvert;
        public int TriggerInvert
        {
            get { return _TriggerInvert; }
            set
            {
                _TriggerInvert = value;
            }
        }

        private float _TriggerLevel;
        public float TriggerLevel
        {
            get { return _TriggerLevel; }
            set
            {
                _TriggerLevel = value;
            }
        }

        private int _DDGTriggerMode;
        public int DDGTriggerMode
        {
            get { return _DDGTriggerMode; }
            set
            {
                _DDGTriggerMode = value;
            }
        }

        private int _GateMode;
        public int GateMode
        {
            get { return _GateMode; }
            set
            {
                _GateMode = value;
            }
        }

        private int _MCPGating;
        public int MCPGating
        {
            get { return _MCPGating; }
            set
            {
                _MCPGating = value;
            }
        }

        private int _MCPGain;
        public int MCPGain
        {
            get { return _MCPGain; }
            set
            {
                _MCPGain = value;
            }
        }

        private int _NumberAccumulations;
        public int NumberAccumulations
        {
            get { return _NumberAccumulations; }
            set
            {
                _NumberAccumulations = value;
            }
        }

        private ImageArea _Image;
        public ImageArea Image
        {
            get { return _Image; }
            set
            {
                _Image = value;
            }
        }

        private uint _Height;
        public uint Height
        {
            get
            {
                return _Height;
            }
        }

        private uint _Width;
        public uint Width
        {
            get
            {
                return _Width;
            }
        }

        public class ImageArea
        {
            public readonly int hbin, vbin, hstart, hend, vstart, vend;
            public ImageArea(int hbin, int vbin, int hstart, int hend, int vstart, int vend)
            {
                this.hbin = hbin;
                this.vbin = vbin;
                this.hstart = hstart;
                this.hend = hend;
                this.vstart = vstart;
                this.vend = vend;

            }
        }

        public DummyAndorCamera()
        {
            _Width = 1024;
            _Height = 256;
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
            return null;
        }

        public override void Close()
        {
        }
    }
}

using System;
#if x64
using ATMCD64CS;
#else
using ATMCD32CS;
#endif


namespace lasercom
{
    /// <summary>
    /// Class representing a generic Andor camera.
    /// Specific Andor camera types should inherit from this class.
    /// </summary>
    public class AndorCamera:ICamera
    {
        // Andor constants and commands
        public const int ReadModeFVB = 0;
        public const int ReadModeMultiTrack = 1;
        public const int ReadModeRandomTrack = 2;
        public const int ReadModeSingleTrack = 3;
        public const int ReadModeImage = 4;

        public const int AcquisitionModeSingle = 1;
        public const int AcquisitionModeAccumulate = 2;

        public const int GatingModeSMBOnly = 2;

        public const int DDGTriggerModeInternal = 0;
        public const int DDGTriggerModeExternal = 1;

        public const int TriggerModeExternal = 1;
        public const int TriggerModeExternalExposure = 7;

        public const int TriggerInvertRising = 0;
        public const int TriggerInvertFalling = 1;
        public const float DefaultTriggerLevel = 3.9F;

        public const int MCPGatingOff = 0;
        public const int MCPGatingOn = 1;
        public const int DefaultMCPGain = 500;

        public uint InitVal;
        public AndorSDK AndorSdk = new AndorSDK();
        public AndorSDK.AndorCapabilities Capabilities;

        private int _ReadMode;
        public int ReadMode
        {
            get { return _ReadMode; }
            set
            {
                _ReadMode = value;
                AndorSdk.SetReadMode(value);
            }
        }

        private int _AcquisitionMode;
        public int AcquisitionMode
        {
            get { return _AcquisitionMode; }
            set
            {
                _AcquisitionMode = value;
                AndorSdk.SetAcquisitionMode(value);
            }
        }

        private int _TriggerMode;
        public int TriggerMode
        {
            get { return _TriggerMode; }
            set
            {
                _TriggerMode = value;
                AndorSdk.SetTriggerMode(value);
            }
        }

        private int _TriggerInvert;
        public int TriggerInvert
        {
            get { return _TriggerInvert; }
            set
            {
                _TriggerInvert = value;
                AndorSdk.SetTriggerInvert(value);
            }
        }

        private float _TriggerLevel;
        public float TriggerLevel
        {
            get { return _TriggerLevel; }
            set
            {
                _TriggerLevel = value;
                AndorSdk.SetTriggerLevel(value);
            }
        }

        private int _DDGTriggerMode;
        public int DDGTriggerMode
        {
            get { return _DDGTriggerMode; }
            set
            {
                _DDGTriggerMode = value;
                AndorSdk.SetDDGTriggerMode(value);
            }
        }

        private int _GateMode;
        public int GateMode
        {
            get { return _GateMode; }
            set
            {
                _GateMode = value;
                AndorSdk.SetGateMode(value);
            }
        }

        private int _MCPGating;
        public int MCPGating
        {
            get { return _MCPGating; }
            set
            {
                _MCPGating = value;
                AndorSdk.SetMCPGating(value);
            }
        }

        private int _MCPGain;
        public int MCPGain
        {
            get { return _MCPGain; }
            set
            {
                _MCPGain = value;
                AndorSdk.SetMCPGain(value);
            }
        }

        private int _NumberAccumulations;
        public int NumberAccumulations
        {
            get { return _NumberAccumulations; }
            set
            {
                _NumberAccumulations = value;
                AndorSdk.SetNumberAccumulations(value);
            }
        }

        private ImageArea _Image;
        public ImageArea Image
        {
            get { return _Image; }
            set 
            {
                _Image = value;
                AndorSdk.SetImage(value.hbin, value.vbin, value.hstart, value.hend, value.vstart, value.vend);
            }
        }

        private uint _Height;
        virtual public uint Height
        {
            get
            {
                return _Height;
            }
        }

        private uint _Width;
        virtual public uint Width
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

        public AndorCamera() : this(".") {}
        
        public AndorCamera(String dir)
        {
            if (dir != null)
            {
                InitVal = AndorSdk.Initialize(dir);
                AndorSdk.GetCapabilities(ref Capabilities);
                AndorSdk.FreeInternalMemory();

                int width = 0, height = 0;
                AndorSdk.GetDetector(ref width, ref height);
                _Width = (uint)width;
                _Height = (uint)height;

                Image = new ImageArea(1, 1, 1, (int)Width, 1, (int)Height);

                GateMode = Constants.GatingModeSMBOnly;
                MCPGating = Constants.MCPGatingOn;

                //TriggerInvert = Constants.TriggerInvertRising;
                //TriggerLevel = Constants.DefaultTriggerLevel; // TTL signal is 4.0V
                MCPGain = Constants.DefaultMCPGain;
            }
        }

        public virtual void Close()
        {
            if (AndorSdk != null) AndorSdk.ShutDown();
        }

        public virtual int[] FullResolutionImage()
        {
            uint npx = Width * Height;
            int[] data = new int[npx];
            AndorSdk.SetReadMode(Constants.ReadModeImage);
            AndorSdk.SetImage(1, 1, 1, (int)Width, 1, (int)Height);
            AndorSdk.StartAcquisition();
            AndorSdk.WaitForAcquisition();
            uint ret = AndorSdk.GetAcquiredData(data, npx);
            ResetReadMode();
            ResetImage();
            return data;
        }

        public virtual int[] CountsFvb()
        {
            uint npx = Width;
            AndorSdk.SetReadMode(Constants.ReadModeFVB);
            int[] data = new int[npx];
            AndorSdk.StartAcquisition();
            AndorSdk.WaitForAcquisition();
            uint ret = AndorSdk.GetAcquiredData(data, npx);
            ResetReadMode();
            return data;
        }

        public virtual int[] Acquire()
        {
            uint npx;
            if (this.ReadMode == Constants.ReadModeFVB)
            {
                npx = Width;
            }
            else if (ReadMode == Constants.ReadModeImage)
            {
                npx = (uint)((Image.hend - Image.hstart + 1) * (Image.vend - Image.vstart + 1));
            }
            else
            {
                throw new NotImplementedException();
            }

            int[] data = new int[npx];
            AndorSdk.StartAcquisition();
            AndorSdk.WaitForAcquisition();
            uint ret = AndorSdk.GetAcquiredData(data, npx);
            return data;
        }

        public void ResetCameraParameters()
        {
            ResetAcquisitionMode();
            ResetReadMode();
            ResetTriggerMode();
            ResetDDGTriggerMode();
            ResetNumberAccumulations();
            ResetImage();
        }

        public void ResetTriggerMode()
        {
            AndorSdk.SetTriggerMode(TriggerMode);
        }

        public void ResetDDGTriggerMode()
        {
            AndorSdk.SetDDGTriggerMode(DDGTriggerMode);
        }

        public void ResetAcquisitionMode()
        {
            AndorSdk.SetAcquisitionMode(AcquisitionMode);
        }

        public void ResetReadMode()
        {
            AndorSdk.SetReadMode(ReadMode);
        }

        public void ResetNumberAccumulations()
        {
            AndorSdk.SetNumberAccumulations(NumberAccumulations);
        }

        public void ResetImage()
        {
            AndorSdk.SetImage(Image.hbin, Image.vbin, Image.hstart, Image.hend, Image.vstart, Image.vend);
        }

    }
}

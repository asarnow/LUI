using System;
using System.Linq;

#if x64
using ATMCD64CS;
#else
using ATMCD32CS;

#endif


namespace lasercom.camera
{
    /// <summary>
    /// Class representing a generic Andor camera.
    /// Specific Andor camera types should inherit from this class.
    /// </summary>
    public class AndorCamera:AbstractCamera
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
        public const int DefaultMCPGain = 10;

        public const int DefaultADChannel = 0;

        public uint InitVal;
        public AndorSDK AndorSdk = new AndorSDK();
        public AndorSDK.AndorCapabilities Capabilities;

        private int _ReadMode;
        public override int ReadMode
        {
            get { return _ReadMode; }
            set
            {
                _ReadMode = value;
                AndorSdk.SetReadMode(value);
            }
        }

        private int _AcquisitionMode;
        public override int AcquisitionMode
        {
            get { return _AcquisitionMode; }
            set
            {
                _AcquisitionMode = value;
                AndorSdk.SetAcquisitionMode(value);
            }
        }

        private int _TriggerMode;
        public override int TriggerMode
        {
            get { return _TriggerMode; }
            set
            {
                _TriggerMode = value;
                AndorSdk.SetTriggerMode(value);
            }
        }

        private int _TriggerInvert;
        public virtual int TriggerInvert
        {
            get { return _TriggerInvert; }
            set
            {
                _TriggerInvert = value;
                AndorSdk.SetTriggerInvert(value);
            }
        }

        private float _TriggerLevel;
        public virtual float TriggerLevel
        {
            get { return _TriggerLevel; }
            set
            {
                _TriggerLevel = value;
                AndorSdk.SetTriggerLevel(value);
            }
        }

        private int _DDGTriggerMode;
        public override int DDGTriggerMode
        {
            get { return _DDGTriggerMode; }
            set
            {
                _DDGTriggerMode = value;
                AndorSdk.SetDDGTriggerMode(value);
            }
        }

        private int _GateMode;
        public virtual int GateMode
        {
            get { return _GateMode; }
            set
            {
                _GateMode = value;
                AndorSdk.SetGateMode(value);
            }
        }

        public override bool HasIntensifier
        {
            get
            {
                return true;
            }
        }

        private int _MCPGating;
        public virtual int MCPGating
        {
            get { return _MCPGating; }
            set
            {
                _MCPGating = value;
                AndorSdk.SetMCPGating(value);
            }
        }

        private int _MinMCPGain;
        public override int MinIntensifierGain
        {
            get
            {
                return _MinMCPGain;
            }
            protected set
            {
                _MinMCPGain = value;
            }
        }
        private int _MaxMCPGain;
        public override int MaxIntensifierGain
        {
            get
            {
                return _MaxMCPGain;
            }
            protected set
            {
                _MaxMCPGain = value;
            }
        }

        private int _MCPGain;
        public override int IntensifierGain
        {
            get { return _MCPGain; }
            set
            {
                _MCPGain = value;
                AndorSdk.SetMCPGain(value);
            }
        }

        private int _NumberAccumulations;
        public virtual int NumberAccumulations
        {
            get { return _NumberAccumulations; }
            set
            {
                _NumberAccumulations = value;
                AndorSdk.SetNumberAccumulations(value);
            }
        }

        private ImageArea _Image;
        public virtual ImageArea Image
        {
            get { return _Image; }
            set 
            {
                _Image = value;
                AndorSdk.SetImage(value.hbin, value.vbin, value.hstart, value.hend, value.vstart, value.vend);
            }
        }

        private uint _Height;
        override public uint Height
        {
            get
            {
                return _Height;
            }
        }

        private uint _Width;
        override public uint Width
        { 
            get
            {
                return _Width; 
            } 
        }

        private int _BitDepth;
        public int BitDepth 
        {
            get
            {
                return _BitDepth;
            }
        }

        private int _NumberADChannels;
        public int NumberADChannels
        {
            get
            {
                return _NumberADChannels;
            }
        }

        private int _CurrentADChannel;
        public int CurrentADChannel
        {
            get
            {
                return _CurrentADChannel;
            }
            set
            {
                //TODO check return from Andor
                //TODO ALL properties in this class need this fix and code reorder to reflect
                AndorSdk.SetADChannel(value);
                _CurrentADChannel = value;
            }
        }

        public override uint AcqSize
        {
            get
            {
                if (this.ReadMode == Constants.ReadModeFVB)
                {
                    return Width;
                }
                else if (ReadMode == Constants.ReadModeImage)
                {
                    return (uint)((Image.hend - Image.hstart + 1) * (Image.vend - Image.vstart + 1));
                }
                else
                {
                    throw new NotImplementedException();
                }
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
        
        public AndorCamera() : this(null, ".") { }

        public AndorCamera(string CalFile = null, string dir = ".")
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
                AndorSdk.GetNumberADChannels(ref _NumberADChannels);
                CurrentADChannel = DefaultADChannel;
                AndorSdk.GetBitDepth(CurrentADChannel, ref _BitDepth);

                Image = new ImageArea(1, 1, 1, (int)Width, 1, (int)Height);

                GateMode = Constants.GatingModeSMBOnly;
                MCPGating = Constants.MCPGatingOn;

                //TriggerInvert = Constants.TriggerInvertRising;
                //TriggerLevel = Constants.DefaultTriggerLevel; // TTL signal is 4.0V
                AndorSdk.GetMCPGainRange(ref _MinMCPGain, ref _MaxMCPGain);
                IntensifierGain = Constants.DefaultMCPGain;
            }

            if (CalFile == null || CalFile == "")
                Calibration = Array.ConvertAll(Enumerable.Range(1, (int)Width).ToArray<int>(), x => (double)x);
            // else load CalFile (or deal with CalFile only in factory)
        }

        public virtual void Close()
        {
            if (AndorSdk != null) AndorSdk.ShutDown();
        }

        public override int[] FullResolutionImage()
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

        public override int[] CountsFvb()
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

        public override int[] Acquire()
        {
            uint npx = AcqSize;
            int[] data = new int[npx];
            uint ret = Acquire(data);
            return data;
        }

        /// <summary>
        /// Acquire data and store in referenced array.
        /// This overload supports memory efficient acquisition if the same
        /// array is continually re-passed.
        /// The array must be a legal size for acquisition.
        /// AndorSDK return codes (uint):
        /// DRV_SUCCESS             Data copied. 
        /// DRV_NOT_INITIALIZED     System not initialized.
        /// DRV_ACQUIRING           Acquisition in progress.
        /// DRV_ERROR_ACK           Unable to communicate with card.
        /// DRV_P1INVALID           Invalid pointer (i.e. NULL).
        /// DRV_P2INVALID           Array size is incorrect.
        /// DRV_NO_NEW_DATA         No acquisition has taken place
        /// </summary>
        /// <param name="DataBuffer"></param>
        /// <returns></returns>
        public override uint Acquire(int[] DataBuffer)
        {
            uint npx = (uint)DataBuffer.Length;
            AndorSdk.StartAcquisition();
            AndorSdk.WaitForAcquisition();
            uint ret = AndorSdk.GetAcquiredData(DataBuffer, npx);
            return ret;
        }

        public override string DecodeStatus(uint status)
        {
            switch (status)
            {
                case AndorSDK.DRV_SUCCESS:
                    return "DRV_SUCCESS";
                case AndorSDK.DRV_NOT_INITIALIZED:
                    return "DRV_NOT_INITIALIZED";
                case AndorSDK.DRV_ACQUIRING:
                    return "DRV_ACQUIRING";
                case AndorSDK.DRV_ERROR_ACK:
                    return "DRV_ERROR_ACK";
                case AndorSDK.DRV_P11INVALID:
                    return "DRV_P1INVALID";
                case AndorSDK.DRV_P2INVALID:
                    return "DRV_P2INVALID";
                case AndorSDK.DRV_NO_NEW_DATA:
                    return "DRV_NO_NEW_DATA";
                default:
                    return "BAD CODE";
            }
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


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Close();
            }
        }
    }
}

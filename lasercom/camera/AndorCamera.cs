using System;
#if x64
using ATMCD64CS;
#else
using ATMCD32CS;
#endif

//  <summary>
//      Class representing a generic Andor camera.
//      Specific Andor camera types should inherit from this class.
//  </summary>

namespace LUI
{
    public class AndorCamera:ICamera
    {
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
        public uint Height { get; }
        private uint _Width;
        public uint Width { get; }

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
            InitVal = AndorSdk.Initialize(dir);
            AndorSdk.GetCapabilities(ref Capabilities);
            AndorSdk.FreeInternalMemory();

            int width = 0, height = 0;
            AndorSdk.GetDetector(ref width, ref height);
            _Width = (uint)width;
            _Height = (uint)height;

            Image = new ImageArea(1, 1, 1, (int)Width, 1, (int)Height);

            AndorSdk.SetTriggerInvert(Constants.TriggerInvertRising);
            AndorSdk.SetTriggerLevel(Constants.DefaultTriggerLevel); // TTL signal is 4.0V
            AndorSdk.SetMCPGain(Constants.DefaultMCPGain);
        }

        public virtual void Close()
        {
            AndorSdk.ShutDown();
        }

        public int[] FullResolutionImage()
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

        public int[] CountsFvb()
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

        public int[] Acquire()
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
            ResetNumberAccumulations();
            ResetImage();
        }

        public void ResetTriggerMode()
        {
            AndorSdk.SetTriggerMode(TriggerMode);
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
            if (NumberAccumulations != null) AndorSdk.SetNumberAccumulations(NumberAccumulations);
        }

        public void ResetImage()
        {
            AndorSdk.SetImage(Image.hbin, Image.vbin, Image.hstart, Image.hend, Image.vstart, Image.vend);
        }

    }
}

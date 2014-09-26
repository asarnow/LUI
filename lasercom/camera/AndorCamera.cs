using System;
//#if x64
using ATMCD64CS;
//#else
//using ATMCD32CS;
//#endif

//  <summary>
//      Abstract class representing a camera. Any camera class implementation
//      should inherit from this class.
//  </summary>

namespace LUI
{
    public class AndorCamera:ICamera
    {
        public uint InitVal;
        public AndorSDK AndorSdk = new AndorSDK();
        public AndorSDK.AndorCapabilities Capabilities;

        public int ReadMode
        {
            get { return ReadMode; }
            set
            {
                ReadMode = value;
                AndorSdk.SetReadMode(value);
            }
        }
        public int AcquisitionMode
        {
            get { return AcquisitionMode; }
            set
            {
                AcquisitionMode = value;
                AndorSdk.SetAcquisitionMode(value);
            }
        }
        public int TriggerMode
        {
            get { return TriggerMode; }
            set
            {
                TriggerMode = value;
                AndorSdk.SetTriggerMode(value);
            }
        }
        public int NumberAccumulations
        {
            get { return NumberAccumulations; }
            set
            {
                NumberAccumulations = value;
                AndorSdk.SetNumberAccumulations(value);
            }
        }
        public uint Height;
        public uint Width;

        public AndorCamera(String dir)
        {
            InitVal = AndorSdk.Initialize(dir);
            AndorSdk.GetCapabilities(ref Capabilities);
            AndorSdk.FreeInternalMemory();

            Width = 1024; //TODO detect these values
            Height = 256;
        }

        public int[] GetCountsFvb()
        {
            throw new NotImplementedException();
        }

        public virtual void Close()
        {
            AndorSdk.ShutDown();
        }

        public int[] GetImage()
        {
            return FullResolutionImage();
        }

        public int[] FullResolutionImage()
        {
            uint npx = Width * Height;
            int[] data = new int[npx];
            ReadMode = Constants.ReadModeImage;
            AndorSdk.SetImage(1, 1, 1, (int)Width, 1, (int)Height);
            AcquisitionMode = Constants.AcquisitionModeSingle;
            TriggerMode = Constants.TriggerModeExternalExposure;
            AndorSdk.StartAcquisition();
            AndorSdk.WaitForAcquisition();
            uint ret = AndorSdk.GetAcquiredData(data, npx);
            return data;
        }
    }
}

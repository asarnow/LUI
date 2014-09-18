using System;
#if x64
using ATMCD64CS;
#else
using ATMCD32CS;
#endif

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

        public int ReadMode;
        public int AcqMode;
        public int TrigMode;
        public int NumberAccumulations;
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
            return GetFullResolutionImage();
        }

        public int[] GetFullResolutionImage()
        {
            uint npx = Width * Height;
            int[] data = new int[npx];
            SetReadMode(Constants.ReadModeImage);
            AndorSdk.SetImage(1,1,1,(int)Width,1,(int)Height);
            SetAcquisitionMode(Constants.AcqModeSingle);
            SetTriggerMode(Constants.TrigModeExternalExposure);
            AndorSdk.StartAcquisition();
            AndorSdk.WaitForAcquisition();
            uint ret = AndorSdk.GetAcquiredData(data, npx);
            return data;
        }

        public void SetReadMode(int readMode)
        {
            this.ReadMode = readMode;
            AndorSdk.SetReadMode(readMode);
        }

        public void SetAcquisitionMode(int acqMode)
        {
            this.AcqMode = acqMode;
            AndorSdk.SetAcquisitionMode(acqMode);
        }

        public void SetTriggerMode(int trigMode)
        {
            this.TrigMode = trigMode;
            AndorSdk.SetTriggerMode(trigMode);
        }

        public void SetNumberAccumulations(int n)
        {
            this.NumberAccumulations = n;
            AndorSdk.SetNumberAccumulations(n);
        }
    }
}

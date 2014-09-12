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
        }

        public int[] GetCountsFvb()
        {
            throw new NotImplementedException();
        }

        public virtual void Close()
        {
            AndorSdk.ShutDown();
        }


        public void GetImage()
        {
            throw new NotImplementedException();
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

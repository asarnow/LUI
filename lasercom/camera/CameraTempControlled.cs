using System;

#if x64
using ATMCD64CS;
#else
using ATMCD32CS;
#endif

//  <summary>
//      Temperature controlled camera.
//  </summary>

namespace lasercom.camera
{

    public class CameraTempControlled:AndorCamera
    {
        public const float TemperatureEps = 3F;
        public const int ShutdownTemp = 5;

        private int _MinTemp;
        public int MinTemp
        {
            get
            {
                return _MinTemp;
            }
        }

        private int _MaxTemp;
        public int MaxTemp
        {
            get
            {
                return MaxTemp;
            }
        }

        public int Temperature
        {
            get
            {
                int currentTemperature = 0;
                AndorSdk.GetTemperature(ref currentTemperature);
                return currentTemperature;
            }
        }

        public float TemperatureF
        {
            get
            {
                float currentTemperature = 0;
                AndorSdk.GetTemperatureF(ref currentTemperature);
                return currentTemperature;
            }
        }

        public CameraTempControlled(string CalFile, string Dir, int InitialGain, int Temperature)
            : base(CalFile, Dir, InitialGain)
        {
            AndorSdk.GetTemperatureRange(ref _MinTemp, ref _MaxTemp);
            AndorSdk.CoolerON();
            EquilibrateTemperature(Temperature);
        }

        public void EquilibrateTemperature(int targetTemperature)
        {
            if (targetTemperature < MinTemp || targetTemperature > MaxTemp)
            {
                throw new ArgumentException("Temperature out of range.");
            }
            AndorSdk.SetTemperature(targetTemperature);
            float currentTemperature = 0;
            AndorSdk.GetTemperatureF(ref currentTemperature);
            while ( Math.Abs(currentTemperature - targetTemperature) > TemperatureEps )
            {
                AndorSdk.GetTemperatureF(ref currentTemperature);
            }
        }

        public void EquilibrateTemperature()
        {
            int currentTemperature = 0;
            uint status = AndorSDK.DRV_TEMP_NOT_STABILIZED;
            while (status != AndorSDK.DRV_TEMP_STABILIZED)
            {
                status = AndorSdk.GetTemperature(ref currentTemperature);
            }
        }

        public void WaitForTemperatureIncrease(int thresholdTemperature)
        {
            float currentTemperature = 0;
            AndorSdk.GetTemperatureF(ref currentTemperature);
            while (currentTemperature < (thresholdTemperature - TemperatureEps))
            {
                AndorSdk.GetTemperatureF(ref currentTemperature);
            }
        }

        public override void Close()
        {
            if (AndorSdk != null)
            {
                AndorSdk.CoolerOFF();
                WaitForTemperatureIncrease(ShutdownTemp);
                AndorSdk.ShutDown();
            }
        }

    }

}

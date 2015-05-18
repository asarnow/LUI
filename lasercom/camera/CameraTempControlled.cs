using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

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

        public CameraTempControlled(string CalFile, string Dir, int InitialGain, int Temperature)
            : base(CalFile, Dir, InitialGain)
        {
            EquilibrateTemperature(Temperature);
        }

        public void EquilibrateTemperature(int targetTemperature)
        {
            AndorSdk.CoolerON();
            AndorSdk.SetTemperature(targetTemperature);
            float currentTemperature = 0;
            AndorSdk.GetTemperatureF(ref currentTemperature);
            while ( Math.Abs(currentTemperature - targetTemperature) > Constants.TemperatureEps )
            {
                AndorSdk.GetTemperatureF(ref currentTemperature);
            }
        }

        public override void Close()
        {
            if (AndorSdk != null)
            {
                AndorSdk.CoolerOFF();
                AndorSdk.ShutDown();
            }
        }

    }

}

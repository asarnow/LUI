﻿using System;

#if x64
using ATMCD64CS;
#else
using ATMCD32CS;
using System.Threading.Tasks;
using System.Threading;
using lasercom.objects;
#endif

//  <summary>
//      Temperature controlled camera.
//  </summary>

namespace lasercom.camera
{

    public class CameraTempControlled:AndorCamera
    {
        public const int DefaultTemperature = 20;
        public const float TemperatureEps = 3F;
        public const int ShutdownTemp = 5;

        public const uint TemperatureStabilized = AndorSDK.DRV_TEMP_STABILIZED;
        public const uint TemperatureNotStabilized = AndorSDK.DRV_TEMP_NOT_STABILIZED;

        protected int _MinTemp;
        public int MinTemp
        {
            get
            {
                return _MinTemp;
            }
        }

        protected int _MaxTemp;
        public int MaxTemp
        {
            get
            {
                return _MaxTemp;
            }
        }

        public virtual int Temperature
        {
            get
            {
                int currentTemperature = 0;
                AndorSdk.GetTemperature(ref currentTemperature);
                return currentTemperature;
            }
        }

        public virtual float TemperatureF
        {
            get
            {
                float currentTemperature = 0;
                AndorSdk.GetTemperatureF(ref currentTemperature);
                return currentTemperature;
            }
        }

        public virtual uint TemperatureStatus
        {
            get
            {
                int currentTemperature = 0;
                return AndorSdk.GetTemperature(ref currentTemperature);
            }
        }

        public CameraTempControlled() { }

        public CameraTempControlled(LuiObjectParameters p) : this(p as CameraParameters) { }

        public CameraTempControlled(CameraParameters p) : base(p)
        {
            AndorSdk.GetTemperatureRange(ref _MinTemp, ref _MaxTemp);
            AndorSdk.CoolerON();
            EquilibrateTemperature(p.Temperature);
        }

        public override void Update(CameraParameters p)
        {
            base.Update(p);
            EquilibrateTemperature(p.Temperature);
        }

        public virtual void EquilibrateTemperature(int targetTemperature, CancellationToken? token = null)
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
                if (token.HasValue && token.Value.IsCancellationRequested) break;
                AndorSdk.GetTemperatureF(ref currentTemperature);
            }
        }

        public virtual void EquilibrateTemperature(CancellationToken? token = null)
        {
            int currentTemperature = 0;
            uint status = AndorSDK.DRV_TEMP_NOT_STABILIZED;
            while (status != AndorSDK.DRV_TEMP_STABILIZED)
            {
                if (token.HasValue && token.Value.IsCancellationRequested) break;
                status = AndorSdk.GetTemperature(ref currentTemperature);
            }
        }

        public async Task EquilibrateTemperatureAsync(int targetTemperature)
        {
            await Task.Run(() => EquilibrateTemperature(targetTemperature));
        }

        public async Task EquilibrateTemperatureAsync(int targetTemperature, CancellationToken token)
        {
            await Task.Run(() => EquilibrateTemperature(targetTemperature, token));
        }

        public async Task EquilibrateTemperatureAsync()
        {
            await Task.Run(() => EquilibrateTemperature());
        }

        public async Task EquilibrateTemperatureAsync(CancellationToken token)
        {
            await Task.Run(() => EquilibrateTemperature(token));
        }

        public bool EquilibrateUntil(Func<bool> BreakoutCondition)
        {
            return EquilibrateUntil(BreakoutCondition, 200);
        }

        public virtual bool EquilibrateUntil(Func<bool> BreakoutCondition, int PollDelayMs)
        {
            int currentTemperature = 0;
            uint status = AndorSDK.DRV_TEMP_NOT_STABILIZED;
            while (status != AndorSDK.DRV_TEMP_STABILIZED)
            {
                status = AndorSdk.GetTemperature(ref currentTemperature);
                if (BreakoutCondition()) return true;
                Thread.Sleep(PollDelayMs);
            }
            return false;
        }

        public virtual void WaitForTemperatureIncrease(int thresholdTemperature)
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

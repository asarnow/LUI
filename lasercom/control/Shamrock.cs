using System;
using ATSHAMROCKCS;

namespace lasercom.control
{
    class Shamrock : AbstractSpectrograph
    {
        ShamrockSDK ShamrockSdk = new ShamrockSDK();
        private object Shamlock;
        private int Device { get; set; }

        private volatile uint _LastStatus;
        public uint LastStatus
        {
            get
            {
                return _LastStatus;
            }
            private set
            {
                _LastStatus = value;
            }
        }

        private int _NumGratings;
        public int NumGratings
        {
            get
            {
                return _NumGratings;
            }
        }

        public int Grating
        {
            get
            {
                lock (Shamlock)
                {
                    int g = -1;
                    LastStatus = ShamrockSdk.ShamrockGetGrating(Device, ref g);
                    return g;
                }
            }
            set
            {
                lock (Shamlock)
                {
                    LastStatus = ShamrockSdk.ShamrockSetGrating(Device, value);
                }
            }
        }

        private float _MinWavelength;
        public float MinWavelength
        {
            get
            {
                return _MinWavelength;
            }
            private set
            {
                _MinWavelength = value;
            }
        }

        private float _MaxWavelength;
        public float MaxWavelength
        {
            get
            {
                return _MaxWavelength;
            }
            private set
            {
                _MaxWavelength = value;
            }
        }

        public override double Wavelength
        {
            get
            {
                lock (Shamlock)
                {
                    float wl = float.NaN;
                    LastStatus = ShamrockSdk.ShamrockGetWavelength(Device, ref wl);
                    return wl;
                }
            }
            set
            {
                lock (Shamlock)
                {
                    if (value < MinWavelength || value > MaxWavelength) throw new ArgumentException("Wavelength limit error.");
                    LastStatus = ShamrockSdk.ShamrockSetWavelength(Device, (float)value);
                }
                
            }
        }

        private double _SlitWidth;
        public override double SlitWidth
        {
            get
            {
                return _SlitWidth;
            }
            set
            {
                lock (Shamlock)
                {
                    LastStatus = ShamrockSdk.ShamrockSetAutoSlitWidth(Device, 0, (float)value);
                    _SlitWidth = value;
                }
            }
        }

        public Shamrock(int Device = 0)
        {
            this.Device = Device;
            ShamrockSdk.ShamrockInitialize("");
            ShamrockSdk.ShamrockGetNumberGratings(Device, ref _NumGratings);
            ShamrockSdk.ShamrockGetWavelengthLimits(Device, Grating, ref _MinWavelength, ref _MaxWavelength);
        }

        public override void Close()
        {
            throw new NotImplementedException();
        }
    }
}

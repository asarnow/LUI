using System;
using NationalInstruments.NI4882;
using log4net;
using LUI.gpib;

//  <summary>
//      Represents a Stanford DDG.
//  </summary>

namespace lasercom.ddg
{
    public abstract class StanfordDigitalDelayGenerator:IDigitalDelayGenerator
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IGPIBProvider GPIBProvider { get; set; }

        public StanfordDigitalDelayGenerator(IGPIBProvider _GPIBProvider)
        {
            GPIBProvider = _GPIBProvider;
        }

        void IDigitalDelayGenerator.SetADelay(double delay)
        {
            throw new NotImplementedException();
        }

        void IDigitalDelayGenerator.SetBDelay(double delay)
        {
            throw new NotImplementedException();
        }

        void IDigitalDelayGenerator.SetCDelay(double delay)
        {
            throw new NotImplementedException();
        }

        void IDigitalDelayGenerator.SetDDelay(double delay)
        {
            throw new NotImplementedException();
        }

        double IDigitalDelayGenerator.GetADelay()
        {
            throw new NotImplementedException();
        }

        double IDigitalDelayGenerator.GetBDelay()
        {
            throw new NotImplementedException();
        }

        double IDigitalDelayGenerator.GetCDelay()
        {
            throw new NotImplementedException();
        }

        double IDigitalDelayGenerator.GetDDelay()
        {
            throw new NotImplementedException();
        }
    }
}

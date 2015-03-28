using System;
using NationalInstruments.NI4882;
using log4net;
using lasercom.gpib;

//  <summary>
//      Represents a Stanford DDG.
//  </summary>

namespace lasercom.ddg
{
    public abstract class StanfordDigitalDelayGenerator:AbstractDigitalDelayGenerator
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IGpibProvider GPIBProvider { get; set; }
        public byte GPIBAddress { get; set; }

        public StanfordDigitalDelayGenerator(IGpibProvider _GPIBProvider, byte _GPIBAddress)
        {
            GPIBProvider = _GPIBProvider;
            GPIBAddress = _GPIBAddress;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Nothing to dispose of.
            }
        }

    }
}

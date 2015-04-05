using System;
using System.Linq;
using NationalInstruments.NI4882;
using log4net;
using lasercom.gpib;
using lasercom.objects;

//  <summary>
//      Represents a Stanford DDG.
//  </summary>

namespace lasercom.ddg
{
    /// <summary>
    /// Base class for Stanford Instruments DDGs.
    /// </summary>
    public abstract class StanfordDigitalDelayGenerator:AbstractDigitalDelayGenerator
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public IGpibProvider GPIBProvider { get; set; }
        public byte GPIBAddress { get; set; }

        public StanfordDigitalDelayGenerator(byte _GPIBAddress, params ILuiObject[] dependencies)
        {
            GPIBProvider = (IGpibProvider)dependencies.First(d => d is IGpibProvider);
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

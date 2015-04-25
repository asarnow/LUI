using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lasercom.ddg
{
    /// <summary>
    /// Dummy DDG implementing required methods as no-ops.
    /// </summary>
    public class DummyDigitalDelayGenerator:AbstractDigitalDelayGenerator
    {

        public DummyDigitalDelayGenerator()
        {

        }

        public static string[] Delays = { "A" };

        public static string[] Triggers = { "T0", "A" };

        public double T0Delay { get; set; }

        public double ADelay { get; set; }
        //public Delay ARelative { get; set; }

        public double BDelay { get; set; }
        //public Delay BRelative { get; set; }

        public double ABDelay { get; set; }

        public double CDelay { get; set; }
        //public Delay CRelative { get; set; }

        public double DDelay { get; set; }
        //public Delay DRelative { get; set; }

        public double CDDelay { get; set; }

        public void LoggedWrite(string command)
        {
        }

        public void SetADelay(double delay)
        {
            ADelay = delay;
        }

        public void SetBDelay(double delay)
        {
            BDelay = delay;
        }

        public void SetCDelay(double delay)
        {
            CDDelay = delay;
        }

        public void SetDDelay(double delay)
        {
            DDelay = delay;
        }

        public double GetADelay()
        {
            return ADelay;
        }

        public double GetBDelay()
        {
            return BDelay;
        }

        public double GetCDelay()
        {
            return CDDelay;
        }

        public double GetDDelay()
        {
            return DDelay;
        }

        protected override void Dispose(bool disposing)
        {
            // Do nothing.
        }
    }
}

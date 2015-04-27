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

        public override string[] Delays
        {
            get
            {
                return new string[] { "A", "B" };
            }
        }

        public override string[] Triggers
        {
            get
            {
                return new string[] { "T0", "A" };
            }
        }

        public override string[] DelayPairs
        {
            get
            {
                return new string[] { "AB" };
            }
        }

        public double T0Delay { get; set; }

        public double ADelay { get; set; }

        public double BDelay { get; set; }

        public void SetADelay(double delay)
        {
            ADelay = delay;
        }

        public void SetBDelay(double delay)
        {
            BDelay = delay;
        }

        public double GetADelay()
        {
            return ADelay;
        }

        public double GetBDelay()
        {
            return BDelay;
        }

        protected override void Dispose(bool disposing)
        {
            // Do nothing.
        }

        public override void SetDelay(char DelayName, char TriggerName, double Delay)
        {
            switch (DelayName)
            {
                case 'A':
                    SetADelay(Delay);
                    break;
                case 'B':
                    SetBDelay(Delay);
                    break;
                default:
                    throw new ArgumentException("Illegal delay output given.");
            }
        }

        public override void SetDelayPulse(Tuple<char, char> DelayPair, char TriggerName, double Delay, double Width)
        {
            SetDelay(DelayPair.Item1, TriggerName, Delay);
            SetDelay(DelayPair.Item2, DelayPair.Item1, Width);
        }
    }
}

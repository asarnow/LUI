using System;

namespace lasercom.ddg
{
    /// <summary>
    /// Dummy DDG implementing required methods as no-ops.
    /// </summary>
    public class DummyDigitalDelayGenerator:AbstractDigitalDelayGenerator
    {

        public DummyDigitalDelayGenerator()
        {
            ADelay = "T,+0.01";
            BDelay = "A,+0.02";
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
                return new string[] { "T", "A" };
            }
        }

        public override string[] DelayPairs
        {
            get
            {
                return new string[] { "AB" };
            }
        }

        public string ADelay { get; set; }

        public string BDelay { get; set; }

        public void SetADelay(string Trigger, double delay)
        {
            ADelay = Trigger + "," + delay.ToString();
        }

        public void SetBDelay(string Trigger, double delay)
        {
            BDelay = Trigger + "," + delay.ToString();
        }

        public string GetADelay()
        {
            return ADelay;
        }

        public string GetBDelay()
        {
            return BDelay;
        }

        protected override void Dispose(bool disposing)
        {
            // Do nothing.
        }

        public override void SetDelay(string DelayName, string TriggerName, double Delay)
        {
            switch (DelayName)
            {
                case "A":
                    SetADelay(TriggerName, Delay);
                    break;
                case "B":
                    SetBDelay(TriggerName, Delay);
                    break;
                default:
                    throw new ArgumentException("Illegal delay output given.");
            }
        }

        public override void SetDelayPulse(Tuple<string, string> DelayPair, string TriggerName, double Delay, double Width)
        {
            SetDelay(DelayPair.Item1, TriggerName, Delay);
            SetDelay(DelayPair.Item2, DelayPair.Item1, Width);
        }

        public override string GetDelay(string DelayName)
        {
            switch (DelayName)
            {
                case "A":
                    return ADelay;
                case "B":
                    return BDelay;
                default:
                    throw new ArgumentException("Illegal delay specified");
            }
        }

        public override string GetDelayTrigger(string DelayName)
        {
            return GetDelay(DelayName).Split(',')[0];
        }

        public override double GetDelayValue(string DelayName)
        {
            return Double.Parse(GetDelay(DelayName).Split(',')[1]);
        }

    }
}

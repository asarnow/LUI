using System;
using System.Linq;
using lasercom.objects;

namespace lasercom.ddg
{
    /// <summary>
    /// Base class for all DDGs.
    /// </summary>
    public abstract class AbstractDigitalDelayGenerator : LuiObject, IDigitalDelayGenerator
    {

        public abstract void SetDelay(string DelayName, string TriggerName, double Delay);

        public abstract void SetDelayPulse(Tuple<string, string> DelayPair, string TriggerName, double Delay, double Width);

        public abstract string GetDelay(string DelayName);

        public abstract string GetDelayTrigger(string DelayName);

        public abstract double GetDelayValue(string DelayName);

        public abstract string[] Delays
        {
            get;
        }

        public abstract string[] DelayPairs
        {
            get;
        }

        public abstract string[] Triggers
        {
            get;
        }

        public virtual string[] GetAllowedTriggers(string DelayName)
        {
            return Triggers.Except(new string[] { DelayName }).ToArray();
        }

    }
}

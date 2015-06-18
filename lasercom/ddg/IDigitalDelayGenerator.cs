using System;

namespace lasercom.ddg
{
    /// <summary>
    /// Defines the operations supported by a DDG.
    /// </summary>
    public interface IDigitalDelayGenerator
    {
        void SetDelay(string DelayName, string TriggerName, double Delay);
        void SetDelayPulse(Tuple<string, string> DelayPair, string TriggerName, double Delay, double Width);

        string GetDelay(string DelayName);
        string GetDelayTrigger(string DelayName);
        double GetDelayValue(string DelayName);

        string[] GetAllowedTriggers(string DelayName);

        string[] Delays
        {
            get;
        }

        string[] DelayPairs
        {
            get;
        }

        string[] Triggers
        {
            get;
        }

    }
}

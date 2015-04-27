using lasercom.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lasercom.ddg
{
    /// <summary>
    /// Defines the operations supported by a DDG.
    /// </summary>
    public interface IDigitalDelayGenerator
    {
        void SetDelay(char DelayName, char TriggerName, double Delay);
        void SetDelayPulse(Tuple<char, char> DelayPair, char TriggerName, double Delay, double Width);

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

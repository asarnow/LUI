using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LUI
{
    public interface IDigitalDelayGenerator
    {
        void LoggedWrite(String command);

        void SetADelay(double delay);
        void SetBDelay(double delay);
        void SetCDelay(double delay);
        void SetDDelay(double delay);

        double GetADelay();
        double GetBDelay();
        double GetCDelay();
        double GetDDelay();
    }
}

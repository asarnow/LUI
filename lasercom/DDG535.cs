using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using log4net;
using NationalInstruments.NI4882;

namespace LUI
{
    public class DDG535:AbstractDigitalDelayGenerator
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private double T0Delay { public get; public set; }
        private double ADelay { public get; private set; }
        private double BDelay { public get; private set; }
        private double ABDelay { public get; private set; }
        private double CDelay { public get; private set; }
        private double DDelay { public get; private set; }
        private double CDDelay { public get; private set; }

        public DDG535(int address):base(address)
        {
            
        }

        public void SetADelay(double delay)
        {
            ADelay = delay;
            String command = Constants.DDG535.SetDelayTimeCommand +
                Constants.DDG535.AOutput + "," +
                Constants.DDG535.T0Output + "," +
                delay.ToString();
            LoggedWrite(command);
        }

        public void SetBDelay(double delay)
        {
            BDelay = delay;
            String command = Constants.DDG535.SetDelayTimeCommand +
                Constants.DDG535.BOutput + "," +
                Constants.DDG535.T0Output + "," +
                delay.ToString();
            LoggedWrite(command);
        }

        public void SetCDelay(double delay)
        {
            CDelay = delay;
            String command = Constants.DDG535.SetDelayTimeCommand +
                Constants.DDG535.COutput + "," +
                Constants.DDG535.T0Output + "," +
                delay.ToString();
            LoggedWrite(command);
        }

        public void SetDDelay(double delay)
        {
            DDelay = delay;
            String command = Constants.DDG535.SetDelayTimeCommand +
                Constants.DDG535.DOutput + "," +
                Constants.DDG535.T0Output + "," +
                delay.ToString();
            LoggedWrite(command);
        }
    }
}

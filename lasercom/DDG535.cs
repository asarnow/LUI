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

        public const string SetDelayTimeCommand = "DT ";
        public const string TriggerInput = "0";
        public const string T0Output = "1";
        public const string AOutput = "2";
        public const string BOutput = "3";
        public const string ABOutput = "4";
        public const string COutput = "5";
        public const string DOutput = "6";
        public const string CDOutput = "7";

        private double T0Delay { public get; public set; }

        private double ADelay { public get; private set; }
        //private Delay ARelative { public get; private set; }

        private double BDelay { public get; private set; }
        //private Delay BRelative { public get; private set; }

        private double ABDelay { public get; private set; }
        
        private double CDelay { public get; private set; }
        //private Delay CRelative { public get; private set; }

        private double DDelay { public get; private set; }
        //private Delay DRelative { public get; private set; }

        private double CDDelay { public get; private set; }

        //public enum Delay { T0, A, B, AB, C, D, CD }

        public DDG535(int address):base(address)
        {
            
        }

        private void ReadAllDelays()
        {
            string command = SetDelayTimeCommand + AOutput;
            string response = LoggedWriteRead(command);
            if (response != null)
            {
                string[] tok = response.Split('+');
                double ADelay = double.Parse( tok[1] );
                string relative = tok[0];
            }
        }

        public void SetADelay(double delay, string relative = T0Output)
        {
            ADelay = delay;
            string command = SetDelayTimeCommand +
                AOutput + "," +
                relative + "," +
                delay.ToString();
            LoggedWrite(command);
        }

        public void SetBDelay(double delay, string relative = T0Output)
        {
            BDelay = delay;
            string command = SetDelayTimeCommand +
                BOutput + "," +
                relative + "," +
                delay.ToString();
            LoggedWrite(command);
        }

        public void SetCDelay(double delay, string relative = T0Output)
        {
            CDelay = delay;
            string command = SetDelayTimeCommand +
                COutput + "," +
                relative + "," +
                delay.ToString();
            LoggedWrite(command);
        }

        public void SetDDelay(double delay, string relative = T0Output)
        {
            DDelay = delay;
            string command = SetDelayTimeCommand +
                DOutput + "," +
                relative + "," +
                delay.ToString();
            LoggedWrite(command);
        }
    }
}

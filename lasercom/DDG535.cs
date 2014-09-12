using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using log4net;
using NationalInstruments.NI4882;

namespace LUI
{
    public class DDG535:StanfordDigitalDelayGenerator
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public const string SetDelayTimeCommand = "DT ";
        public const string TriggerInput = "0";
        public const string T0Output = "1";
        public const string AOutput = "2";
        public const string BOutput = "3";
        public const string ABOutput = "4";
        public const string COutput = "5";
        public const string DOutput = "6";
        public const string CDOutput = "7";

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

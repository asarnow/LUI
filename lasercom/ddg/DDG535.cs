using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using log4net;
using NationalInstruments.NI4882;

namespace lasercom.ddg
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

        private string _ADelay;
        public string ADelay
        {
            get
            {
                return _ADelay;
            }
            set
            {
                _ADelay = value;
                SetDelay(AOutput, _ADelay);
            }
        }

        private string _BDelay;
        public string BDelay
        {
            get
            {
                return _BDelay;
            }
            set
            {
                _BDelay = value;
                SetDelay(BOutput, _BDelay);
            }
        }

        private string _CDelay;
        public string CDelay
        {
            get
            {
                return _CDelay;
            }
            set
            {
                _CDelay = value;
                SetDelay(COutput, _CDelay);
            }
        }

        private string _DDelay;
        public string DDelay
        {
            get
            {
                return _DDelay;
            }
            set
            {
                _DDelay = value;
                SetDelay(DOutput, _DDelay);
            }
        }

        public DDG535(int address):base(address)
        {
            ReadAllDelays();
        }

        private void SetDelay(string DelayOutput, string setting)
        {
            string command = SetDelayTimeCommand + DelayOutput + "," + setting;
            LoggedWrite(command);
        }

        public void SetADelay(double delay, string relative = T0Output)
        {
            ADelay = relative + "," + delay.ToString();
        }

        public void SetBDelay(double delay, string relative = T0Output)
        {
            BDelay = relative + "," + delay.ToString();
        }

        public void SetCDelay(double delay, string relative = T0Output)
        {
            CDelay = relative + "," + delay.ToString();
        }

        public void SetDDelay(double delay, string relative = T0Output)
        {
            DDelay = relative + "," + delay.ToString();
        }

        private void ReadAllDelays()
        {
            ReadADelay();
            ReadBDelay();
            ReadCDelay();
            ReadDDelay();
        }

        private void ReadADelay()
        {
            string command = SetDelayTimeCommand + AOutput;
            // e.g. "1,+0.001000000000"
            string response = LoggedQuery(command);
            if (response != null && response.StartsWith(AOutput + ","))
            {
                _ADelay = response;
            }
        }

        private void ReadBDelay()
        {
            string command = SetDelayTimeCommand + BOutput;
            // e.g. "1,+0.001000000000"
            string response = LoggedQuery(command);
            if (response != null && response.StartsWith(BOutput + ","))
            {
                _BDelay = response;
            }
        }

        private void ReadCDelay()
        {
            string command = SetDelayTimeCommand + COutput;
            // e.g. "1,+0.001000000000"
            string response = LoggedQuery(command);
            if (response != null && response.StartsWith(COutput + ","))
            {
                _CDelay = response;
            }
        }

        private void ReadDDelay()
        {
            string command = SetDelayTimeCommand + DOutput;
            // e.g. "1,+0.001000000000"
            string response = LoggedQuery(command);
            if (response != null && response.StartsWith(DOutput + ","))
            {
                _DDelay = response;
            }
        }
    }
}

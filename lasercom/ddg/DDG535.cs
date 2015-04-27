using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using lasercom.gpib;
using NationalInstruments.NI4882;
using lasercom.objects;

namespace lasercom.ddg
{
    /// <summary>
    /// Represents a Stanford Instruments DDG 535.
    /// </summary>
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

        public const byte DefaultGPIBAddress = 15;

        private static Dictionary<string, string> _DelayMap = new Dictionary<string,string>
        { 
            { "T0", T0Output },
            { "A", AOutput },
            { "B", BOutput },
            {"AB", ABOutput},
            { "C", COutput },
            { "D", DOutput },
            {"CD", CDOutput}
        };

        public static Dictionary<string, string> DelayMap
        {
            get
            {
                return _DelayMap;
            }
        }

        public override string[] Delays
        {
            get
            {
                return _DelayMap.Keys.Where(x => x.Length == 1 && x != "T0").ToArray();
            }
        }

        public override string[] DelayPairs
        {
            get
            {
                return _DelayMap.Keys.Where(x => x.Length == 2).ToArray();
            }
        }

        public override string[] Triggers
        {
            get
            {
                return _DelayMap.Keys.Where(x => x.Length == 1 && x != "D").ToArray();
            }
        }

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

        //public DDG535(int address):base(address)
        //{
        //    ReadAllDelays();
        //}

        public DDG535(byte GPIBAddress, params ILuiObject[] dependencies) : base(GPIBAddress, dependencies)
        {
            ReadAllDelays();
        }

        /// <summary>
        /// Sets any delay.
        /// </summary>
        /// <param name="DelayName"></param>
        /// <param name="TriggerName"></param>
        /// <param name="Delay"></param>
        public override void SetDelay(char DelayName, char TriggerName, double Delay)
        {
            string DelayOutput = DelayMap[DelayName.ToString()];
            string TriggerOutput = DelayMap[TriggerName.ToString()];
            SetDelay(DelayOutput, TriggerOutput, Delay);
        }

        /// <summary>
        /// Sets paired delays for pulse generation.
        /// </summary>
        /// <param name="DelayPair"></param>
        /// <param name="TriggerName"></param>
        /// <param name="Delay"></param>
        /// <param name="Width"></param>
        public override void SetDelayPulse(Tuple<char, char> DelayPair, char TriggerName, double Delay, double Width)
        {
            string DelayOutput1 = DelayMap[DelayPair.Item1.ToString()];
            string DelayOutput2 = DelayMap[DelayPair.Item2.ToString()];
            string TriggerOutput = DelayMap[TriggerName.ToString()];
            SetDelay(DelayOutput1, TriggerOutput, Delay);
            SetDelay(DelayOutput2, DelayOutput1, Width);
        }

        /// <summary>
        /// Internal use only - calls function to update property and write to device.
        /// </summary>
        /// <param name="DelayOutput"></param>
        /// <param name="TriggerOutput"></param>
        /// <param name="Delay"></param>
        private void SetDelay(string DelayOutput, string TriggerOutput, double Delay)
        {
            switch (DelayOutput)
            {
                case AOutput:
                    SetADelay(Delay, TriggerOutput);
                    break;
                case BOutput:
                    SetBDelay(Delay, TriggerOutput);
                    break;
                case COutput:
                    SetCDelay(Delay, TriggerOutput);
                    break;
                case DOutput:
                    SetDDelay(Delay, TriggerOutput);
                    break;
                default:
                    throw new ArgumentException("Illegal delay output given.");
            }
        }

        /// <summary>
        /// Internal use only - writes delay to device.
        /// </summary>
        /// <param name="DelayOutput"></param>
        /// <param name="setting"></param>
        private void SetDelay(string DelayOutput, string setting)
        {
            string command = SetDelayTimeCommand + DelayOutput + "," + setting;
            GPIBProvider.LoggedWrite(GPIBAddress, command);
        }

        /// <summary>
        /// Sets A delay, may be used externally.
        /// </summary>
        /// <param name="delay"></param>
        /// <param name="relative"></param>
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
            string response = GPIBProvider.LoggedQuery(GPIBAddress, command);
            if (response != null && response.StartsWith(AOutput + ","))
            {
                _ADelay = response;
            }
        }

        private void ReadBDelay()
        {
            string command = SetDelayTimeCommand + BOutput;
            // e.g. "1,+0.001000000000"
            string response = GPIBProvider.LoggedQuery(GPIBAddress, command);
            if (response != null && response.StartsWith(BOutput + ","))
            {
                _BDelay = response;
            }
        }

        private void ReadCDelay()
        {
            string command = SetDelayTimeCommand + COutput;
            // e.g. "1,+0.001000000000"
            string response = GPIBProvider.LoggedQuery(GPIBAddress, command);
            if (response != null && response.StartsWith(COutput + ","))
            {
                _CDelay = response;
            }
        }

        private void ReadDDelay()
        {
            string command = SetDelayTimeCommand + DOutput;
            // e.g. "1,+0.001000000000"
            string response = GPIBProvider.LoggedQuery(GPIBAddress, command);
            if (response != null && response.StartsWith(DOutput + ","))
            {
                _DDelay = response;
            }
        }
    }
}

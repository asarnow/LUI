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
    public class DG535:StanfordDigitalDelayGenerator
    {
        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public const string SetDelayTimeCommand = "DT ";
        public const string TriggerInput = "0";
        public const string TOutput = "1";
        public const string AOutput = "2";
        public const string BOutput = "3";
        public const string ABOutput = "4";
        public const string COutput = "5";
        public const string DOutput = "6";
        public const string CDOutput = "7";
        public const string TName = "T";
        public const string AName = "A";
        public const string BName = "B";
        public const string ABName = "AB";
        public const string CName = "C";
        public const string DName = "D";
        public const string CDName = "CD";


        public const byte DefaultGPIBAddress = 15;

        private static Dictionary<string, string> _DelayMap = new Dictionary<string,string>
        { 
            // Forward lookup
            { TName,  TOutput  },
            { AName,  AOutput  },
            { BName,  BOutput  },
            { ABName, ABOutput },
            { CName,  COutput  },
            { DName,  DOutput  },
            { CDName, CDOutput },
            // Reverse lookup
            { TOutput,  TName  },
            { AOutput,  AName  },
            { BOutput,  BName  },
            { ABOutput, ABName },
            { COutput,  CName  },
            { DOutput,  DName  },
            { CDOutput, CDName }
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
                return new string[] { AName, BName, CName, DName };
            }
        }

        public override string[] DelayPairs
        {
            get
            {
                return new string[] { ABName, CDName };
            }
        }

        public override string[] Triggers
        {
            get
            {
                return new string[] { TName, AName, BName, CName };
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

        public DG535(byte GPIBAddress, params ILuiObject[] dependencies) : base(GPIBAddress, dependencies)
        {
            ReadAllDelays();
        }

        /// <summary>
        /// Sets any delay.
        /// </summary>
        /// <param name="DelayName"></param>
        /// <param name="TriggerName"></param>
        /// <param name="Delay"></param>
        public override void SetDelay(string DelayName, string TriggerName, double Delay)
        {
            string DelayOutput = DelayMap[DelayName];
            string TriggerOutput = DelayMap[TriggerName];
            SetNamedDelay(DelayOutput, TriggerOutput, Delay);
        }

        /// <summary>
        /// Sets paired delays for pulse generation.
        /// </summary>
        /// <param name="DelayPair"></param>
        /// <param name="TriggerName"></param>
        /// <param name="Delay"></param>
        /// <param name="Width"></param>
        public override void SetDelayPulse(Tuple<string, string> DelayPair, string TriggerName, double Delay, double Width)
        {
            string DelayOutput1 = DelayMap[DelayPair.Item1];
            string DelayOutput2 = DelayMap[DelayPair.Item2];
            string TriggerOutput = DelayMap[TriggerName];
            SetNamedDelay(DelayOutput1, TriggerOutput, Delay);
            SetNamedDelay(DelayOutput2, DelayOutput1, Width);
        }

        /// <summary>
        /// Internal use only - calls function to update property and write to device.
        /// </summary>
        /// <param name="DelayOutput"></param>
        /// <param name="TriggerOutput"></param>
        /// <param name="Delay"></param>
        private void SetNamedDelay(string DelayOutput, string TriggerOutput, double Delay)
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
        public void SetADelay(double delay, string relative = TOutput)
        {
            ADelay = relative + "," + delay.ToString();
        }

        public void SetBDelay(double delay, string relative = TOutput)
        {
            BDelay = relative + "," + delay.ToString();
        }

        public void SetCDelay(double delay, string relative = TOutput)
        {
            CDelay = relative + "," + delay.ToString();
        }

        public void SetDDelay(double delay, string relative = TOutput)
        {
            DDelay = relative + "," + delay.ToString();
        }

        public override string GetDelay(string DelayName)
        {
            string DelayOutput = DelayMap[DelayName];
            return GetNamedDelay(DelayOutput);
        }

        public override string GetDelayTrigger(string DelayName)
        {
            return _DelayMap[GetDelay(DelayName).Split(',')[0]];
        }

        public override double GetDelayValue(string DelayName)
        {
            return Double.Parse(GetDelay(DelayName).Split(',')[1]);
        }

        /// <summary>
        /// Internal use only - calls function to read from device.
        /// </summary>
        /// <param name="DelayOutput"></param>
        /// <param name="TriggerOutput"></param>
        /// <param name="Delay"></param>
        private string GetNamedDelay(string DelayOutput)
        {
            switch (DelayOutput)
            {
                case AOutput:
                    return ADelay;
                case BOutput:
                    return BDelay;
                case COutput:
                    return CDelay;
                case DOutput:
                    return DDelay;
                default:
                    throw new ArgumentException("Illegal delay output given.");
            }
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
            _ADelay = response;
        }

        private void ReadBDelay()
        {
            string command = SetDelayTimeCommand + BOutput;
            // e.g. "1,+0.001000000000"
            string response = GPIBProvider.LoggedQuery(GPIBAddress, command);
            _BDelay = response;
        }

        private void ReadCDelay()
        {
            string command = SetDelayTimeCommand + COutput;
            // e.g. "1,+0.001000000000"
            string response = GPIBProvider.LoggedQuery(GPIBAddress, command);
            _CDelay = response;
        }

        private void ReadDDelay()
        {
            string command = SetDelayTimeCommand + DOutput;
            // e.g. "1,+0.001000000000"
            string response = GPIBProvider.LoggedQuery(GPIBAddress, command);
            _DDelay = response;
        }

    }
}

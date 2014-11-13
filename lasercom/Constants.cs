//  <summary>
//      Constants used in LUI.
//  </summary>    

using System;

namespace LUI
{
    public static class Constants
    {
        // Library constants
        public const Double GroundState = double.PositiveInfinity;
        public const Double DarkState = double.NegativeInfinity;

        // Default option values
        public const float DefaultTemperatureF = 20F;
        public const int DefaultTemperature = 20;
        public const float TemperatureEps = 3F;

        // BeamFlags constants and commands
        public const int DefaultBeamFlagsCommandDelay = 150;

        // Andor constants and commands
        public const int ReadModeFVB = 0;
        public const int ReadModeMultiTrack = 1;
        public const int ReadModeRandomTrack = 2;
        public const int ReadModeSingleTrack = 3;
        public const int ReadModeImage = 4;

        public const int AcquisitionModeSingle = 1;
        public const int AcquisitionModeAccumulate = 2;

        public const int GatingModeSMBOnly = 2;

        public const int DDGTriggerModeInternal = 0;
        public const int DDGTriggerModeExternal = 1;

        public const int TriggerModeExternal = 1;
        public const int TriggerModeExternalExposure = 7;

        public const int TriggerInvertRising = 0;
        public const int TriggerInvertFalling = 1;
        public const float DefaultTriggerLevel = 3.9F;

        public const int MCPGatingOff = 0;
        public const int MCPGatingOn = 1;
        public const int DefaultMCPGain = 500;

        // NI constants and GPIB commands
        public const int BoardNumber = 0;

        // Stanford DDG535
        public static class DDG535
        {
            public const string SetDelayTimeCommand = "DT ";
            public const string TriggerInput = "0";
            public const string T0Output = "1";
            public const string AOutput = "2";
            public const string BOutput = "3";
            public const string ABOutput = "4";
            public const string COutput = "5";
            public const string DOutput = "6";
            public const string CDOutput = "7";
        }
    }
}

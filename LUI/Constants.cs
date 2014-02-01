//  <summary>
//      Constants used in LUI.
//  </summary>    

namespace LUI
{
    public static class Constants
    {
        public const float DefaultTemperatureF = 20F;
        public const int DefaultTemperature = 20;
        public const float TemperatureEps = 0.1F;

        public const string OpenFlashCommand = "!0SO1";
        public const string CloseFlashCommand = "!0SO000";

        public const string OpenLaserCommand = "!0SO2";
        public const string CloseLaserCommand = "!0SO000";

        public const string OpenLaserAndFlashCommand = "!0S03";
        public const string CloseLaserAndFlashCommand = "!SO000";

        public const int ReadModeFVB = 0;

        public const int AcqModeSingle = 1;
        public const int AcqModeAccumulate = 2;

        public const int TrigModeExternal = 1;
        public const int TrigModeExternalExposure = 7;
    }
}

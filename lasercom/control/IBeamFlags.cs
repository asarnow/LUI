
namespace lasercom.control
{
    public enum BeamFlagState { Open, Closed }
    /// <summary>
    /// Defines the public operations supported by beam flags.
    /// </summary>
    public interface IBeamFlags
    {
        BeamFlagState FlashState { get; }
        BeamFlagState LaserState { get; }
        int Delay { get; set; } // Time in miliseconds to sleep between commands.

        BeamFlagState ToggleLaser();
        BeamFlagState ToggleFlash();
        void ToggleLaserAndFlash();

        void OpenLaser();
        void CloseLaser();

        void OpenFlash();
        void CloseFlash();

        void OpenLaserAndFlash();
        void CloseLaserAndFlash();
    }
}

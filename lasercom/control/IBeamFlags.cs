
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

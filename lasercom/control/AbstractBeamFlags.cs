using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using lasercom;
using lasercom.objects;

namespace lasercom.control
{
    /// <summary>
    /// Base class for all beam flag classes.
    /// </summary>
    public abstract class AbstractBeamFlags: LuiObject, IBeamFlags
    {
        public BeamFlagState FlashState { get; set; }
        public BeamFlagState LaserState { get; set; }
        public int Delay { get; set; } // Time in miliseconds to sleep between commands.

        virtual public BeamFlagState ToggleLaser()
        {
            switch (LaserState)
            {
                case BeamFlagState.Closed:
                    OpenLaser();
                    break;
                case BeamFlagState.Open:
                    CloseLaser();
                    break;
            }
            return LaserState;
        }

        virtual public BeamFlagState ToggleFlash()
        {
            switch (FlashState)
            {
                case BeamFlagState.Closed:
                    OpenFlash();
                    break;
                case BeamFlagState.Open:
                    CloseFlash();
                    break;
            }
            return FlashState;
        }

        virtual public void OpenLaser()
        {
            LaserState = BeamFlagState.Open;
        }

        virtual public void CloseLaser()
        {
            LaserState = BeamFlagState.Closed;
        }

        virtual public void OpenFlash()
        {
            FlashState = BeamFlagState.Open;
        }

        virtual public void CloseFlash()
        {
            FlashState = BeamFlagState.Closed;
        }

        virtual public void ToggleLaserAndFlash()
        {
            ToggleFlash();
            ToggleLaser();
        }

        virtual public void OpenLaserAndFlash()
        {
            LaserState = BeamFlagState.Open;
            FlashState = BeamFlagState.Open;
        }

        virtual public void CloseLaserAndFlash()
        {
            LaserState = BeamFlagState.Closed;
            FlashState = BeamFlagState.Closed;
        }

    }
}


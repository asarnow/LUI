using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LUI;

namespace LUI
{
    public abstract class AbstractBeamFlags
    {        public enum State { Open, Closed }

        /*        public struct State
                {
                    public readonly string StateCommand;
                    public readonly StateName Name;
                }*/

        public State FlashState;
        public State LaserState;

        public State ToggleLaser()
        {
            switch (LaserState)
            {
                case State.Closed:
                    OpenLaser();
                    break;
                case State.Open:
                    CloseLaser();
                    break;
            }
            return LaserState;
        }

        public void OpenLaser()
        {
            LaserState = State.Open;
        }

        public void CloseLaser()
        {
            LaserState = State.Closed;
        }

        public State GetLaserState()
        {
            return LaserState;
        }

        public State ToggleFlash()
        {
            switch (FlashState)
            {
                case State.Closed:
                    OpenFlash();
                    break;
                case State.Open:
                    CloseFlash();
                    break;
            }
            return FlashState;
        }

        public void OpenFlash()
        {
            FlashState = State.Open;
        }

        public void CloseFlash()
        {
            FlashState = State.Closed;
        }

        public State GetFlashState()
        {
            return FlashState;
        }

        public void ToggleLaserAndFlash()
        {
            ToggleFlash();
            ToggleLaser();
        }

        public void OpenLaserAndFlash()
        {
            LaserState = State.Open;
            FlashState = State.Open;
        }

        public void CloseLaserAndFlash()
        {
            LaserState = State.Closed;
            FlashState = State.Closed;
        }
    }
}


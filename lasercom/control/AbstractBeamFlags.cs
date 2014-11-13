using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LUI;

namespace LUI
{
    public abstract class AbstractBeamFlags
    {        
        public enum State { Open, Closed }
        public State FlashState;
        public State LaserState;
        public int Delay { get; set; } // Time in miliseconds to sleep between commands.

        virtual public State ToggleLaser()
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

        virtual public void OpenLaser()
        {
            LaserState = State.Open;
        }

        virtual public void CloseLaser()
        {
            LaserState = State.Closed;
        }

        virtual public State GetLaserState()
        {
            return LaserState;
        }

        virtual public State ToggleFlash()
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

        virtual public void OpenFlash()
        {
            FlashState = State.Open;
        }

        virtual public void CloseFlash()
        {
            FlashState = State.Closed;
        }

        virtual public State GetFlashState()
        {
            return FlashState;
        }

        virtual public void ToggleLaserAndFlash()
        {
            ToggleFlash();
            ToggleLaser();
        }

        virtual public void OpenLaserAndFlash()
        {
            LaserState = State.Open;
            FlashState = State.Open;
        }

        virtual public void CloseLaserAndFlash()
        {
            LaserState = State.Closed;
            FlashState = State.Closed;
        }
    }
}


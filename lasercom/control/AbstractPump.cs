using lasercom.objects;

namespace lasercom.control
{
    /// <summary>
    /// Base class for all pumps.
    /// </summary>
    public abstract class AbstractPump : LuiObject, IPump
    {
        private PumpState _CurrentState;
        public PumpState CurrentState
        {
            get
            {
                return _CurrentState;
            }
            protected set
            {
                _CurrentState = value;
            }
        }

        public virtual PumpState Toggle()
        {
            switch (CurrentState)
            {
                case PumpState.Open:
                    SetClosed();
                    break;
                case PumpState.Closed:
                    SetOpen();
                    break;
            }
            return CurrentState;
        }

        public virtual void SetOpen()
        {
            CurrentState = PumpState.Open;
            //TODO Which is which?
        }

        public virtual void SetClosed()
        {
            CurrentState = PumpState.Closed;
        }

        public virtual PumpState GetState()
        {
            return CurrentState;
        }

    }
}

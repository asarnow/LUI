using lasercom.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lasercom.control
{
    /// <summary>
    /// Base class for all pumps.
    /// </summary>
    public abstract class AbstractPump : LuiObject, IPump
    {
        public enum State { Open, Closed }
        public State CurrentState;

        public virtual State Toggle()
        {
            switch (CurrentState)
            {
                case State.Open:
                    SetClosed();
                    break;
                case State.Closed:
                    SetOpen();
                    break;
            }
            return CurrentState;
        }

        public virtual void SetOpen()
        {
            CurrentState = State.Open;
            //TODO Which is which?
        }

        public virtual void SetClosed()
        {
            CurrentState = State.Closed;
        }

        public virtual State GetState()
        {
            return CurrentState;
        }

    }
}

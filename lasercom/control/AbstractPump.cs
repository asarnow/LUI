using lasercom.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//  <summary>
//      AbstractPump is an abstract class for concrete Pump objects to inherit.
//  </summary>
namespace lasercom.control
{
    public abstract class AbstractPump : LuiObject
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//  <summary>
//      AbstractPump is an abstract class for concrete Pump objects to inherit.
//  </summary>
namespace lasercom.control
{
    public abstract class AbstractPump
    {
        public enum State { Open, Closed }
        public State CurrentState;

        public State Toggle()
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

        public void SetOpen()
        {
            CurrentState = State.Open;
            //TODO Which is which?
        }

        public void SetClosed()
        {
            CurrentState = State.Closed;
        }

        public State GetState()
        {
            return CurrentState;
        }
    }
}

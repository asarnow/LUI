using System;
using System.IO.Ports;

//  <summary>
//      Represents the syringe pump.
//  </summary>

namespace lasercom.control
{
    class HarvardPump:AbstractPump
    {
        private readonly SerialPort _port;

        public HarvardPump(String portName)
        {
            // DtrEnable causes DTR pin to go high on port open
            _port = new SerialPort(portName) { DtrEnable = true };            
            SetClosed();
        }

        public String GetPortName()
        {
            return _port.PortName;
        }

        new public State Toggle()
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

        new public void SetOpen()
        {
            CurrentState = State.Open;
            _port.Open(); //TODO Which is which?
        }

        new public void SetClosed()
        {
            CurrentState = State.Closed;
            _port.Close();
        }

        new public State GetState()
        {
            return CurrentState;
        }
    }
}

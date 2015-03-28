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

        public override State Toggle()
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

        public override void SetOpen()
        {
            CurrentState = State.Open;
            _port.Open(); //TODO Which is which?
        }

        public override void SetClosed()
        {
            CurrentState = State.Closed;
            _port.Close();
        }

        public override State GetState()
        {
            return CurrentState;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_port.IsOpen) _port.Close();
                _port.Dispose();
            }
        }
    }
}

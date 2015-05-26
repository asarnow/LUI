using System;
using System.IO.Ports;

namespace lasercom.control
{
    /// <summary>
    /// Represents a Harvard Apparatus syringe pump using the custom
    /// flip-flop box and foot-pedal hack.
    /// </summary>
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

        public override PumpState Toggle()
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

        public override void SetOpen()
        {
            CurrentState = PumpState.Open;
            _port.Open(); //TODO Which is which?
        }

        public override void SetClosed()
        {
            CurrentState = PumpState.Closed;
            _port.Close();
        }

        public override PumpState GetState()
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

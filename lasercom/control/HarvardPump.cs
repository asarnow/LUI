using System;
using System.IO.Ports;
using lasercom.objects;

namespace lasercom.control
{
    /// <summary>
    /// Represents a Harvard Apparatus syringe pump using the custom
    /// flip-flop box and foot-pedal hack.
    /// </summary>
    public class HarvardPump:AbstractPump
    {
        private SerialPort _port;

        public HarvardPump(LuiObjectParameters p) : 
            this(p as PumpParameters) { }

        public HarvardPump(PumpParameters p)
        {
            if (p == null || p.PortName == null)
                throw new ArgumentException("PortName must be defined.");
            Init(p.PortName);
        }

        public HarvardPump(String portName)
        {
            Init(portName);
        }

        private void Init(string portName)
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

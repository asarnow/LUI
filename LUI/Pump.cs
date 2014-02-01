using System;
using System.IO.Ports;

//  <summary>
//      Represents the syringe pump.
//  </summary>

namespace LUI
{
    class Pump
    {

        public enum State{Open, Closed}

        public State CurrentState;
        private readonly SerialPort _port;

        public Pump(String portName)
        {
            // RtsEnable causes RTS pin to go high on port open
            _port = new SerialPort(portName) {RtsEnable = true};            
            SetClosed();
        }

        public String GetPortName()
        {
            return _port.PortName;
        }

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
            _port.Open(); //TODO Which is which?
        }

        public void SetClosed()
        {
            CurrentState = State.Closed;
            _port.Close();
        }

        public State GetState()
        {
            return CurrentState;
        }
    }
}

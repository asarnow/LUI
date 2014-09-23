using System;
using System.IO.Ports;

//  <summary>
//      Represents the beam flag microcontroller.
//      If different microcontrollers are used, an interface and abstract
//      class should be added for the beam flags classes to inherit from.
//  </summary>

namespace LUI
{
    public class BeamFlags:AbstractBeamFlags
    {

        public enum State { Open, Closed }

        /*        public struct State
                {
                    public readonly string StateCommand;
                    public readonly StateName Name;
                }*/

        public State FlashState;
        public State LaserState;
        public readonly string OpenFlashCommand;
        public readonly string CloseFlashCommand;
        public readonly string OpenLaserCommand;
        public readonly string CloseLaserCommand;
        public readonly string OpenLaserAndFlashCommand;
        public readonly string CloseLaserAndFlashCommand;
        private readonly SerialPort _port;

        public BeamFlags(string openFlashCommand,
                         string closeFlashCommand,
                         string openLaserCommand,
                         string closeLaserCommand,
                         string openLaserAndFlashCommand,
                         string closeLaserAndFlashCommand,
                         String portName)
        {
            OpenFlashCommand = openFlashCommand;
            CloseFlashCommand = closeFlashCommand;
            OpenLaserCommand = openLaserCommand;
            CloseLaserCommand = closeLaserCommand;
            OpenLaserAndFlashCommand = openLaserAndFlashCommand;
            CloseLaserAndFlashCommand = closeLaserAndFlashCommand;
            _port = new SerialPort(portName);
            CloseLaserAndFlash();
        }

        public BeamFlags(String portName)
        {
            OpenFlashCommand = Constants.OpenFlashCommand;
            CloseFlashCommand = Constants.CloseFlashCommand;
            OpenLaserCommand = Constants.OpenLaserCommand;
            CloseLaserCommand = Constants.CloseLaserCommand;
            OpenLaserAndFlashCommand = Constants.OpenLaserAndFlashCommand;
            CloseLaserAndFlashCommand = Constants.CloseLaserAndFlashCommand;
            _port = new SerialPort(portName);
            CloseLaserAndFlash();
        }

        public String GetPortName()
        {
            return _port.PortName;
        }

        public State ToggleLaser()
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

        public void OpenLaser()
        {
            _port.Open();
            _port.Write(OpenLaserCommand);
            _port.Close();
            LaserState = State.Open;
        }

        public void CloseLaser()
        {
            _port.Open();
            _port.Write(CloseLaserCommand);
            _port.Close();
            LaserState = State.Closed;
        }

        public State GetLaserState()
        {
            return LaserState;
        }

        public State ToggleFlash()
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

        public void OpenFlash()
        {
            _port.Open();
            _port.Write(OpenFlashCommand);
            _port.Close();
            FlashState = State.Open;
        }

        public void CloseFlash()
        {
            _port.Open();
            _port.Write(CloseFlashCommand);
            _port.Close();
            FlashState = State.Closed;
        }

        public State GetFlashState()
        {
            return FlashState;
        }

        public void ToggleLaserAndFlash()
        {
            ToggleFlash();
            ToggleLaser();
        }

        public void OpenLaserAndFlash()
        {
            _port.Open();
            _port.Write(OpenLaserAndFlashCommand);
            _port.Close();
            LaserState = State.Open;
            FlashState = State.Open;
        }

        public void CloseLaserAndFlash()
        {
            _port.Open();
            _port.Write(CloseLaserAndFlashCommand);
            _port.Close();
            LaserState = State.Closed;
            FlashState = State.Closed;
        }
    }
}

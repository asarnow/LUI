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

        public override State ToggleLaser()
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

        public override void OpenLaser()
        {
            _port.Open();
            _port.Write(OpenLaserCommand);
            _port.Close();
            LaserState = State.Open;
        }

        public override void CloseLaser()
        {
            _port.Open();
            _port.Write(CloseLaserAndFlashCommand);
            _port.Close();
            LaserState = State.Closed;
            if (FlashState == State.Open) OpenFlash();
        }

        public override State GetLaserState()
        {
            return LaserState;
        }

        public override State ToggleFlash()
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

        public override void OpenFlash()
        {
            _port.Open();
            _port.Write(OpenFlashCommand);
            _port.Close();
            FlashState = State.Open;
        }

        public override void CloseFlash()
        {
            _port.Open();
            _port.Write(CloseLaserAndFlashCommand);
            _port.Close();
            FlashState = State.Closed;
            if (LaserState == State.Open) OpenLaser();
        }

        public override State GetFlashState()
        {
            return FlashState;
        }

        public override void ToggleLaserAndFlash()
        {
            ToggleFlash();
            ToggleLaser();
        }

        public override void OpenLaserAndFlash()
        {
            _port.Open();
            _port.Write(OpenLaserAndFlashCommand);
            _port.Close();
            LaserState = State.Open;
            FlashState = State.Open;
        }

        public override void CloseLaserAndFlash()
        {
            _port.Open();
            _port.Write(CloseLaserAndFlashCommand);
            _port.Close();
            LaserState = State.Closed;
            FlashState = State.Closed;
        }
    }
}

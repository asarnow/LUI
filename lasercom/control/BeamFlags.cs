using System;
using System.IO.Ports;

//  <summary>
//      Represents the beam flag microcontroller.
//      If different microcontrollers are used, an interface and abstract
//      class should be added for the beam flags classes to inherit from.
//  </summary>

namespace lasercom.control
{
    public class BeamFlags:AbstractBeamFlags
    {
        public const string OpenFlashCommand = "!0SO1";
        public const string CloseFlashCommand = "!0SO000";

        public const string OpenLaserCommand = "!0SO2";
        public const string CloseLaserCommand = "!0SO000";

        public const string OpenLaserAndFlashCommand = "!0SO3";
        public const string CloseLaserAndFlashCommand = "!0SO000";

        private readonly SerialPort _port;

        public BeamFlags(String portName)
        {
            Delay = Constants.DefaultBeamFlagsCommandDelay;
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

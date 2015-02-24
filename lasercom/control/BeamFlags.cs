using System;
using System.IO.Ports;

//  <summary>
//      Represents the beam flag microcontroller (232 SDA12).
//      If different microcontrollers are used, an interface and abstract
//      class should be added for the beam flags classes to inherit from.
//  </summary>

namespace lasercom.control
{
    /// <summary>
    /// Class representing BeamFlags operated by 232 SDA12 microcontroller.
    /// Note that due to limitations of the microcontroller, the flags can
    /// NOT BE CLOSED SIMULTANEOUSLY. Instead, that behavior is emulated.
    /// </summary>
    public class BeamFlags:AbstractBeamFlags
    {
        public const string OpenFlashCommand = "!0SO1";
        public const string CloseFlashCommand = "!0SO000";

        public const string OpenLaserCommand = "!0SO2";
        public const string CloseLaserCommand = "!0SO000";

        public const string OpenLaserAndFlashCommand = "!0SO3";
        public const string CloseLaserAndFlashCommand = "!0SO000";

        // Approximate time in ms for solenoid to switch.
        public const int DefaultDelay = 150;

        private readonly SerialPort _port;

        public BeamFlags(String portName)
        {
            Delay = DefaultDelay;
            _port = new SerialPort(portName);
            CloseLaserAndFlash();
        }

        public String GetPortName()
        {
            return _port.PortName;
        }

        public override void OpenLaser()
        {
            OpenLaser(true);
        }

        public void OpenLaser(bool wait)
        {
            _port.Open();
            _port.Write(OpenLaserCommand);
            _port.Close();
            if (wait) System.Threading.Thread.Sleep(Delay);
            LaserState = State.Open;
        }

        public override void OpenFlash()
        {
            OpenFlash(true);
        }

        public void OpenFlash(bool wait)
        {
            _port.Open();
            _port.Write(OpenFlashCommand);
            _port.Close();
            if (wait) System.Threading.Thread.Sleep(Delay);
            FlashState = State.Open;
        }

        public override void OpenLaserAndFlash()
        {
            OpenLaserAndFlash(true);
        }

        public void OpenLaserAndFlash(bool wait)
        {
            _port.Open();
            _port.Write(OpenLaserAndFlashCommand);
            _port.Close();
            if (wait) System.Threading.Thread.Sleep(Delay);
            LaserState = State.Open;
            FlashState = State.Open;
        }

        public override void CloseLaser()
        {
            _port.Open();
            _port.Write(CloseLaserAndFlashCommand);
            _port.Close();
            LaserState = State.Closed;
            if (FlashState == State.Open) OpenFlash(false);
            //throw new NotImplementedException("Can't close flags independently");
        }

        public override void CloseFlash()
        {
            _port.Open();
            _port.Write(CloseLaserAndFlashCommand);
            _port.Close();
            FlashState = State.Closed;
            if (LaserState == State.Open) OpenLaser(false);
            //throw new NotImplementedException("Can't close flags independently");
        }

        public override void CloseLaserAndFlash()
        {
            _port.Open();
            _port.Write(CloseLaserAndFlashCommand);
            _port.Close();
            LaserState = State.Closed;
            FlashState = State.Closed;
        }

        public override void EnsurePortDisposed()
        {
            _port.Close();
            _port.Dispose();
        }
    }
}

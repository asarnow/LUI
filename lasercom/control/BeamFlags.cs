using System;
using System.IO.Ports;
using lasercom.objects;

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

        public int Delay { get; set; } // Time in miliseconds to sleep between commands.

        private SerialPort _port;

        public BeamFlags(LuiObjectParameters p) : this(p as BeamFlagsParameters) { }

        public BeamFlags(BeamFlagsParameters p)
        {
            if (p == null || p.PortName == null)
                throw new ArgumentException("PortName must be defined.");
            Init(p.PortName);
        }

        public BeamFlags(String portName)
        {
            Init(portName);
        }

        private void Init(string portName)
        {
            Delay = DefaultDelay;
            _port = new SerialPort(portName);
            CloseLaserAndFlash();
        }

        public string PortName
        {
            get
            {
                return _port.PortName;
            }
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
            LaserState = BeamFlagState.Open;
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
            FlashState = BeamFlagState.Open;
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
            LaserState = BeamFlagState.Open;
            FlashState = BeamFlagState.Open;
        }

        public override void CloseLaser()
        {
            _port.Open();
            _port.Write(CloseLaserAndFlashCommand);
            _port.Close();
            LaserState = BeamFlagState.Closed;
            if (FlashState == BeamFlagState.Open) OpenFlash(false);
            //throw new NotImplementedException("Can't close flags independently");
        }

        public override void CloseFlash()
        {
            _port.Open();
            _port.Write(CloseLaserAndFlashCommand);
            _port.Close();
            FlashState = BeamFlagState.Closed;
            if (LaserState == BeamFlagState.Open) OpenLaser(false);
            //throw new NotImplementedException("Can't close flags independently");
        }

        public override void CloseLaserAndFlash()
        {
            _port.Open();
            _port.Write(CloseLaserAndFlashCommand);
            _port.Close();
            LaserState = BeamFlagState.Closed;
            FlashState = BeamFlagState.Closed;
        }

        private void EnsurePortDisposed()
        {
            if (_port != null)
            {
                if (_port.IsOpen) _port.Close();
                _port.Dispose();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                CloseLaserAndFlash();
                EnsurePortDisposed();
            }
        }

        public override void Update(BeamFlagsParameters p)
        {
            Delay = p.Delay;
        }
    }
}

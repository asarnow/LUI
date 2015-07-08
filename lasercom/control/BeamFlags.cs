using lasercom.objects;
using System;
using System.IO.Ports;
using System.Threading;

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
        public const int DefaultDelay = 300;

        public int Delay { get; set; } // Time in miliseconds to sleep between commands.

        public string PortName
        {
            get
            {
                return _port.PortName;
            }
        }

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
            _port.Open();
            CloseLaserAndFlash();
        }

        public override void OpenLaser()
        {
            OpenLaser(true);
        }

        /// <summary>
        /// Opens the laser flag, optionally sleeping to ensure the flag has opened completely.
        /// LaserState is updated only after sleeping in case of monitoring by another thread.
        /// </summary>
        /// <param name="wait"></param>
        private void OpenLaser(bool wait)
        {
            _port.Write(OpenLaserCommand);
            if (wait) Thread.Sleep(Delay);
            LaserState = BeamFlagState.Open;
        }

        public override void OpenFlash()
        {
            OpenFlash(true);
        }

        private void OpenFlash(bool wait)
        {
            _port.Write(OpenFlashCommand);
            if (wait) Thread.Sleep(Delay);
            FlashState = BeamFlagState.Open;
        }

        public override void OpenLaserAndFlash()
        {
            OpenLaserAndFlash(true);
        }

        private void OpenLaserAndFlash(bool wait)
        {
            _port.Write(OpenLaserAndFlashCommand);
            if (wait) Thread.Sleep(Delay);
            LaserState = BeamFlagState.Open;
            FlashState = BeamFlagState.Open;
        }

        public override void CloseLaser()
        {
            CloseLaser(true);
        }

        /// <summary>
        /// Emulating closing the laser flag independently by closing both flags
        /// and then re-opening the flash flag if necessary.
        /// Using the sleep in OpenFlash() allows both State values to be updated
        /// after waking (ensuring the flags have reached their new positions).
        /// </summary>
        /// <param name="wait"></param>
        private void CloseLaser(bool wait)
        {
            _port.Write(CloseLaserAndFlashCommand);
            if (FlashState == BeamFlagState.Open) OpenFlash(wait); // Sleep in OpenFlash if requested.
            else if (wait) Thread.Sleep(Delay); // Didn't enter OpenFlash, need to sleep.
            LaserState = BeamFlagState.Closed;
            //throw new NotImplementedException("Can't close flags independently");
        }

        public override void CloseFlash()
        {
            CloseFlash(true);
        }

        private void CloseFlash(bool wait)
        {
            _port.Write(CloseLaserAndFlashCommand);
            if (LaserState == BeamFlagState.Open) OpenLaser(wait);
            else if (wait) Thread.Sleep(Delay);
            FlashState = BeamFlagState.Closed;
            //throw new NotImplementedException("Can't close flags independently");
        }

        public override void CloseLaserAndFlash()
        {
            CloseLaserAndFlash(true);
        }

        private void CloseLaserAndFlash(bool wait)
        {
            _port.Write(CloseLaserAndFlashCommand);
            if (wait) Thread.Sleep(Delay);
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

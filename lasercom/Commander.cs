using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if x64
using ATMCD64CS;
#else
using ATMCD32CS;
#endif

using log4net;
using NationalInstruments.NI4882;
using lasercom.camera;
using lasercom.control;
using lasercom.ddg;
using lasercom.io;

namespace lasercom
{

    public class Commander
    {
        private ICamera _Camera;
        public ICamera Camera
        {
            get
            {
                return _Camera;
            }
            set
            {
                _Camera = value;
            }
        }
        public AbstractBeamFlags BeamFlag { get; set; }
        public IDigitalDelayGenerator DDG { get; set; }
        public AbstractPump Pump { get; set; }
        public List<Double> Delays { get; set; }

        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Commander(ICamera camera = null, AbstractBeamFlags beamFlags = null, IDigitalDelayGenerator ddg = null, AbstractPump pump = null)
        {
            // Set dummies instead of null values to save a *ton* of null checks elsewhere.
            Camera = camera != null ? camera : new DummyCamera();
            BeamFlag = beamFlags != null ? beamFlags : new DummyBeamFlags();
            DDG = ddg != null ? ddg : new DummyDigitalDelayGenerator();
            Pump = pump; //TODO Change to dummy pump.
        }

        public void SetDelays(string file)
        {
            // read file
            Delays = new List<double>();
        }

        public int[] Collect(int n)
        {
            for (int i = 0; i < n; i++)
            {
                Camera.CountsFvb();
            }
            return null;
        }

        public int[] Dark()
        {
            BeamFlag.CloseLaserAndFlash();
            return Camera.Acquire();
        }

        public uint Dark(int[] DataBuffer)
        {
            BeamFlag.CloseLaserAndFlash();
            return Camera.Acquire(DataBuffer);
        }

        public int[] Flash()
        {
            BeamFlag.CloseLaserAndFlash();
            BeamFlag.OpenFlash();
            int[] data = Camera.Acquire();
            BeamFlag.CloseLaserAndFlash();
            return data;
        }

        public uint Flash(int[] DataBuffer)
        {
            BeamFlag.CloseLaserAndFlash();
            BeamFlag.OpenFlash();
            uint ret = Camera.Acquire(DataBuffer);
            BeamFlag.CloseLaserAndFlash();
            return ret;
        }

        public int[] Trans()
        {
            BeamFlag.OpenLaserAndFlash();
            int[] data = Camera.Acquire();
            BeamFlag.CloseLaserAndFlash();
            return data;
        }

        public uint Trans(int[] DataBuffer)
        {
            BeamFlag.OpenLaserAndFlash();
            uint ret = Camera.Acquire(DataBuffer);
            BeamFlag.CloseLaserAndFlash();
            return ret;
        }

    }
}

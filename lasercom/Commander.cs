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
        public AndorCamera Camera { get; set; }
        public AbstractBeamFlags BeamFlags { get; set; }
        public IDigitalDelayGenerator DDG { get; set; }
        public AbstractPump Pump { get; set; }
        public List<Double> Delays { get; set; }

        public event EventHandler CalibrationChanged;
        private double[] _Calibration;
        public double[] Calibration
        {
            get
            {
                return _Calibration;
            }
            set
            {
                _Calibration = value;
                EventHandler handler = CalibrationChanged;
                if (handler != null)
                {
                    handler(this, EventArgs.Empty);
                }
            }
        }

        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Commander(AndorCamera camera, AbstractBeamFlags beamFlags, IDigitalDelayGenerator ddg)
        {
            Camera = camera;
            BeamFlags = beamFlags;
            DDG = ddg;
            Calibration = Array.ConvertAll(Enumerable.Range(1, (int)Camera.Width).ToArray<int>(), x => (double)x);
        }

        public Commander() : this(new DummyAndorCamera(), new DummyBeamFlags(), new DummyDigitalDelayGenerator())
        {
            //Camera = new DummyAndorCamera();
            //BeamFlags = new BeamFlags("COM1");
            //BeamFlags = new DummyBeamFlags();
            //int address = 0;
            //DDG = new DDG535(address);
            //DDG = new DummyDigitalDelayGenerator();
            //Pump = new HarvardPump("COM3");
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
            BeamFlags.CloseLaserAndFlash();
            return Camera.Acquire();
        }

        public uint Dark(int[] DataBuffer)
        {
            BeamFlags.CloseLaserAndFlash();
            return Camera.Acquire(DataBuffer);
        }

        public int[] Flash()
        {
            BeamFlags.CloseLaserAndFlash();
            BeamFlags.OpenFlash();
            int[] data = Camera.Acquire();
            System.Threading.Thread.Sleep(BeamFlags.Delay);
            BeamFlags.CloseLaserAndFlash();
            return data;
        }

        public uint Flash(int[] DataBuffer)
        {
            BeamFlags.CloseLaserAndFlash();
            BeamFlags.OpenFlash();
            uint ret = Camera.Acquire(DataBuffer);
            System.Threading.Thread.Sleep(BeamFlags.Delay);
            BeamFlags.CloseLaserAndFlash();
            return ret;
        }

        public int[] Trans()
        {
            BeamFlags.OpenLaserAndFlash();
            int[] data = Camera.Acquire();
            System.Threading.Thread.Sleep(BeamFlags.Delay);
            BeamFlags.CloseLaserAndFlash();
            return data;
        }

        public uint Trans(int[] DataBuffer)
        {
            BeamFlags.OpenLaserAndFlash();
            uint ret = Camera.Acquire(DataBuffer);
            System.Threading.Thread.Sleep(BeamFlags.Delay);
            BeamFlags.CloseLaserAndFlash();
            return ret;
        }

    }
}

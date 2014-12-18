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
        public int[] Calibration { get; set; }

        private static readonly ILog Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Commander(AndorCamera camera, AbstractBeamFlags beamFlags, IDigitalDelayGenerator ddg)
        {
            Camera = camera;
            BeamFlags = beamFlags;
            DDG = ddg;
        }

        public Commander()
        {
            Camera = new DummyAndorCamera();
            //BeamFlags = new BeamFlags("COM1");
            BeamFlags = new DummyBeamFlags();
            //int address = 0;
            //DDG = new DDG535(address);
            DDG = new DummyDigitalDelayGenerator();
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

        public int[] Flash()
        {
            BeamFlags.CloseLaserAndFlash();
            BeamFlags.OpenFlash();
            int[] data = Camera.Acquire();
            System.Threading.Thread.Sleep(BeamFlags.Delay);
            BeamFlags.CloseLaserAndFlash();
            return data;
        }

        public int[] Trans()
        {
            BeamFlags.OpenLaserAndFlash();
            int[] data = Camera.Acquire();
            System.Threading.Thread.Sleep(BeamFlags.Delay);
            BeamFlags.CloseLaserAndFlash();
            return data;
        }

    }
}

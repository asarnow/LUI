using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if x64
using ATMCD64CS;
#else
using ATMCD32CS;
#endif

using LUI;

namespace lasercom
{

    class Commander
    {
        private ICamera Camera { public get; private set; }
        private BeamFlags BeamFlags { public get; private set; }
        private DigitalDelayGenerator DDG { public get; private set; }
        private Pump Pump { public get; private set; }
        private List<Double> Delays { public get; public set; }
        private int[] Calibration { public get; public set; }

        public Commander()
        {
            Camera = new CameraTempControlled(".");
            BeamFlags = new BeamFlags("COM1");
            int address = 0;
            DDG = new DigitalDelayGenerator(address, new Board());

        }

        public void setDelays(string file)
        {
            // read file
            Delays = new List<double>();
        }

        public int[] collect(int n)
        {
            for (int i = 0; i < n; i++)
            {
                Camera.GetCountsFvb();
            }
            return null;
        }

    }
}

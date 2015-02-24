using lasercom;
using lasercom.camera;
using lasercom.control;
using lasercom.ddg;
using lasercom.io;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DetectorTester
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AndorCamera Camera = new CameraTempControlled(".");
            BeamFlags BeamFlags = new BeamFlags("COM1");
            //IDigitalDelayGenerator DDG = new DDG535(15);
            IDigitalDelayGenerator DDG = new DummyDigitalDelayGenerator();

            Commander Commander = new Commander(Camera, BeamFlags, DDG);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1(Commander));
        }
    }
}

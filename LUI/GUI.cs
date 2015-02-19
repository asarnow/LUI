using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using NDesk.Options;

using lasercom;
using LUI;
using lasercom.control;
using lasercom.ddg;

namespace LUI
{
    class GUI
    {

        [STAThread]
        static void Main(string[] args)
        {

            // Get preferences

#if DUMMY
            Commander Commander = new Commander();            
#else
            AndorCamera Camera = new CameraTempControlled(".");
            BeamFlags BeamFlags = new BeamFlags("COM3");
            //IDigitalDelayGenerator DDG = new DDG535(15);
            IDigitalDelayGenerator DDG = new DummyDigitalDelayGenerator();
            Commander Commander = new Commander(Camera, BeamFlags, DDG);
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ParentForm(Commander));
        }

    }
}

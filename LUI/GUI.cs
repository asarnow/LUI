using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using NDesk.Options;

using LUI;

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
            BeamFlags BeamFlags = new BeamFlags("COM1");
            IDigitalDelayGenerator DDG = new DDG535(15);
            Commander Commander = new Commander(Camera, BeamFlags, DDG);
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ParentForm(Commander));
        }

    }
}

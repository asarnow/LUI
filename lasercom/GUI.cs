using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Threading.Tasks;
using NDesk.Options;

#if x64
using ATMCD64CS;
#else
using ATMCD32CS;
#endif

using LUI;

namespace lasercom
{
    class GUI
    {

        [STAThread]
        static void Main(String[] args)
        {


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ControlForm());
        }

    }
}

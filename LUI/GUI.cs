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
        static void Main(String[] args)
        {

            // Get preferences

            Commander Commander = new Commander();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ControlForm(Commander));
        }

    }
}

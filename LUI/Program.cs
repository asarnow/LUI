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
using LUI.config;
using System.Xml.Serialization;

namespace LUI
{
    class Program
    {

        [STAThread]
        static void Main(string[] args)
        {
            #region Parse command line options
            string configfile = LUI.Constants.DefaultConfigFileLocation;
            bool show_help = false;
            var p = new OptionSet() {
                {"f|file", "Configuration file",
                    (string v) => configfile = v },
                {"h|help", "Show this help text and exit",
                    v => show_help = v != null }
            };

            List<string> extra;
            try
            {
                extra = p.Parse(args);
            }
            catch (OptionException e)
            {
                Console.WriteLine("Invalid arguments: " + e.Message);
                return;
            }

            if (show_help)
            {
                ShowHelp(p);
                return;
            }
            #endregion

            #region Deserialize XML and setup LuiConfig
            LuiConfig Config;
            
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(LuiConfig));
                StreamReader reader = new StreamReader(configfile);
                Config = (LuiConfig)serializer.Deserialize(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                if (ex is FileNotFoundException)
                {
                    Config = new LuiConfig();
                }
                else
                {
                    throw;
                }
            }

            Config.ApplicationParameters.ConfigFile = configfile;
            Config.ConfigureLogging();
            #endregion

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ParentForm(Config));
        }

        static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("LUI 2.00 Help");
            Console.WriteLine("=============");
            foreach (Option o in p)
            {
                Console.WriteLine(o.Prototype + "\t\t\t" + o.Description);
            }
        }
    }
}

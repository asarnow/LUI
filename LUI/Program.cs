using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Serialization;
using LUI.config;
using NDesk.Options;

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
                    v => show_help = true }
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
                using (StreamReader reader = new StreamReader(configfile))
                {
                    Config = (LuiConfig)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                if (ex is FileNotFoundException)
                {
                    Config = LuiConfig.DummyConfig();
                }
                else if (ex is InvalidOperationException)
                {
                    Config = LuiConfig.DummyConfig();
                }
                else
                {
                    throw;
                }
            }

            Config.ConfigFile = configfile;
            Config.ConfigureLogging();
            #endregion

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ParentForm(Config));
        }

        static void ShowHelp(OptionSet p)
        {
            Console.WriteLine("LUI " + Assembly.GetExecutingAssembly().GetName().Version + " Help");
            Console.WriteLine("=============");
            foreach (Option o in p)
            {
                Console.WriteLine(o.Prototype + "\t\t\t" + o.Description);
            }
        }
    }
}

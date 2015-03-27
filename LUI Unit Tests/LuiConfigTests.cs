using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LUI.config;
using lasercom.gpib;
using lasercom.camera;
using System.Xml.Serialization;
using System.IO;
using lasercom.objects;
using System.Collections.Generic;

namespace LUI_Unit_Tests
{
    [TestClass]
    public class LuiConfigTests
    {
        const string ConfigFile = "./config.xml";
        LuiConfig Config;

        [TestInitialize]
        public void SetupLuiConfig()
        {
            Config = new LuiConfig();
            Config.ConfigFile = ConfigFile;
            Config.LogFile = "./log.txt";
            Config.LogLevel = "DEBUG";

            var gpibParameters1 = new GpibProviderParameters();
            gpibParameters1.TypeName = "lasercom.gpib.NIGpibProvider";
            gpibParameters1.Name = "NI PCI Card";
            gpibParameters1.BoardNumber = 0;

            var gpibParameters2 = new GpibProviderParameters();
            gpibParameters2.TypeName = "lasercom.gpib.PrologixGpibProvider";
            gpibParameters2.Name = "USB GPIB Controller";
            gpibParameters2.PortName = "COM1";
            gpibParameters2.Timeout = 300;

            var cameraParameters = new CameraParameters();
            cameraParameters.TypeName = "lasercom.camera.CameraTempControlled";
            cameraParameters.Name = "Andor USB CCD";
            cameraParameters.Dir = "./";
            cameraParameters.Temperature = 20;

            Config.AddParameters(gpibParameters1);
            Config.AddParameters(gpibParameters2);
            Config.AddParameters(cameraParameters);
        }

        [TestMethod]
        public void TestXmlSerialization()
        {
            var serializer = new XmlSerializer(Config.GetType());
            using (var writer = new StreamWriter(Config.ConfigFile))
            {
                serializer.Serialize(writer, Config);
            }

            LuiConfig testConfig = null;
            using (var reader = new StreamReader(ConfigFile))
            {
                testConfig = (LuiConfig)serializer.Deserialize(reader);
            }
            
            Assert.AreEqual(testConfig.ConfigFile, Config.ConfigFile);
            Assert.AreEqual(testConfig.LogFile, Config.LogFile);
            Assert.AreEqual(testConfig.LogLevel, Config.LogLevel);
            foreach (KeyValuePair<Type, IList<LuiObjectParameters>> kvp in Config.ParameterLists)
            {
                for (int i = 0; i < kvp.Value.Count; i++)
                {
                    Assert.AreEqual(kvp.Value[i], testConfig.ParameterLists[kvp.Key][i]);
                }
            }
        }
    }
}

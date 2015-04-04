using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LUI.config;
using lasercom.gpib;
using lasercom.camera;
using System.Xml.Serialization;
using System.IO;
using lasercom.objects;
using System.Collections.Generic;
using lasercom.ddg;
using lasercom;

namespace LUI_Unit_Tests
{
    [TestClass]
    public class LuiConfigTests
    {
        const string ConfigFile = "./config.xml";
        LuiConfig Config;

        GpibProviderParameters gpibParameters1;
        GpibProviderParameters gpibParameters2;
        CameraParameters cameraParameters;
        DelayGeneratorParameters ddgParameters;

        [TestInitialize]
        public void SetupLuiConfig()
        {
            Config = new LuiConfig();
            Config.ConfigFile = ConfigFile;
            Config.LogFile = "./log.txt";
            Config.LogLevel = "DEBUG";

            gpibParameters1 = new GpibProviderParameters();
            gpibParameters1.TypeName = "lasercom.gpib.NIGpibProvider";
            gpibParameters1.Name = "NI PCI Card";
            gpibParameters1.BoardNumber = 0;

            gpibParameters2 = new GpibProviderParameters();
            gpibParameters2.TypeName = "lasercom.gpib.PrologixGpibProvider";
            gpibParameters2.Name = "USB GPIB Controller";
            gpibParameters2.PortName = "COM1";
            gpibParameters2.Timeout = 300;

            cameraParameters = new CameraParameters();
            cameraParameters.TypeName = "lasercom.camera.CameraTempControlled";
            cameraParameters.Name = "Andor USB CCD";
            cameraParameters.Dir = "./";
            cameraParameters.Temperature = 20;

            ddgParameters = new DelayGeneratorParameters();
            ddgParameters.TypeName = "lasercom.ddg.DDG535";
            ddgParameters.Name = "Primary DDG";
            ddgParameters.GpibAddress = 15;
            ddgParameters.GpibProvider = gpibParameters1;

            Config.AddParameters(gpibParameters1);
            Config.AddParameters(gpibParameters2);
            Config.AddParameters(cameraParameters);
            Config.AddParameters(ddgParameters);
        }

        [TestMethod]
        public void TestXmlSerialization()
        {
            //var serializer = new XmlSerializer(Config.GetType());
            //using (var writer = new StreamWriter(Config.ConfigFile))
            //{
            //    serializer.Serialize(writer, Config);
            //}
            Config.Save();

            //LuiConfig testConfig = null;
            //using (var reader = new StreamReader(ConfigFile))
            //{
            //    testConfig = (LuiConfig)serializer.Deserialize(reader);
            //}
            LuiConfig testConfig = LuiConfig.FromFile(ConfigFile);
            
            Assert.AreEqual(testConfig.ConfigFile, Config.ConfigFile);
            Assert.AreEqual(testConfig.LogFile, Config.LogFile);
            Assert.AreEqual(testConfig.LogLevel, Config.LogLevel);
            foreach (KeyValuePair<Type, Dictionary<LuiObjectParameters, ILuiObject>> kvp in Config.LuiObjectTableIndex)
            {
                IList<LuiObjectParameters> list = (IList<LuiObjectParameters>)kvp.Value.Keys.ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    Assert.AreEqual(list[i], ((IList<LuiObjectParameters>)testConfig.LuiObjectTableIndex[kvp.Key].Keys.ToList())[i]);
                }
            }

            GpibProviderParameters dependency = null;
            GpibProviderParameters testParameters = null;
            foreach(var kvp in testConfig.LuiObjectTableIndex[ddgParameters.GetType()])
            {
                if (kvp.Key.Name == ddgParameters.Name)
                {
                    dependency = ((DelayGeneratorParameters)kvp.Key).GpibProvider;
                }
            }

            foreach (var kvp in testConfig.LuiObjectTableIndex[gpibParameters1.GetType()])
            {
                if (kvp.Key.Name == gpibParameters1.Name)
                {
                    testParameters = (GpibProviderParameters)kvp.Key;
                }
            }

            Assert.AreEqual(dependency, testParameters);
            Assert.IsTrue(
                testConfig.LuiObjectTableIndex[dependency.GetType()][dependency] == 
                testConfig.LuiObjectTableIndex[testParameters.GetType()][testParameters]
                );
            Assert.IsTrue( Object.ReferenceEquals(
                    testConfig.LuiObjectTableIndex[dependency.GetType()][dependency],
                    testConfig.LuiObjectTableIndex[testParameters.GetType()][testParameters]
                    ));

            var dummy = new DummyGpibProvider();

            testConfig.LuiObjectTableIndex[dependency.GetType()][dependency] = dummy;

            Assert.IsTrue(Object.ReferenceEquals(testConfig.LuiObjectTableIndex[testParameters.GetType()][testParameters], dummy));
        }

        [TestMethod]
        public void TestInstantiation()
        {
            IEnumerable<LuiObjectParameters> dependencyOrderedParameters = Util.TopologicalSort(Config.LuiObjectParameters, p => p.Dependencies);

            bool ddg = false;
            bool gpib = false;
            foreach (var p in dependencyOrderedParameters)
            {
                if (p == gpibParameters1)
                {
                    gpib = true;
                    break;
                }
                else if (p == ddgParameters)
                {
                    ddg = true;
                    break;
                }
            }
            // Should have encountered gpibParameters1 (the dependency of ddgParameters) first,
            // thus breaking the loop after gpib is true but before ddg is true.
            Assert.IsTrue(gpib);
            Assert.IsFalse(ddg);
        }
    }
}

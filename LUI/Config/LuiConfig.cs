using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using lasercom.gpib;
using lasercom.control;
using lasercom.camera;
using lasercom.ddg;
using lasercom;
using lasercom.objects;
using log4net.Appender;
using log4net;
using log4net.Repository.Hierarchy;
using log4net.Layout;
using log4net.Core;

//  <summary>
//      Class for managing LUI XML config files.
//  </summary>

namespace LUI.config
{
    [XmlRoot( "LUIConfig" )]
    public class LuiConfig
    {
        [XmlElement(typeof(LuiApplicationParameters))]
        public LuiApplicationParameters ApplicationParameters { get; set; }

        [XmlArray("GPIBProviders")]
        [XmlArrayItem("GPIBProviderParameters", typeof(GpibProviderParameters))]
        public List<GpibProviderParameters> GpibProviders { get; set; }

        [XmlArray("BeamFlags")]
        [XmlArrayItem("BeamFlag", typeof(BeamFlagsParameters))]
        public List<BeamFlagsParameters> BeamFlags { get; set; }

        [XmlArray("Cameras")]
        [XmlArrayItem("Camera", typeof(CameraParameters))]
        public List<CameraParameters> Cameras { get; set; }

        [XmlArray("DelayGenerators")]
        [XmlArrayItem("DDG", typeof(DelayGeneratorParameters))]
        public List<DelayGeneratorParameters> DelayGenerators { get; set; }

        //[XmlArray("SerialDevices")]
        //[XmlArrayItem("SerialDevice", typeof(SerialDevice))]
        //public List<SerialDevice> SerialDevices { get; set; }

        [XmlIgnore]
        public Dictionary<Type, object> ParameterLists;

        [System.Xml.Serialization.XmlIgnore]
        public bool Ready
        {
            get
            {
                return (Cameras.Count > 0 && 
                        DelayGenerators.Count > 0 && 
                        BeamFlags.Count > 0);
            }
        }

        public LuiConfig()
            : this(LUI.Constants.DefaultConfigFileLocation)
        {

        }

        public LuiConfig(string ConfigFile)
        {
            ApplicationParameters = new LuiApplicationParameters();
            ApplicationParameters.ConfigFile = ConfigFile;
            ApplicationParameters.LogFile = LUI.Constants.DefaultLogFileLocation;
            ApplicationParameters.LogLevel = LUI.Constants.DefaultLogLevel;

            ParameterLists = new Dictionary<Type, object>();

            GpibProviders = new List<GpibProviderParameters>();
            ParameterLists.Add(typeof(GpibProviderParameters), GpibProviders);
            
            BeamFlags = new List<BeamFlagsParameters>();
            ParameterLists.Add(typeof(BeamFlagsParameters), BeamFlags);

            Cameras = new List<CameraParameters>();
            ParameterLists.Add(typeof(CameraParameters), Cameras);

            DelayGenerators = new List<DelayGeneratorParameters>();
            ParameterLists.Add(typeof(DelayGeneratorParameters), DelayGenerators);
        }

        public Commander CreateCommander()
        {
            Commander Commander = new Commander(Cameras[0], BeamFlags[0], DelayGenerators[0]);
            return Commander;
        }

        public void ConfigureLogging()
        {
            var tracer = new TraceAppender();
            Hierarchy hierarchy = (Hierarchy)LogManager.GetRepository();
            hierarchy.Root.AddAppender(tracer);
            
            PatternLayout patternLayout = new PatternLayout();
            patternLayout.ConversionPattern = "%date [%thread] %-5level %logger - %message%newline";
            patternLayout.ActivateOptions();
            tracer.Layout = patternLayout;

            FileAppender fileAppender = new FileAppender();
            fileAppender.AppendToFile = true;
            fileAppender.File = ApplicationParameters.LogFile;
            fileAppender.Layout = patternLayout;
            fileAppender.LockingModel = new FileAppender.ExclusiveLock();
            fileAppender.ActivateOptions();
            hierarchy.Root.AddAppender(fileAppender);

            //TextBoxAppender textBoxAppender = new TextBoxAppender();
            //textBoxAppender.FormName = "";
            //textBoxAppender.TextBoxName = "";
            //hierarchy.Root.AddAppender(textBoxAppender);

            hierarchy.Root.Level = hierarchy.LevelMap[ApplicationParameters.LogLevel];
            hierarchy.Configured = true;
        }
    }

}

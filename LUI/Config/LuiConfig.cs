using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using lasercom.gpib;
using lasercom.control;
using lasercom.camera;
using lasercom.ddg;
using lasercom;

//  <summary>
//      Class for managing LUI XML config files.
//  </summary>

namespace LUI.config
{
    [XmlRoot( "LUIConfig" )]
    public class LuiConfig
    {
        [XmlArray("GPIBProviders")]
        [XmlArrayItem("GPIBProviderParameters", typeof (GpibProviderParameters))]
        public List<GpibProviderParameters> GPIBProviders { get; set; }

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
        {
            GPIBProviders = new List<GpibProviderParameters>();
            BeamFlags = new List<BeamFlagsParameters>();
            Cameras = new List<CameraParameters>();
            DelayGenerators = new List<DelayGeneratorParameters>();
        }

        public Commander CreateCommander()
        {
            Commander Commander = new Commander(Cameras[0], BeamFlags[0], DelayGenerators[0]);
            return Commander;
        }
    }

}

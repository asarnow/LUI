using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using lasercom.gpib;
using lasercom.control;
using lasercom.camera;
using lasercom.ddg;

//  <summary>
//      Class for managing LUI XML config files.
//  </summary>

namespace LUI.config
{
    [XmlRoot( "LUIConfig" )]
    class LUIConfig
    {
        [XmlArray("GPIBProviders")]
        [XmlArrayItem("GPIBProviderParameters", typeof (GPIBProviderParameters))]
        public List<GPIBProviderParameters> GPIBProviders { get; set; }

        [XmlArray("BeamFlags")]
        [XmlArrayItem("BeamFlag", typeof(BeamFlagParameters))]
        public List<BeamFlagParameters> BeamFlags { get; set; }

        [XmlArray("Cameras")]
        [XmlArrayItem("Camera", typeof(CameraParameters))]
        public List<CameraParameters> Cameras { get; set; }

        [XmlArray("DelayGenerators")]
        [XmlArrayItem("DDG", typeof(DelayGeneratorParameters))]
        public List<DelayGeneratorParameters> DelayGenerators { get; set; }

        [XmlArray("SerialDevices")]
        [XmlArrayItem("SerialDevice", typeof(SerialDevice))]
        public List<SerialDevice> SerialDevices { get; set; }

        

        
        public LUIConfig()
        {
            GPIBProviders = new List<GPIBProviderParameters>();
            SerialDevices = new List<SerialDevice>();
        }

        public void SerializeToXml(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof (LUIConfig));
            using (TextWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, this);
            }
        }

        public LUIConfig DeserializeFromXml(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof (LUIConfig));
            Object obj;
            using (TextReader reader = new StreamReader(filePath))
            {
                obj = deserializer.Deserialize(reader);
            }
            return (LUIConfig) obj;
        }

        public void AddGPIBProviderParameters(GPIBProviderParameters device)
        {
            GPIBProviders.Add(device);
        }

        public void AddSerialDevice(SerialDevice device)
        {
            SerialDevices.Add(device);
        }
    }

}

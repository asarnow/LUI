using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;

//  <summary>
//      Class for managing LUI XML config files.
//  </summary>

namespace LUI.Config
{
    [XmlRoot( "LUIConfig" )]
    class Config
    {
        [XmlArray("GPIBDevices")]
        [XmlArrayItem("GPIBDevice", typeof (GPIBDevice))]
        public List<GPIBDevice> GpibDevices { get; set; }

        [XmlArray("SerialDevices")]
        [XmlArrayItem("SerialDevice", typeof(SerialDevice))]
        public List<SerialDevice> SerialDevices { get; set; }

        

        
        public Config()
        {
            GpibDevices = new List<GPIBDevice>();
            SerialDevices = new List<SerialDevice>();
        }

        public void SerializeToXml(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof (Config));
            using (TextWriter writer = new StreamWriter(filePath))
            {
                serializer.Serialize(writer, this);
            }
        }

        public Config DeserializeFromXml(string filePath)
        {
            XmlSerializer deserializer = new XmlSerializer(typeof (Config));
            Object obj;
            using (TextReader reader = new StreamReader(filePath))
            {
                obj = deserializer.Deserialize(reader);
            }
            return (Config) obj;
        }

        public void AddGpibDevice(GPIBDevice device)
        {
            GpibDevices.Add(device);
        }

        public void AddSerialDevice(SerialDevice device)
        {
            SerialDevices.Add(device);
        }
    }

}

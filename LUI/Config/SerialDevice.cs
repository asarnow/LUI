using System.Xml.Serialization;

//  <summary>
//      XML class for serial device (e.g. beam flags, pump).
//  </summary>

namespace LUI.config
{
    class SerialDevice
    {
        public enum SerialDeviceType
        {
            Pump, BeamFlags, 
        }

        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("PortName")]
        public string PortName { get; set; }

        [XmlElement("DeviceType")]
        public SerialDeviceType DeviceType { get; set; }


    }

}

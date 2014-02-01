using System.Xml.Serialization;

//  <summary>
//      Class for XML describing a configured GPIB device (e.g. Stanford DDG).
//  </summary>

namespace LUI.Config
{
    class GPIBDevice
    {
        public enum GPIBDeviceType{DigitalDelayGenerator}

        [XmlElement("Address")]
        public int Address { get; set; }
        [XmlElement("Address")]
        public string Name { get; set; }
        [XmlElement("DeviceType")]
        public GPIBDeviceType DeviceType { get; set; }
    }
}

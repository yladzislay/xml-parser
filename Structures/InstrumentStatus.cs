using System.Xml.Serialization;

namespace Structures
{
    [XmlRoot("InstrumentStatus")]
    public class InstrumentStatus
    {
        [XmlElement("PackageID")]
        public string PackageID { get; set; }

        [XmlElement("DeviceStatus")]
        public List<DeviceStatus> DeviceStatusList { get; set; }
    }
}
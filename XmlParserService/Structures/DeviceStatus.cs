using System.Xml.Serialization;

namespace XmlParserService.Structures
{
    public class DeviceStatus
    {
        [XmlElement("ModuleCategoryID")]
        public string ModuleCategoryID { get; set; }

        [XmlElement("IndexWithinRole")]
        public int IndexWithinRole { get; set; }

        [XmlElement("RapidControlStatus")]
        public string RapidControlStatus { get; set; }
    }
}
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Structures;

public class InstrumentStatus
{
    public string PackageID { get; set; }

    [XmlElement("DeviceStatus")]
    [JsonPropertyName("DeviceStatus")]
    public List<DeviceStatus> DeviceStatuses { get; set; }
}
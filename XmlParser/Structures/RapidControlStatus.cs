using System.Xml.Serialization;
using XmlParser.Structures.CombinedStatus;

namespace XmlParser.Structures;

[XmlRoot("RapidControlStatus")]
public class RapidControlStatus
{
    [XmlElement("CombinedSamplerStatus", Type = typeof(CombinedSamplerStatus))]
    [XmlElement("CombinedPumpStatus", Type = typeof(CombinedPumpStatus))]
    [XmlElement("CombinedOvenStatus", Type = typeof(CombinedOvenStatus))]
    public CombinedStatus.CombinedStatus CombinedStatus { get; set; }
}
using System.Xml.Serialization;
using Structures.CombinedStatuses;

namespace Structures;

[XmlRoot("RapidControlStatus")]
public class RapidControlStatus
{
    [XmlElement("CombinedSamplerStatus", Type = typeof(CombinedSamplerStatus))]
    [XmlElement("CombinedPumpStatus", Type = typeof(CombinedPumpStatus))]
    [XmlElement("CombinedOvenStatus", Type = typeof(CombinedOvenStatus))]
    public CombinedStatus? CombinedStatus { get; set; }
}
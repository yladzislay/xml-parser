using System.Xml.Serialization;

namespace Structures.CombinedStatus;

[XmlRoot("CombinedSamplerStatus")]
public class CombinedSamplerStatus : CombinedStatus
{
    [XmlElement("Status")]
    public int Status { get; set; }

    [XmlElement("Vial")]
    public string Vial { get; set; }

    [XmlElement("Volume")]
    public int Volume { get; set; }

    [XmlElement("MaximumInjectionVolume")]
    public int MaximumInjectionVolume { get; set; }

    [XmlElement("RackL")]
    public string RackL { get; set; }

    [XmlElement("RackR")]
    public string RackR { get; set; }

    [XmlElement("RackInf")]
    public int RackInf { get; set; }

    [XmlElement("Buzzer")]
    public bool Buzzer { get; set; }
}
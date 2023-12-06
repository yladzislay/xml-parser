using System.Xml.Serialization;

namespace Structures.CombinedStatus;

[XmlInclude(typeof(CombinedSamplerStatus))]
[XmlInclude(typeof(CombinedPumpStatus))]
[XmlInclude(typeof(CombinedOvenStatus))]
public abstract class CombinedStatus
{
    [XmlElement("ModuleState")]
    public string ModuleState { get; set; }

    [XmlElement("IsBusy")]
    public bool IsBusy { get; set; }

    [XmlElement("IsReady")]
    public bool IsReady { get; set; }

    [XmlElement("IsError")]
    public bool IsError { get; set; }

    [XmlElement("KeyLock")]
    public bool KeyLock { get; set; }
}
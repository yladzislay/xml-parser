using System.Xml.Serialization;
using XmlParserService.Structures;

namespace XmlParserService;

public static class XmlParser
{
    public static InstrumentStatus? ParseXml(string xml)
    {
        var serializer = new XmlSerializer(typeof(InstrumentStatus));
        using var reader = new StringReader(xml);
        return (InstrumentStatus?) serializer.Deserialize(reader);
    }
}

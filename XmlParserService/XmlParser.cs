﻿using System.Text.RegularExpressions;
using System.Xml.Serialization;
using XmlParserService.Structures;

namespace XmlParserService;

public static class XmlParser
{
    public static InstrumentStatus? ParseInstrumentStatus(string xml)
    {
        xml = Regex.Replace(xml, "&lt;", "<");
        xml = Regex.Replace(xml, "&gt;", ">");
        xml = Regex.Replace(xml, @"<\?xml.*\?>", string.Empty);

        var serializer = new XmlSerializer(typeof(InstrumentStatus));
        using var reader = new StringReader(xml);
        return (InstrumentStatus?) serializer.Deserialize(reader);
    }
}

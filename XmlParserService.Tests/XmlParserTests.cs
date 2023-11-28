using XmlParserService.Structures;
using Xunit;

namespace XmlParserService.Tests;

public class XmlParserTests
{
    [Fact]
    public void ParseXml_ShouldReturnInstrumentStatusObject()
    {
        var xmlFilePath = Path.Combine("XmlData", "status.xml");
        var xml = File.ReadAllText(xmlFilePath);
        
        var result = XmlParser.ParseXml(xml);
        
        Assert.NotNull(result);
        Assert.IsType<InstrumentStatus>(result);
    }
}
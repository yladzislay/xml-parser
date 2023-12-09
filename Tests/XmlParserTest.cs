using Structures.CombinedStatuses;
using XmlParser;
using Xunit;

namespace Tests;

public class XmlParserTest
{
    [Fact]
    public void InstrumentStatusShouldBeSuccessfullyParsed()
    {
        var xmlFilePath = Path.Combine("Resources", "status_1.xml");
        var xml = File.ReadAllText(xmlFilePath);
            
        var instrumentStatus = Parser.ParseInstrumentStatus(xml);
        var firstDeviceStatus = instrumentStatus?.DeviceStatuses[0];
        var secondDeviceStatus = instrumentStatus?.DeviceStatuses[1];
        var thirdDeviceStatus = instrumentStatus?.DeviceStatuses[2];

        Assert.NotNull(instrumentStatus);
        Assert.NotNull(instrumentStatus.DeviceStatuses);
        Assert.NotNull(firstDeviceStatus?.RapidControlStatus);
        Assert.NotNull(secondDeviceStatus?.RapidControlStatus);
        Assert.NotNull(thirdDeviceStatus?.RapidControlStatus);
        
        Assert.IsType<CombinedSamplerStatus>(firstDeviceStatus.RapidControlStatus.CombinedStatus);
        Assert.IsType<CombinedPumpStatus>(secondDeviceStatus.RapidControlStatus.CombinedStatus);
        Assert.IsType<CombinedOvenStatus>(thirdDeviceStatus.RapidControlStatus.CombinedStatus);
    }
}
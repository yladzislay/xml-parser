using System.Text.Json;
using Structures;
using Structures.CombinedStatuses;
using Structures.Helpers;
using Xunit;

namespace Tests;

public class JsonSerializerTest
{
    [Fact]
    public void InstrumentStatusJsonShouldBeSuccessfullyDeserialized()
    {
        var jsonFilePath = Path.Combine("Resources", "status.json");
        var json = File.ReadAllText(jsonFilePath);
        var instrumentStatus = JsonSerializer.Deserialize<InstrumentStatus>(json);
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
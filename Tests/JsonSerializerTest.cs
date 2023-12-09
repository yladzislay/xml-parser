using System.Text.Json;
using Structures;
using Structures.CombinedStatuses;
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
    
    [Fact]
    public void InstrumentStatusJsonShouldBeSuccessfullySerialized()
    {
        var jsonFilePath = Path.Combine("Resources", "status.json");
        var json = File.ReadAllText(jsonFilePath);
        var instrumentStatus = JsonSerializer.Deserialize<InstrumentStatus>(json);
        var serializedInstrumentStatus = JsonSerializer.Serialize(instrumentStatus);
        var deserializedSerializedInstrumentStatus = JsonSerializer.Deserialize<InstrumentStatus>(serializedInstrumentStatus);
        
        var firstDeviceStatus = deserializedSerializedInstrumentStatus?.DeviceStatuses[0];
        var secondDeviceStatus = deserializedSerializedInstrumentStatus?.DeviceStatuses[1];
        var thirdDeviceStatus = deserializedSerializedInstrumentStatus?.DeviceStatuses[2];
        
        Assert.NotNull(firstDeviceStatus?.RapidControlStatus.CombinedStatus);
        Assert.NotNull(secondDeviceStatus?.RapidControlStatus.CombinedStatus);
        Assert.NotNull(thirdDeviceStatus?.RapidControlStatus.CombinedStatus);
        
        Assert.IsType<CombinedSamplerStatus>(firstDeviceStatus.RapidControlStatus.CombinedStatus);
        Assert.IsType<CombinedPumpStatus>(secondDeviceStatus.RapidControlStatus.CombinedStatus);
        Assert.IsType<CombinedOvenStatus>(thirdDeviceStatus.RapidControlStatus.CombinedStatus);
    }
}
using System.Text.Json;
using AutoMapper;
using Database;
using Database.Entities;
using Structures;
using XmlParser.Helpers;
using Xunit;

namespace Tests;

public class AutoMapperTest
{
    private readonly IMapper _mapper;

    public AutoMapperTest()
    {
        var mapperConfiguration = new MapperConfiguration(
            mapperConfigurationExpression => mapperConfigurationExpression
                .AddProfile<AutoMapperProfile>());

        _mapper = mapperConfiguration.CreateMapper();
    }
        
    [Fact]
    public void InstrumentStatusShouldBeSuccessfullyMapped()
    {
        var jsonFilePath = Path.Combine("Resources", "status.json");
        var json = File.ReadAllText(jsonFilePath);
        var instrumentStatus = JsonSerializer.Deserialize<InstrumentStatus>(json)?.RandomizeModuleState();
        var instrumentStatusEntity = _mapper.Map<InstrumentStatusEntity>(instrumentStatus);
        
        Assert.NotNull(instrumentStatus);
        Assert.NotNull(instrumentStatusEntity);
        
        var firstDeviceStatus = instrumentStatusEntity.DeviceStatuses[0];
        var secondDeviceStatus = instrumentStatusEntity.DeviceStatuses[1];
        var thirdDeviceStatus = instrumentStatusEntity.DeviceStatuses[2];
        
        Assert.NotNull(firstDeviceStatus.RapidControlStatus.CombinedStatus);
        Assert.NotNull(secondDeviceStatus.RapidControlStatus.CombinedStatus);
        Assert.NotNull(thirdDeviceStatus.RapidControlStatus.CombinedStatus);
        
        Assert.IsType<CombinedSamplerStatusEntity>(firstDeviceStatus.RapidControlStatus.CombinedStatus);
        Assert.IsType<CombinedPumpStatusEntity>(secondDeviceStatus.RapidControlStatus.CombinedStatus);
        Assert.IsType<CombinedOvenStatusEntity>(thirdDeviceStatus.RapidControlStatus.CombinedStatus);
        
        for (var index = 0; index < instrumentStatusEntity.DeviceStatuses.Count; index++)
        {
            var moduleState = instrumentStatus.DeviceStatuses[index].RapidControlStatus.CombinedStatus?.ModuleState;
            var mappedModuleState = instrumentStatusEntity.DeviceStatuses[index].RapidControlStatus.CombinedStatus.ModuleState;
            Assert.NotNull(moduleState);
            Assert.NotNull(mappedModuleState);
            Assert.Equal(moduleState, mappedModuleState);
        }
    }
}
using System.Text.Json;
using AutoMapper;
using Database;
using Database.Entities;
using Structures;
using Structures.CombinedStatuses;
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
        var instrumentStatus = JsonSerializer.Deserialize<InstrumentStatus>(json);
        Assert.NotNull(instrumentStatus);
            
        var instrumentStatusEntity = _mapper.Map<InstrumentStatusEntity>(instrumentStatus);
        Assert.NotNull(instrumentStatusEntity);
        Assert.Equal(instrumentStatus.PackageID, instrumentStatusEntity.PackageID);
            
        var moduleState = instrumentStatus.DeviceStatuses[0].RapidControlStatus.CombinedStatus?.ModuleState;
        Assert.NotNull(moduleState);
            
        var mappedModuleState = instrumentStatusEntity.DeviceStatusList[0].RapidControlStatus.CombinedStatus.ModuleState;
        Assert.NotNull(mappedModuleState);
        Assert.Equal(moduleState, mappedModuleState);
            
        Assert.NotNull(instrumentStatus);
        Assert.NotNull(instrumentStatus.DeviceStatuses);
        
        var firstDeviceStatus = instrumentStatus?.DeviceStatuses[0];
        var secondDeviceStatus = instrumentStatus?.DeviceStatuses[1];
        var thirdDeviceStatus = instrumentStatus?.DeviceStatuses[2];
        
        Assert.NotNull(firstDeviceStatus?.RapidControlStatus);
        Assert.NotNull(secondDeviceStatus?.RapidControlStatus);
        Assert.NotNull(thirdDeviceStatus?.RapidControlStatus);
        
        Assert.IsType<CombinedSamplerStatus>(firstDeviceStatus.RapidControlStatus.CombinedStatus);
        Assert.IsType<CombinedPumpStatus>(secondDeviceStatus.RapidControlStatus.CombinedStatus);
        Assert.IsType<CombinedOvenStatus>(thirdDeviceStatus.RapidControlStatus.CombinedStatus);
    }
}
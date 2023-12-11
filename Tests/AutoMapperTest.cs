using System.Text.Json;
using AutoMapper;
using Database;
using Database.Entities;
using Structures;
using Structures.CombinedStatuses;
using Tests.AutoMapper;
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
    public void TestUseDestinationValueWithCondition()
    {
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile<TestMappingProfile>());
        var mapper = configuration.CreateMapper();
        var source = new TestSource { Id = 1, IsActive = true, Name = "Source Name" };
        var destination = new TestDestination { Id = 2, IsActive = false, Name = "Destination Name" };

        mapper.Map(source, destination);
        
        Assert.Equal(1, source.Id);               // Source.Id should not be changed
        Assert.True(source.IsActive);             // Source.IsActive should not be changed
        Assert.Equal("Source Name", source.Name); // Source.Name should not be changed

        Assert.Equal(2, destination.Id);                    // Destination.Id should remain unchanged
        Assert.False(destination.IsActive);                 // Destination.IsActive should remain unchanged
        Assert.Equal("Destination Name", destination.Name); // Destination.Name should remain unchanged
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
        
        for (var index = 0; index < instrumentStatusEntity.DeviceStatuses.Count; index++)
        {
            var combinedStatusEntity = instrumentStatusEntity.DeviceStatuses[index].RapidControlStatus.CombinedStatus = _mapper
                .Map<CombinedStatus, CombinedStatusEntity>(instrumentStatus.DeviceStatuses[index].RapidControlStatus.CombinedStatus,
                    instrumentStatusEntity.DeviceStatuses[index].RapidControlStatus.CombinedStatus);
            var rapidControlStatusEntity = instrumentStatusEntity.DeviceStatuses[index].RapidControlStatus = _mapper
                .Map<RapidControlStatus, RapidControlStatusEntity>(instrumentStatus.DeviceStatuses[index].RapidControlStatus,
                    instrumentStatusEntity.DeviceStatuses[index].RapidControlStatus);
            instrumentStatusEntity.DeviceStatuses[index] = _mapper
                .Map<DeviceStatus, DeviceStatusEntity>(instrumentStatus.DeviceStatuses[index]);

            instrumentStatusEntity.DeviceStatuses[index].RapidControlStatus = rapidControlStatusEntity;
            instrumentStatusEntity.DeviceStatuses[index].RapidControlStatus.CombinedStatus = combinedStatusEntity;
            
            rapidControlStatusEntity.Id = 1;
            combinedStatusEntity.Id = index + 1;
            
            var moduleState = instrumentStatus.DeviceStatuses[index].RapidControlStatus.CombinedStatus?.ModuleState;
            var mappedModuleState = instrumentStatusEntity.DeviceStatuses[index].RapidControlStatus.CombinedStatus.ModuleState;
            
            Assert.NotNull(moduleState);
            Assert.NotNull(mappedModuleState);
            Assert.Equal(moduleState, mappedModuleState);
        }
        
        Assert.Equal(1, instrumentStatusEntity.DeviceStatuses[0].RapidControlStatus.Id);
        Assert.Equal(1, instrumentStatusEntity.DeviceStatuses[0].RapidControlStatus.CombinedStatus.Id);
        Assert.Equal(2, instrumentStatusEntity.DeviceStatuses[1].RapidControlStatus.CombinedStatus.Id);
        Assert.Equal(3, instrumentStatusEntity.DeviceStatuses[2].RapidControlStatus.CombinedStatus.Id);
        
        var firstDeviceStatus = instrumentStatusEntity.DeviceStatuses[0];
        var secondDeviceStatus = instrumentStatusEntity.DeviceStatuses[1];
        var thirdDeviceStatus = instrumentStatusEntity.DeviceStatuses[2];
        
        Assert.NotNull(firstDeviceStatus.RapidControlStatus.CombinedStatus);
        Assert.NotNull(secondDeviceStatus.RapidControlStatus.CombinedStatus);
        Assert.NotNull(thirdDeviceStatus.RapidControlStatus.CombinedStatus);
        
        Assert.IsType<CombinedSamplerStatusEntity>(firstDeviceStatus.RapidControlStatus.CombinedStatus);
        Assert.IsType<CombinedPumpStatusEntity>(secondDeviceStatus.RapidControlStatus.CombinedStatus);
        Assert.IsType<CombinedOvenStatusEntity>(thirdDeviceStatus.RapidControlStatus.CombinedStatus);
    }
}
using System.Text.Json;
using AutoMapper;
using Database;
using Database.Entities;
using Structures;
using Xunit;

namespace Tests
{
    public class DatabaseUnits
    {
        private readonly IMapper _mapper;

        public DatabaseUnits()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperProfile>();
            });

            _mapper = configuration.CreateMapper();
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
            
            var moduleState = instrumentStatus.DeviceStatusList[0].RapidControlStatus.CombinedStatus.ModuleState;
            Assert.NotNull(moduleState);
            
            var mappedModuleState = instrumentStatusEntity.DeviceStatusList[0].RapidControlStatus.CombinedStatus.ModuleState;
            Assert.NotNull(mappedModuleState);
            Assert.Equal(moduleState, mappedModuleState);
            
            var firstDeviceStatus = instrumentStatusEntity.DeviceStatusList[0];
            Assert.NotNull(firstDeviceStatus.RapidControlStatus);
            Assert.IsType<CombinedSamplerStatusEntity>(firstDeviceStatus.RapidControlStatus.CombinedStatus);

            var secondDeviceStatus = instrumentStatusEntity.DeviceStatusList[1];
            Assert.NotNull(secondDeviceStatus.RapidControlStatus);
            Assert.IsType<CombinedPumpStatusEntity>(secondDeviceStatus.RapidControlStatus.CombinedStatus);

            var thirdDeviceStatus = instrumentStatusEntity.DeviceStatusList[2];
            Assert.NotNull(thirdDeviceStatus.RapidControlStatus);
            Assert.IsType<CombinedOvenStatusEntity>(thirdDeviceStatus.RapidControlStatus.CombinedStatus);
        }
    }
}
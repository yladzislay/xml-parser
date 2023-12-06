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
        }
    }
}
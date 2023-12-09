using AutoMapper;
using Database.Entities;
using Structures;
using Structures.CombinedStatuses;

namespace Database;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CombinedSamplerStatus, CombinedSamplerStatusEntity>();
        CreateMap<CombinedPumpStatus, CombinedPumpStatusEntity>();
        CreateMap<CombinedOvenStatus, CombinedOvenStatusEntity>();

        CreateMap<CombinedStatus, CombinedStatusEntity>()
            .Include<CombinedSamplerStatus, CombinedSamplerStatusEntity>()
            .Include<CombinedPumpStatus, CombinedPumpStatusEntity>()
            .Include<CombinedOvenStatus, CombinedOvenStatusEntity>();

        CreateMap<RapidControlStatus, RapidControlStatusEntity>();
        CreateMap<DeviceStatus, DeviceStatusEntity>();
        CreateMap<InstrumentStatus, InstrumentStatusEntity>();
    }
}
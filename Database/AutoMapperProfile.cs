using AutoMapper;
using Database.Entities;
using Structures;
using Structures.CombinedStatus;

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

        CreateMap<RapidControlStatus, RapidControlStatusEntity>()
            .ForMember
            (
                rapidControlStatusEntity => rapidControlStatusEntity.CombinedStatus, 
                memberConfigurationExpression => memberConfigurationExpression
                    .MapFrom(src => src.CombinedStatus)
            );

        CreateMap<DeviceStatus, DeviceStatusEntity>();
        CreateMap<InstrumentStatus, InstrumentStatusEntity>();
    }
}
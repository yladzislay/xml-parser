using AutoMapper;
using Database.Entities;
using Structures;
using Structures.CombinedStatuses;

namespace Database;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CombinedSamplerStatus, CombinedSamplerStatusEntity>()
            .ForMember(combinedSamplerStatusEntity => combinedSamplerStatusEntity.RapidControlStatus,
                memberConfigurationExpression => memberConfigurationExpression.Ignore())
            .ForMember(combinedSamplerStatusEntity => combinedSamplerStatusEntity.Id,
                memberConfigurationExpression => memberConfigurationExpression.Ignore());
        
        CreateMap<CombinedPumpStatus, CombinedPumpStatusEntity>()
            .ForMember(combinedPumpStatusEntity => combinedPumpStatusEntity.RapidControlStatus,
                memberConfigurationExpression => memberConfigurationExpression.Ignore())
            .ForMember(combinedPumpStatusEntity => combinedPumpStatusEntity.Id,
                memberConfigurationExpression => memberConfigurationExpression.Ignore());
        
        CreateMap<CombinedOvenStatus, CombinedOvenStatusEntity>()
            .ForMember(combinedOvenStatusEntity => combinedOvenStatusEntity.RapidControlStatus,
                memberConfigurationExpression => memberConfigurationExpression.Ignore())
            .ForMember(combinedOvenStatusEntity => combinedOvenStatusEntity.Id,
                memberConfigurationExpression => memberConfigurationExpression.Ignore());
        
        CreateMap<CombinedStatus, CombinedStatusEntity>()
            .Include<CombinedSamplerStatus, CombinedSamplerStatusEntity>()
            .Include<CombinedPumpStatus, CombinedPumpStatusEntity>()
            .Include<CombinedOvenStatus, CombinedOvenStatusEntity>()
            .ForMember(combinedStatusEntity => combinedStatusEntity.RapidControlStatus,
                memberConfigurationExpression => memberConfigurationExpression.Ignore())
            .ForMember(combinedStatusEntity => combinedStatusEntity.Id,
                memberConfigurationExpression => memberConfigurationExpression.Ignore());

        CreateMap<RapidControlStatus, RapidControlStatusEntity>()
            .ForMember(rapidControlStatusEntity => rapidControlStatusEntity.DeviceStatus,
                memberConfigurationExpression => memberConfigurationExpression.Ignore())
            .ForMember(rapidControlStatusEntity => rapidControlStatusEntity.Id,
                memberConfigurationExpression => memberConfigurationExpression.Ignore());

        CreateMap<DeviceStatus, DeviceStatusEntity>()
            .ForMember(deviceStatusEntity => deviceStatusEntity.InstrumentStatus,
                memberConfigurationExpression => memberConfigurationExpression.Ignore());
            
        CreateMap<InstrumentStatus, InstrumentStatusEntity>();
    }
}
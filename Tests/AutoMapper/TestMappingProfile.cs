using AutoMapper;

namespace Tests.AutoMapper;

public class TestMappingProfile : Profile
{
    public TestMappingProfile()
    {
        CreateMap<TestSource, TestDestination>()
            .ForMember(testDestination => testDestination.Id,
                memberConfigurationExpression => memberConfigurationExpression
                    .MapFrom((_, testDestination) => testDestination.Id))
            .ForMember(testDestination => testDestination.IsActive,
                memberConfigurationExpression => memberConfigurationExpression
                    .MapFrom((_, testDestination) => testDestination.IsActive))
            .ForMember(testDestination => testDestination.Name,
                memberConfigurationExpression => memberConfigurationExpression
                    .MapFrom((_, testDestination) => testDestination.Name));
    }
}
using AutoMapper;
using Net9Auth.API.Models.AggregatedLogging;
using Net9Auth.Shared.Models.AggregateLogging;

namespace Net9Auth.API.Services.AggregateLogging;

public class AggregatedLogMappingProfile : Profile
{
    public AggregatedLogMappingProfile()
    {
        CreateMap<CreateAggregatedLogDto, AggregatedLog>();
        CreateMap<AggregatedLog, AggregatedLogDto>();
    }
}
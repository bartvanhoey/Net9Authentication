using AutoMapper;
using Net9Auth.API.Models.AggregatedLogging.ExceptionLogging;
using Net9Auth.Shared.Models.AggregateLogging.ExceptionLogging;

namespace Net9Auth.API.Services.AggregateLogging.ExceptionLogging;

public class ExceptionLogMappingProfile : Profile
{
    public ExceptionLogMappingProfile()
    {
        
        CreateMap<CreateExceptionLogDto, ExceptionLog>()
            .ForMember(src => src.ExceptionFrequency, x => x.MapFrom(dest => 1))
            .ForMember(src => src.InsertTime, x => x.MapFrom(dest => DateTime.UtcNow));
        
        
        
        
        CreateMap<ExceptionLog, ExceptionLogDto>();
    }
}
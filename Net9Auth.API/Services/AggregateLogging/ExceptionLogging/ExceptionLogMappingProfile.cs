using AutoMapper;
using Net9Auth.API.Models.AggregatedLogging.ExceptionLogging;
using Net9Auth.Shared.Models.AggregateLogging.ExceptionLogging;

namespace Net9Auth.API.Services.AggregateLogging.ExceptionLogging;

public class ExceptionLogMappingProfile : Profile
{
    public ExceptionLogMappingProfile()
    {
        CreateMap<CreateExceptionLogDto, ExceptionLog>();
        CreateMap<ExceptionLog, ExceptionLogDto>();
    }
}
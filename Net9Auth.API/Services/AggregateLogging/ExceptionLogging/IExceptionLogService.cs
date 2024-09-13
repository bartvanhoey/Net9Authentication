using Net9Auth.API.Services.Infra;
using Net9Auth.Shared.Models.AggregateLogging.ExceptionLogging;

namespace Net9Auth.API.Services.AggregateLogging.ExceptionLogging;

public interface IExceptionLogService : ICrudApiService<ExceptionLogDto, CreateExceptionLogDto, UpdateExceptionLogDto, GetExceptionLogListDto>
{
    
}
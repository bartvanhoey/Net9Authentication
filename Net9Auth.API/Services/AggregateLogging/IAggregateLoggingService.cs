using Net9Auth.API.Services.Infra;
using Net9Auth.Shared.Models.AggregateLogging;

namespace Net9Auth.API.Services.AggregateLogging;

public interface IAggregatedLogService : ICrudApiService<AggregatedLogDto, CreateAggregatedLogDto, UpdateAggregatedLogDto, GetAggregatedLogListDto>
{
    
}
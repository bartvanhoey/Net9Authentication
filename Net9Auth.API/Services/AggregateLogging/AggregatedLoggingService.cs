using AutoMapper;
using Net9Auth.API.Database;
using Net9Auth.API.Models.AggregatedLogging;
using Net9Auth.Shared.Infrastructure.Functional;
using Net9Auth.Shared.Infrastructure.Functional.Errors;
using Net9Auth.Shared.Infrastructure.Models;
using Net9Auth.Shared.Models.AggregateLogging;
using static Net9Auth.Shared.Infrastructure.Functional.Result;

namespace Net9Auth.API.Services.AggregateLogging;

public class AggregatedLogService : IAggregatedLogService
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public AggregatedLogService(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public Task<Result<AggregatedLogDto>> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<PagedResultDto<AggregatedLogDto>>> GetListAsync(GetAggregatedLogListDto input)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<AggregatedLogDto>> CreateAsync(CreateAggregatedLogDto createDto)
    {
        try
        {
            
            var aggregatedLog = _mapper.Map<CreateAggregatedLogDto, AggregatedLog>(createDto);
            aggregatedLog.CreatedAt = DateTime.UtcNow;
            var dbEntity = await _db.AggregatedLogs.AddAsync(aggregatedLog);
            await _db.SaveChangesAsync();
            var aggregatedLogDto = _mapper.Map<AggregatedLog, AggregatedLogDto>(dbEntity.Entity);
            return Ok(aggregatedLogDto);
        }
        catch (Exception exception)
        {
            return Fail<AggregatedLogDto>(new BasicResultError(exception.Message));
        }
    }

    public Task<Result> UpdateAsync(Guid id, UpdateAggregatedLogDto input)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
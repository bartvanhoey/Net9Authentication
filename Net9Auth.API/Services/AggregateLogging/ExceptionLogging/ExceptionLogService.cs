using AutoMapper;
using Net9Auth.API.Database;
using Net9Auth.API.Models.AggregatedLogging.ExceptionLogging;
using Net9Auth.Shared.Infrastructure.Functional;
using Net9Auth.Shared.Infrastructure.Functional.Errors;
using Net9Auth.Shared.Infrastructure.Models;
using Net9Auth.Shared.Models.AggregateLogging.ExceptionLogging;
using static Net9Auth.Shared.Infrastructure.Functional.Result;

namespace Net9Auth.API.Services.AggregateLogging.ExceptionLogging;

public class ExceptionLogService : IExceptionLogService
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public ExceptionLogService(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public Task<Result<ExceptionLogDto>> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<PagedResultDto<ExceptionLogDto>>> GetListAsync(GetExceptionLogListDto input)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<ExceptionLogDto>> CreateAsync(CreateExceptionLogDto createDto)
    {
        try
        {
            
            var aggregatedLog = _mapper.Map<CreateExceptionLogDto, ExceptionLog>(createDto);
            aggregatedLog.InsertTime = DateTime.UtcNow;
            var dbEntity = await _db.ExceptionLogs.AddAsync(aggregatedLog);
            await _db.SaveChangesAsync();
            var aggregatedLogDto = _mapper.Map<ExceptionLog, ExceptionLogDto>(dbEntity.Entity);
            return Ok(aggregatedLogDto);
        }
        catch (Exception exception)
        {
            return Fail<ExceptionLogDto>(new BasicResultError(exception.Message));
        }
    }

    public Task<Result> UpdateAsync(Guid id, UpdateExceptionLogDto input)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
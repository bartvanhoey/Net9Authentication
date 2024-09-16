using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Net9Auth.API.Database;
using Net9Auth.API.Models.AggregatedLogging.ExceptionLogging;
using Net9Auth.Shared.Infrastructure.Functional;
using Net9Auth.Shared.Infrastructure.Functional.Errors;
using Net9Auth.Shared.Infrastructure.Models;
using Net9Auth.Shared.Models.AggregateLogging.ExceptionLogging;
using static Net9Auth.Shared.Infrastructure.Functional.Result;

namespace Net9Auth.API.Services.AggregateLogging.ExceptionLogging;

public class ExceptionLogApiService : IExceptionLogApiService
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public ExceptionLogApiService(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public Task<Result<ExceptionLogDto>> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<PagedResultDto<ExceptionLogDto>>> GetListAsync(GetExceptionLogListDto dto)
    {
        try
        {
            var exceptionLogs = await _db.ExceptionLogs.Skip(dto.SkipCount).Take(dto.MaxResultCount).ToListAsync();
            var exceptionLogDtos = _mapper.Map<List<ExceptionLog>, List<ExceptionLogDto>>(exceptionLogs);
            return Ok(new PagedResultDto<ExceptionLogDto>(exceptionLogs.Count,
                exceptionLogDtos.OrderByDescending(x => x.InsertTime).ToList()));
        }
        catch (Exception exception)
        {
            return Fail<PagedResultDto<ExceptionLogDto>>(new BasicResultError(exception.Message));
        }
    }

    public async Task<Result<ExceptionLogDto>> CreateAsync(CreateExceptionLogDto createDto)
    {
        try
        {
            var exceptionLog = _mapper.Map<CreateExceptionLogDto, ExceptionLog>(createDto);
            var frequency = await exceptionLog.SetFrequency(_db);

            await exceptionLog.RemoveSameExceptionLogsKeepLastAsync(_db, frequency, 20);
 
            var dbEntity = await _db.ExceptionLogs.AddAsync(exceptionLog);
            
            await _db.SaveChangesAsync();
            return Ok(_mapper.Map<ExceptionLog, ExceptionLogDto>(dbEntity.Entity));
        }
        catch (Exception exception)
        {
            return Fail<ExceptionLogDto>(exception);
        }
    }


    public Task<Result> UpdateAsync(Guid id, UpdateExceptionLogDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }
}
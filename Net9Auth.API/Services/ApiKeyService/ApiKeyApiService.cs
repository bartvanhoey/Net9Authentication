using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Net9Auth.API.Database;
using Net9Auth.API.Models.ApiKeys;
using Net9Auth.Shared.Infrastructure.Extensions;
using Net9Auth.Shared.Infrastructure.Functional;
using Net9Auth.Shared.Infrastructure.Functional.Errors;
using Net9Auth.Shared.Infrastructure.Models;
using Net9Auth.Shared.Models.ApiKeys;
using static System.DateTime;
using static Net9Auth.API.Infrastructure.ApiKeyGenerator;
using static Net9Auth.Shared.Infrastructure.Functional.Result;

namespace Net9Auth.API.Services.ApiKeyService;

public class ApiKeyApiService : IApiKeyApiService
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public ApiKeyApiService(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<Result<ApiKeyDto>> GetAsync(Guid id)
    {
        try
        {
            var apiKey = await _db.ApiKeys.FirstOrDefaultAsync(x => x.Id == id);
            if (apiKey == null) return Fail<ApiKeyDto>(ResultErrorFactory.NotFound(id.ToString()));
            var apiKeyDtos = _mapper.Map<ApiKey, ApiKeyDto>(apiKey);
            return Ok(apiKeyDtos);
        }
        catch (Exception exception)
        {
            return Fail<ApiKeyDto>(new BasicResultError(exception.Message));
        }
    }

    public async Task<Result<PagedResultDto<ApiKeyDto>>> GetListAsync(GetApiKeyListDto dto)
    {
        try
        {
            var apiKeys = dto.Purpose.IsNullOrWhiteSpace()
                ? await _db.ApiKeys.Skip(dto.SkipCount).Take(dto.MaxResultCount).ToListAsync()
                : await _db.ApiKeys.Where(x => x.Purpose == dto.Purpose).Skip(dto.SkipCount)
                    .Take(dto.MaxResultCount).ToListAsync();

            var apiKeyDtos = _mapper.Map<List<ApiKey>, List<ApiKeyDto>>(apiKeys);
            return Ok(new PagedResultDto<ApiKeyDto>(apiKeys.Count, apiKeyDtos.OrderByDescending(x => x.CreatedAt).ToList()));
        }
        catch (Exception exception)
        {
            return Fail<PagedResultDto<ApiKeyDto>>(new BasicResultError(exception.Message));
        }
    }

    public async Task<Result<ApiKeyDto>> CreateAsync(CreateApiKeyDto createDto)
    {
        try
        {
            var apiKey = _mapper.Map<CreateApiKeyDto, ApiKey>(createDto);
            var key = GenerateApiKey();
            apiKey.Key = key;
            apiKey.CreatedAt = UtcNow;
            var createdEntry = await _db.ApiKeys.AddAsync(apiKey);
            await _db.SaveChangesAsync();
            var apiKeyDto = _mapper.Map<ApiKey, ApiKeyDto>(createdEntry.Entity);
            return Ok(apiKeyDto);
        }
        catch (Exception exception)
        {
            return Fail<ApiKeyDto>(new BasicResultError(exception.Message));
        }
    }

    public async Task<Result> UpdateAsync(Guid id, UpdateApiKeyDto input)
    {
        try
        {
            var dbApiKey = await _db.ApiKeys.FirstOrDefaultAsync(x => x.Id == id);
            if (dbApiKey == null) throw new ArgumentNullException(nameof(dbApiKey));
            _mapper.Map(input, dbApiKey);
            await _db.SaveChangesAsync();
            return Ok();
        }
        catch (Exception exception)
        {
            return Fail(new BasicResultError(exception.Message));
        }
    }

    public Task<Result> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result> RevokeAsync(Guid id, string revokeReason)
    {
        throw new NotImplementedException();
    }
}
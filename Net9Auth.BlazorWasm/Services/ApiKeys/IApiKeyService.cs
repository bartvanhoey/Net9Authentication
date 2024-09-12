using Net9Auth.Shared.Infrastructure.Functional;
using Net9Auth.Shared.Infrastructure.Models;
using Net9Auth.Shared.Models.ApiKeys;

namespace Net9Auth.BlazorWasm.Services.ApiKeys;

public interface IApiKeyService
{
    Task<Result<ApiKeyDto>> CreateAsync(CreateApiKeyDto createDto);
    Task<Result> UpdateAsync(Guid id, UpdateApiKeyDto input);
    Task<Result<PagedResultDto<ApiKeyDto>?>> GetListAsync(GetApiKeyListDto input);
    Task<Result> DeleteAsync(Guid id);
    Task<Result> RevokeAsync(Guid id, string revokeReason);
    Task<Result<ApiKeyDto?>> GetAsync(Guid id);
    
}
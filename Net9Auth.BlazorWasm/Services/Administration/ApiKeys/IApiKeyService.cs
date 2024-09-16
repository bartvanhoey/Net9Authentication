using Net9Auth.Shared.Infrastructure.Functional;
using Net9Auth.Shared.Infrastructure.Models;
using Net9Auth.Shared.Models.ApiKeys;

namespace Net9Auth.BlazorWasm.Services.Administration.ApiKeys;

public interface IApiKeyService
{
    Task<Result<ApiKeyDto?>> CreateAsync(CreateApiKeyDto input);
    Task<Result> UpdateAsync(Guid id, UpdateApiKeyDto input);
    Task<Result<PagedResultDto<ApiKeyDto>?>> GetListAsync(GetApiKeyListDto dto);
    Task<Result> DeleteAsync(Guid id);
    Task<Result> RevokeAsync(RevokeApiKeyDto input);
    Task<Result<ApiKeyDto?>> GetAsync(Guid id);
    
}
using Net9Auth.Shared.Infrastructure.Functional;
using Net9Auth.Shared.Infrastructure.Models;
using Net9Auth.Shared.Models.ApiKeys;

namespace Net9Auth.BlazorWasm.Services.ApiKeys;

public interface IApiKeyService
{
    Task<Result<ApiKeyDto?>> CreateAsync(CreateApiKeyCtrlInput input);
    Task<Result> UpdateAsync(Guid id, UpdateApiKeyCtrlInput input);
    Task<Result<PagedResultDto<ApiKeyDto>?>> GetListAsync(GetApiKeyListCtrlInput input);
    Task<Result> DeleteAsync(Guid id);
    Task<Result> RevokeAsync(RevokeApiKeyCtrlInput input);
    Task<Result<ApiKeyDto?>> GetAsync(Guid id);
    
}
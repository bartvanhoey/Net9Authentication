using Net9Auth.API.Services.Infra;
using Net9Auth.Shared.Models.ApiKeys;

namespace Net9Auth.API.Services.ApiKeyService;

public interface IApiKeyApiService : ICrudApiService<ApiKeyDto, CreateApiKeyDto, UpdateApiKeyDto, GetApiKeyListDto>
{
    
}
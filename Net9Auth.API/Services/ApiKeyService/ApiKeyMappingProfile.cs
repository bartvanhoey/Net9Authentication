using AutoMapper;
using Net9Auth.API.Models.ApiKeys;
using Net9Auth.Shared.Models.ApiKeys;

namespace Net9Auth.API.Services.ApiKeyService;

public class ApiKeyMappingProfile : Profile
{
    public ApiKeyMappingProfile()
    {
        
        CreateMap<GetApiKeyCtrlInput, GetApiKeyDto>();
        CreateMap<GetApiKeyListCtrlInput, GetApiKeyListDto>();
        CreateMap<CreateApiKeyCtrlInput, CreateApiKeyDto>();
        CreateMap<UpdateApiKeyCtrlInput, UpdateApiKeyDto>();
        CreateMap<CreateApiKeyDto, ApiKey>();
        CreateMap<ApiKey, ApiKeyDto>();
        CreateMap<UpdateApiKeyDto, ApiKey>();
    }
}
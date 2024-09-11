using AutoMapper;
using Net9Auth.API.Models.ApiKeys;
using Net9Auth.Shared.Models.ApiKeys;

namespace Net9Auth.API.Services.ApiKeyService;

public class ApiKeyMappingProfile : Profile
{
    public ApiKeyMappingProfile()
    {
        CreateMap<CreateApiKeyDto, ApiKey>();
        CreateMap<ApiKey, ApiKeyDto>();
        CreateMap<UpdateApiKeyDto, ApiKey>();
    }
}
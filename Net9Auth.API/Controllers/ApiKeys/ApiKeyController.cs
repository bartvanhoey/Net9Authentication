using Microsoft.AspNetCore.Mvc;
using Net9Auth.API.Controllers.Authentication.Base;

using Net9Auth.API.Services.ApiKeyService;
using Net9Auth.Shared.Infrastructure.Models;
using Net9Auth.Shared.Models.ApiKeys;

namespace Net9Auth.API.Controllers.ApiKeys;


// [Authorize(Roles = "Admin")]
[ApiController]
[Route("api/api-key")]
public class ApiKeyController : CustomControllerBase
{
    private readonly IApiKeyApiService _svc;

    public ApiKeyController(IHostEnvironment env, IApiKeyApiService svc) : base(env) => _svc = svc;

    [HttpPost("create")]
    public async Task<ApiKeyCreatedDto> CreateApiKeyAsync(CreateApiKeyDto createApiKeyDto)
    {
        var result = await _svc.CreateAsync(createApiKeyDto);
        return result.IsSuccess ? new ApiKeyCreatedDto(result.Value) : new ApiKeyCreatedDto(result.Error?.Message);
    }
    
    [HttpPut("update")]
    public async Task<ApiKeyUpdatedDto> UpdateApiKeyAsync(UpdateApiKeyDto updateApiKeyDto)
    {
        var result = await _svc.UpdateAsync(updateApiKeyDto.Id, updateApiKeyDto);
        return result.IsSuccess ? new ApiKeyUpdatedDto() : new ApiKeyUpdatedDto(result.Error?.Message);
    }
    
    [HttpGet]
    public async Task<PagedResultDto<ApiKeyDto>> GetApiKeys(GetApiKeyListDto getApiKeyListDto)
    {
        var result = await _svc.GetListAsync(getApiKeyListDto);
        return result.Value;
    }
}


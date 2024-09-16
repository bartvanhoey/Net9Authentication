using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Net9Auth.API.Controllers.Authentication.Base;
using Net9Auth.API.Services.ApiKeyService;
using Net9Auth.Shared.Infrastructure.Models;
using Net9Auth.Shared.Models.ApiKeys;

namespace Net9Auth.API.Controllers.Administration.ApiKeys;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/api-key")]
public class ApiKeyController : CustomControllerBase
{
    private readonly IApiKeyApiService _svc;
    private readonly IMapper _mapper;

    public ApiKeyController(IHostEnvironment env, IApiKeyApiService svc, IMapper mapper) : base(env)
    {
        _svc = svc;
        _mapper = mapper;
    }

    [HttpPost("create")]
    public async Task<CreateApiKeyCtrlResult> CreateApiKeyAsync(CreateApiKeyDto input)
    {
        var emailAddressResult = Request.GetEmailAddress();
        if (emailAddressResult.IsFailure) return new CreateApiKeyCtrlResult(null, emailAddressResult.Error?.Message);

        var createApiKeyDto = _mapper.Map<CreateApiKeyDto, CreateApiKeyDto>(input);
        createApiKeyDto.CreatedBy = emailAddressResult.Value;

        var result = await _svc.CreateAsync(createApiKeyDto);
        return result.IsSuccess
            ? new CreateApiKeyCtrlResult(result.Value)
            : new CreateApiKeyCtrlResult(null, result.Error?.Message);
    }

    [HttpPut("update")]
    public async Task<UpdateApiKeyCtrlResult> UpdateApiKeyAsync(UpdateApiKeyDto input)
    {
        var updateApiKeyDto = _mapper.Map<UpdateApiKeyDto, UpdateApiKeyDto>(input);

        var result = await _svc.UpdateAsync(updateApiKeyDto.Id, updateApiKeyDto);
        return result.IsSuccess ? new UpdateApiKeyCtrlResult() : new UpdateApiKeyCtrlResult(result.Error?.Message);
    }

    [HttpPost]
    public async Task<PagedResultDto<ApiKeyDto>> GetApiKeys(GetApiKeyListDto dto)
    {
        var result = await _svc.GetListAsync(dto);
        return result.Value;
    }

    [HttpPost]
    [Route("by-id")]
    public async Task<GetApiKeyByIdResultDto> GetApiKeyById(GetApiKeyDto dto)
    {
        var result = await _svc.GetAsync(dto.Id);
        return result.IsSuccess
            ? new GetApiKeyByIdResultDto(result.Value)
            : new GetApiKeyByIdResultDto(result.Error?.Message);
    }
}
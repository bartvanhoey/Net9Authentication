using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Net9Auth.API.Controllers.Authentication.Base;
using Net9Auth.API.Infrastructure.ApiKeyAuthorizationFilters.Dynamic;
using Net9Auth.API.Services.AggregateLogging.ExceptionLogging;
using Net9Auth.Shared.Infrastructure.Models;
using Net9Auth.Shared.Models.AggregateLogging.ExceptionLogging;
using Net9Auth.Shared.Models.ApiKeys;

namespace Net9Auth.API.Controllers.Administration.AggregatedLogging.ExceptionLogging;

[ApiController]
[Route("api/exception-log")]
public class ExceptionLogController : CustomControllerBase
{
    private readonly IMapper _mapper;
    private readonly IExceptionLogApiService _svc;

    public ExceptionLogController(IHostEnvironment env, IMapper mapper, IExceptionLogApiService svc) : base(env)
    {
        _mapper = mapper;
        _svc = svc;
    }

    [HttpPost("create")]
    [DynamicApiKeyExceptionLogAuthorizationFilter]
    public async Task<CreateExceptionLogCtrlResult> CreateExceptionLogAsync(CreateExceptionLogDto dto)
    {
        var result = await _svc.CreateAsync(dto);
        return result.IsSuccess ? new CreateExceptionLogCtrlResult(result.Value) : new CreateExceptionLogCtrlResult(result.Error?.Message);
    }
    
    [HttpPost]
    public async Task<PagedResultDto<ExceptionLogDto>> GetExceptionLogs(GetExceptionLogListDto dto)
    {
        
        var result = await _svc.GetListAsync(dto);
        return result.Value;
    }
}
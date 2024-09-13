using Microsoft.AspNetCore.Mvc;
using Net9Auth.API.Controllers.Authentication.Base;
using Net9Auth.API.Infrastructure.ApiKeyAuthorizationFilters.Dynamic;
using Net9Auth.API.Services.AggregateLogging.ExceptionLogging;
using Net9Auth.Shared.Models.AggregateLogging.ExceptionLogging;

namespace Net9Auth.API.Controllers.AggregatedLogging.ExceptionLogging;

[ApiController]
[Route("api/exception-log")]
public class ExceptionLogController : CustomControllerBase
{
    private readonly IExceptionLogService _svc;

    public ExceptionLogController(IHostEnvironment env, IExceptionLogService svc) : base(env) => _svc = svc;

    [HttpPost("create")]
    [DynamicApiKeyExceptionLogAuthorizationFilter]
    public async Task<ExceptionLogCreatedDto> CreateExceptionLogAsync(CreateExceptionLogDto createExceptionLogDto)
    {
        var result = await _svc.CreateAsync(createExceptionLogDto);
        return result.IsSuccess ? new ExceptionLogCreatedDto(result.Value) : new ExceptionLogCreatedDto(result.Error?.Message);
    }
}
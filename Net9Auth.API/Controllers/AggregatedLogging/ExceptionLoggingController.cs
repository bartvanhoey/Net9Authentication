using Microsoft.AspNetCore.Mvc;
using Net9Auth.API.Controllers.Authentication.Base;
using Net9Auth.API.Infrastructure.ApiKeyAuthorizationFilters.Dynamic;
using Net9Auth.API.Services.AggregateLogging;
using Net9Auth.Shared.Models.AggregateLogging;

namespace Net9Auth.API.Controllers.AggregatedLogging;

[ApiController]
[Route("api/aggregated-log")]
public class AggregatedLogController : CustomControllerBase
{
    private readonly IAggregatedLogService _svc;

    public AggregatedLogController(IHostEnvironment env, IAggregatedLogService svc) : base(env) => _svc = svc;

    [HttpPost("create")]
    [DynamicApiKeyAggregatedLogAuthorizationFilter]
    public async Task<AggregatedLogCreatedDto> CreateAggregatedLogAsync(CreateAggregatedLogDto createAggregatedLogDto)
    {
        var result = await _svc.CreateAsync(createAggregatedLogDto);
        return result.IsSuccess ? new AggregatedLogCreatedDto(result.Value) : new AggregatedLogCreatedDto(result.Error?.Message);
    }
}
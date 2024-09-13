using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Net9Auth.API.Services.ApiKeyService;
using Net9Auth.Shared.Infrastructure.Extensions;
using Net9Auth.Shared.Models.ApiKeys;
using static Net9Auth.API.Infrastructure.Consts.ApplicationConstants;

namespace Net9Auth.API.Infrastructure.ApiKeyAuthorizationFilters.Dynamic;

public class DynamicApiKeyExceptionLogAuthorizationFilter : Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (context.HttpContext.Request.Headers.TryGetValue(ApiKeyHeaderName, out var contextApiKey))
        {
            var svc = context.HttpContext.RequestServices.GetRequiredService<IApiKeyApiService>();
            var apiKeysResult = await svc.GetListAsync(new GetApiKeyListDto(ExceptionLoggingPurpose));
            if (apiKeysResult.IsFailure)
                context.Result = new UnauthorizedObjectResult(apiKeysResult.Error?.Message ?? "Unknown reason");

            var apiKey = contextApiKey.FirstOrDefault();
            if (apiKey == null || apiKey.IsNullOrWhiteSpace())
                context.Result = new UnauthorizedObjectResult("API key missing");

            var apiKeys = apiKeysResult.Value.Items.Select(x => x.Key).ToList();
            if (apiKeys.Count == 0)
                context.Result = new UnauthorizedObjectResult("No API keys in list");

            if (!apiKeys.Contains(apiKey ?? throw new InvalidOperationException()))
                context.Result = new UnauthorizedObjectResult("Invalid API key");
        }
        else
            context.Result = new UnauthorizedObjectResult("API key missing");
    }
}
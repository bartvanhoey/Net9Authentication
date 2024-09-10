using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Net9Auth.Shared.Infrastructure.Extensions;

namespace Net9Auth.API.Infrastructure.ApiKeys.Dynamic;

public class DynamicApiKeyWeatherForecastAuthorizationFilter :  Attribute, IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (context.HttpContext.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var contextApiKey))
        {
            var apiKeyService = context.HttpContext.RequestServices.GetRequiredService<IDynamicApiKeyWeatherForecastService>();
            var apiKeys = await apiKeyService.GetDynamicApiKeysWeatherForecast();

            var apiKey = contextApiKey.FirstOrDefault();
            if (apiKey == null || apiKey.IsNullOrWhiteSpace()) 
                context.Result = new UnauthorizedObjectResult("API key missing");
            
            if (!apiKeys.Contains(apiKey ?? throw new InvalidOperationException())) 
                context.Result = new UnauthorizedObjectResult("Invalid API key");
        }
        else
            context.Result = new UnauthorizedObjectResult("API key missing");
    }
}
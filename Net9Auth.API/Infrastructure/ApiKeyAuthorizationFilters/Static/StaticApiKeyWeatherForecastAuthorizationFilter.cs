using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Net9Auth.API.Infrastructure.Consts;

namespace Net9Auth.API.Infrastructure.ApiKeyAuthorizationFilters.Static;


public class StaticApiKeyWeatherForecastAuthorizationFilter :  Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (context.HttpContext.Request.Headers.TryGetValue(ApplicationConstants.ApiKeyHeaderName, out var contextApiKey))
        {
            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = configuration.GetValue<string>(ApplicationConstants.ApiKeySectionName);
            if (apiKey == null || !apiKey.Equals(contextApiKey))
                context.Result = new UnauthorizedObjectResult("Invalid API key");
        }
        else
        {
            context.Result = new UnauthorizedObjectResult("API key missing");
        }
    }
}
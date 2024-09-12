using Net9Auth.API.Infrastructure.ApiKeyAuthorizationFilters.Dynamic;
using Net9Auth.API.Infrastructure.ApiKeyAuthorizationFilters.Static;

namespace Net9Auth.API.Infrastructure.ApiKeyAuthorizationFilters;

public static class ApiKeyRegistration
{
   public static void SetupApiKeyFiltering(this IServiceCollection services)
   {
      services.AddScoped<StaticApiKeyWeatherForecastAuthorizationFilter>();
      services.AddScoped<DynamicApiKeyWeatherForecastAuthorizationFilter>();
      services.AddScoped<DynamicApiKeyExceptionLogAuthorizationFilter>();
      services.AddScoped<IDynamicApiKeyWeatherForecastService, DynamicApiKeyWeatherForecastService>();
   }
}
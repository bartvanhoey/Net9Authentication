using Net9Auth.API.Infrastructure.ApiKeys.Dynamic;
using Net9Auth.API.Infrastructure.ApiKeys.Static;

namespace Net9Auth.API.Infrastructure.ApiKeys;

public static class ApiKeyRegistration
{
   public static void SetupApiKeyFiltering(this IServiceCollection services)
   {
      services.AddScoped<StaticApiKeyWeatherForecastAuthorizationFilter>();
      services.AddScoped<DynamicApiKeyWeatherForecastAuthorizationFilter>();
      services.AddScoped<IDynamicApiKeyWeatherForecastService, DynamicApiKeyWeatherForecastService>();
   }
}
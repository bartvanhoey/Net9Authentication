namespace Net9Auth.API.Infrastructure.ApiKeyAuthorizationFilters.Dynamic;

public interface IDynamicApiKeyWeatherForecastService
{
    public  Task<List<string>> GetDynamicApiKeysWeatherForecast();
}
namespace Net9Auth.API.Infrastructure.ApiKeys.Dynamic;

public interface IDynamicApiKeyWeatherForecastService
{
    public  Task<List<string>> GetDynamicApiKeysWeatherForecast();
}
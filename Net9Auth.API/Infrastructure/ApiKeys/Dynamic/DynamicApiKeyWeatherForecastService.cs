namespace Net9Auth.API.Infrastructure.ApiKeys.Dynamic;

public class DynamicApiKeyWeatherForecastService : IDynamicApiKeyWeatherForecastService
{
    public async Task<List<string>> GetDynamicApiKeysWeatherForecast()
    {
        await Task.CompletedTask;
        return
        [
            "847F1A39EC51446684F4AB6763EB5270", "847F1A39EC51446684F4AB6763EB5271", "847F1A39EC51446684F4AB6763EB5273"
        ];
    }
}
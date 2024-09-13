using Microsoft.AspNetCore.Mvc;
using Net9Auth.API.Infrastructure.ApiKeyAuthorizationFilters.Static;
using Net9Auth.Shared.Models.Weather;

namespace Net9Auth.API.Controllers.Weatherforecast;

[ApiController]
[Route("api/static-api-key-weather-forecast")]
public class StaticApiKeyWeatherForecastController(ILogger<StaticApiKeyWeatherForecastController> logger) : ControllerBase
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    [HttpGet]
    [StaticApiKeyWeatherForecastAuthorizationFilter]
    public IEnumerable<WeatherForecast> Get()
    {
        var context = HttpContext;

        var tryGetValue = context.Request.Headers.TryGetValue("x-api-key", out var apiKey);

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}

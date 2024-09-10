using System.Net;
using static System.Console;

namespace Net9Auth.Console1;

public class StaticApiKeyWeatherForecastControllerTester
{
    public async Task Start() // Only one API Key from appsettings is authorized
    {
        const string apiEndpoint = "https://localhost:7247/";
        List<string> apiKeys =
        [
            "847F1A39EC51446684F4AB6763EB5270", "847F1A39EC51446684F4AB6763EB5271", "847F1A39EC51446684F4AB6763EB5272",
            "847F1A39EC51446684F4AB6763EB5273"
        ];

        WriteLine($"{Environment.NewLine}===============Static=====================");
        foreach (var apiKey in apiKeys)
        {
            try
            {
                var httpClient = await new HttpService().GetHttpClientAsync(apiEndpoint, apiKey);
                var response = await httpClient.Value.GetAsync($"{apiEndpoint}api/static-api-key-weather-forecast");

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    WriteLine($"{Environment.NewLine}ApiKey {apiKey} is Unauthorized{Environment.NewLine}");
                    continue;
                }

                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                WriteLine($"Console1 api result: {json}");
            }
            catch (HttpRequestException)
            {
                WriteLine("Is apiEndpoint correct?");
            }
            catch (Exception exception)
            {
                WriteLine(exception.Message);
            }
        }
        WriteLine("====================================");
    }
}
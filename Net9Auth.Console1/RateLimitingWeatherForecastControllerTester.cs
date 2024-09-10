using System.Net;
using static System.Console;

namespace Net9Auth.Console1;

public class RateLimitingWeatherForecastControllerTester
{
    public async Task Start()  
    {
        const string apiEndpoint = "https://localhost:7247/";

        WriteLine($"{Environment.NewLine}===============RateLimiting=====================");

        for (int i = 0; i < 1000; i++)
        {
            try
            {
                var httpClient = await new HttpService().GetHttpClientAsync(apiEndpoint);
                var response =
                    await httpClient.Value.GetAsync($"{apiEndpoint}api/rate-limiting-weather-forecast");

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    WriteLine($"is Unauthorized{Environment.NewLine}");
                    continue;
                }

                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                WriteLine($"Console1 api result: {json}");
            }
            catch (HttpRequestException requestException)
            {
                WriteLine("Is apiEndpoint correct?");
                WriteLine(requestException.Message);
                
            }
            catch (Exception exception)
            {
                WriteLine(exception.Message);
            }
        }
    }
}
using System.Net;
using System.Net.Http.Json;
using Net9Auth.Shared.Models.AggregateLogging;
using static System.Console;

namespace Net9Auth.Console1;

public class DynamicApiKeyAggregatedLogControllerTester
{
    public async Task Start() // Multiple API Keys from IDynamicApiKeyWeatherForecastService are authorized
    {
        const string apiEndpoint = "https://localhost:7247/";
        List<string> apiKeys =
        [
            "5D8E48F659E81D89F86FED09B46F0667172AEDBBFB8AFCAEBFB3C0669AF05477183B1D8355A464679EC3B992F9075AA34958652E8E33ABE086D4DB1DADD37878"
        ];

        WriteLine($"{Environment.NewLine}===============Dynamic=====================");
        foreach (var apiKey in apiKeys)
        {
            try
            {
                var createAggregatedLogDto = new CreateAggregatedLogDto
                {
                    OriginProgram = "Console1",
                    OriginCompany = "string",
                    LogLevel = 0,
                    ExceptionName = "NullReferenceException",
                    ExceptionMessage = "string",
                    ClassName = "string",
                    MethodName = "string",
                    LineNumber = "string",
                    StackTrace = "string",
                    Description = "string"
                };

                var httpClient = await new HttpService().GetHttpClientAsync(apiEndpoint, apiKey);
                var response = await httpClient.Value.PostAsJsonAsync($"{apiEndpoint}api/aggregated-log/create",
                    createAggregatedLogDto);

                if (response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    WriteLine($"{Environment.NewLine}ApiKey {apiKey} is Unauthorized{Environment.NewLine}");
                    continue;
                }

                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                WriteLine($"Console1 api result: {json}");
            }
            catch (HttpRequestException exception)
            {
                WriteLine("Is apiEndpoint correct?");
            }
            catch (Exception exception)
            {
                WriteLine(exception.Message);
            }
        }
    }
}
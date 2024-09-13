namespace Net9Auth.ConsoleExceptionLogTester2;

public class HttpService
{
    public async Task<Lazy<HttpClient>> GetHttpClientAsync(string apiEndpoint, string? apiKey = null)
    {
        var client = new Lazy<HttpClient>(() => new HttpClient());
        if (apiKey != null && !string.IsNullOrWhiteSpace(apiKey) )
        {
            client.Value.DefaultRequestHeaders.Add("x-api-key", apiKey);
        }
        client.Value.BaseAddress = new Uri(apiEndpoint);
        return await Task.FromResult(client);
    }
}
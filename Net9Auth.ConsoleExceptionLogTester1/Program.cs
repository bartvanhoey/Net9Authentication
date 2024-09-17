// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Net.Http.Json;
using Net9Auth.ConsoleExceptionLogTester1;
using Net9Auth.Shared.Models.AggregateLogging.ExceptionLogging;
using static System.Console;



const string apiEndpoint = "https://localhost:7247/";
const string apiKey =
    "50C2BA2DADE4E8DD1CCE9E350823242856AF830318E27EE0B6F401CC3C5A9C7D192EF79DB33CC4A940DE7F43A4F21BE1FBCDB65D0E4BEB382932F53FBAB904D3";

var random = new Random();

while (true)
{
    Thread.Sleep(1000);
    try
    {
        var exceptionType = random.Next(1, 5); // Generate a random number between 1 and 4
        throw exceptionType switch
        {
            // ReSharper disable once NotResolvedInText
            1 => new ArgumentNullException("Example argument is null"),
            2 => new IndexOutOfRangeException("Example index is out of range"),
            3 => new InvalidOperationException("Example operation is invalid"),
            4 => new DivideByZeroException("Example division by zero"),
            _ => new Exception("Generic exception")
        };
    }
    catch (Exception exception)
    {
        var createExceptionLogDto = new CreateExceptionLogDto
        {
            ApplicationName = "ConsoleExceptionLogTester1",
            ExceptionType = exception.GetType().Name,
            ExceptionMessage = exception.Message,
            ClassName = "ClassNameConsoleExceptionLogTester1",
            MethodName = "MethodNameConsoleExceptionLogTester1",
            LineNumber = "5",
            StackTrace = exception.StackTrace,
            ApplicationTime = DateTime.UtcNow,
            Environment = "Development",
            UserInfo = "bartvanhoey@hotmail.com",
            ServerInfo = "Net9Auth.ConsoleExceptionLogTester1",
            CustomData = "always occurs at 2PM at night"
        };

        try
        {
            var httpClient = await new HttpService().GetHttpClientAsync(apiEndpoint, apiKey);
            var response = await httpClient.Value.PostAsJsonAsync($"{apiEndpoint}api/exception-log/create",
                createExceptionLogDto);
            if (response.StatusCode == HttpStatusCode.Unauthorized) WriteLine($"{Environment.NewLine}ApiKey {apiKey} is Unauthorized{Environment.NewLine}");
            WriteLine($"{exception.GetType().Name} sent to API from ConsoleLogTester1");
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            WriteLine($"{ex.GetType().Name}: Could NOT send {exception.GetType().Name} to API fromConsoleLogTester1");
        }
        
    }
}



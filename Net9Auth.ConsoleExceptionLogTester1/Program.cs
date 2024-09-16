// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Net.Http.Json;
using Net9Auth.ConsoleExceptionLogTester1;
using Net9Auth.Shared.Models.AggregateLogging.ExceptionLogging;
using static System.Console;



const string apiEndpoint = "https://localhost:7247/";
const string apiKey =
    "171E44BFFBEDBE696A845D2F69A36596A41AD2E41CBF1CF0B5F8C5615417CF294BB3432E19F1D2A5DA413C75002DBE9A14E111E3ABCA2F54EA9200CC58C8851B";

var random = new Random();

while (true)
{
    Thread.Sleep(1000);
    try
    {
        var exceptionType = random.Next(1, 5); // Generate a random number between 1 and 4
        throw exceptionType switch
        {
            1 => new ArgumentNullException("Example argument is null"),
            2 => new IndexOutOfRangeException("Example index is out of range"),
            3 => new InvalidOperationException("Example operation is invalid"),
            4 => new DivideByZeroException("Example division by zero"),
            _ => new Exception("Generic exception")
        };
    }
    catch (Exception exception)
    {
        WriteLine($"ConsoleLogTester1-{exception.GetType().Name}");
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
            response.EnsureSuccessStatusCode();
        }
        catch (Exception ex)
        {
            WriteLine(ex.Message);
        }
    }
}



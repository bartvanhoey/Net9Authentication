// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Net.Http.Json;
using Net9Auth.ConsoleExceptionLogTester2;
using Net9Auth.Shared.Models.AggregateLogging.ExceptionLogging;
using static System.Console;



const string apiEndpoint = "https://localhost:7247/";
const string apiKey = "7905CEDC79A19B494CFC48AFC20782FD9CF771DD66379661642CA5A6AD4EF8E1BEE94F3B78770624831F9283A3B2BF3BC08633B60A9428409925732338213D8D";

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
        WriteLine($"ConsoleLogTester2-{exception.GetType().Name}");
        var createExceptionLogDto = new CreateExceptionLogDto
        {
            ApplicationName = "ConsoleExceptionLogTester2",
            ExceptionType = exception.GetType().Name,
            ExceptionMessage = exception.Message,
            ClassName = "ClassNameConsoleExceptionLogTester2",
            MethodName = "MethodNameConsoleExceptionLogTester2",
            LineNumber = "5",
            StackTrace = exception.StackTrace,
            ApplicationTime = DateTime.UtcNow,
            Environment = "Development",
            UserInfo = "bartvanhoey@hotmail.com",
            ServerInfo = "Net9Auth.ConsoleExceptionLogTester2",
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



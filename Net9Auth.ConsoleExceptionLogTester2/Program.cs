// See https://aka.ms/new-console-template for more information

using System.Net;
using System.Net.Http.Json;
using Net9Auth.ConsoleExceptionLogTester2;
using Net9Auth.Shared.Models.AggregateLogging.ExceptionLogging;
using static System.Console;

const string apiEndpoint = "https://localhost:7247/";
const string apiKey = "B5B751E2A51F785258718440078B7601C27ED1FE1D9953CB77D066184B18C30E8DE8312DD66AA8068C3E3709D26FF8D13CBDA63D78FB01B75944C7BE445C44CA";

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
            WriteLine($"{exception.GetType().Name} sent to API from ConsoleLogTester2");
        }
        catch (Exception ex)
        {
            WriteLine($"{ex.GetType().Name}: Could NOT send {exception.GetType().Name} to API fromConsoleLogTester2");
        }
    }
}



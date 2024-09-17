using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Console;


namespace Net9Auth.WinForms
{
    internal static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
        
        static HttpClient client = new HttpClient();

        static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            // Log the exception, display it, etc
            Debug.WriteLine(e.Exception.Message);

            var trace = new StackTrace(e.Exception, true);

            var className = trace.GetFrame(0).GetMethod().ReflectedType?.FullName;
            var methodName = trace.GetFrame(0).GetMethod().Name;
            var lineNumber = trace.GetFrame(0).GetFileLineNumber();
            var columnNumber = trace.GetFrame(0).GetFileColumnNumber();
            

            WriteLine($"ClassName: {className}");
            WriteLine($"MethodName: {methodName}");
            WriteLine($"LineNumber: {lineNumber}");
            WriteLine($"ColumnNumber: {columnNumber}");

            client.BaseAddress = new Uri("https://localhost:7247/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("x-api-key", "50C2BA2DADE4E8DD1CCE9E350823242856AF830318E27EE0B6F401CC3C5A9C7D192EF79DB33CC4A940DE7F43A4F21BE1FBCDB65D0E4BEB382932F53FBAB904D3");

            var createExceptionLogDto = new CreateExceptionLogDto
            {
                ApplicationName = "Net9Auth.WinForms",
                ExceptionType = e.Exception.GetType().Name,
                ExceptionMessage = e.Exception.Message,
                ClassName = className,
                MethodName = methodName,
                LineNumber = lineNumber.ToString(),
                StackTrace = e.Exception.StackTrace,
                ApplicationTime = DateTime.UtcNow,
                Environment = "Development",
                UserInfo = "bartvanhoey@hotmail.com",
                ServerInfo = "Net9Auth.ConsoleExceptionLogTester1",
                CustomData = "always occurs at 2PM at night"
            };


            try
            {
                Task.Run(async () =>
                {
                    var response = await client.PostAsJsonAsync("api/exception-log", createExceptionLogDto);
                    WriteLine("Asynchronous work completed.");
                    response.EnsureSuccessStatusCode();
                    // return URI of the created resource.
                    var location = response.Headers.Location;
                    WriteLine($"Location: {location}");
                }).Wait();
            }
            catch (Exception exception)
            {
                WriteLine(exception);
                throw;
            }
            

            
            

          


        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            // Log the exception, display it, etc
            Debug.WriteLine((e.ExceptionObject as Exception).Message);
        }
    }

    



    public class CreateExceptionLogDto
    {
        // ExceptionType: The class

         public string ExceptionType { get; set; }

        // ExceptionMessage: The detailed message associated with the exception, often describing the error.
        public string ExceptionMessage { get; set; }

        // StackTrace: The stack trace provides the sequence of method calls that led to the exception, which is crucial for debugging.
        public string StackTrace { get; set; }
         public string ClassName { get; set; }
        public string MethodName { get; set; }

        public string LineNumber { get; set; }

        // ApplicationName: The specific application or service in which the exception occurred.
        public string ApplicationName { get; set; }

        // ApplicationTime: The exact date and time when the exception occurred.
        public DateTime? ApplicationTime { get; set; }

        public string Company { get; set; }

        // Environment: The environment where the exception occurred (e.g., Development, Testing, Production).
        public string Environment { get; set; }

        // UserInfo: Details of the user who was logged in when the exception occurred (if applicable).
        public string UserInfo { get; set; }

        // RequestDetails: Information about the request that triggered the exception, including URL, HTTP method, headers, and parameters.
        public string RequestDetails { get; set; }

        // ServerInfo: Details about the server where the exception occurred, such as hostname, IP address, and OS version.
        public string ServerInfo { get; set; }

        // CustomData: Any additional data that might be relevant, like transaction IDs, correlation IDs, or specific application context.
        public string CustomData { get; set; }

        // SeverityLevel: A classification of the exception’s impact (e.g., Critical, Warning, Info).
        public ExceptionLogLevel? SecurityLevel { get; set; }
    }

    public enum ExceptionLogLevel
    {
        /// <summary>
        /// Anything and everything you might want to know about a running block of code.
        /// </summary>
        Verbose = 0,

        /// <summary>
        /// Internal system events that aren't necessarily observable from the outside.
        /// </summary>
        Debug = 1,

        /// <summary>
        /// The lifeblood of operational intelligence - things happen.
        /// </summary>
        Information = 2,

        /// <summary>
        /// Service is degraded or endangered.
        /// </summary>
        Warning = 3,

        /// <summary>
        /// Functionality is unavailable, invariants are broken or data is lost.
        /// </summary>
        Error = 4,

        /// <summary>
        /// When one of these occurs, the place is burning    
        /// </summary>
        Fatal = 5
    }
}

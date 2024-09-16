namespace Net9Auth.Shared.Models.AggregateLogging.ExceptionLogging;

public class ExceptionLogDto
{
    public Guid Id { get; set; }
    
    // ExceptionType: The class or type of the exception (e.g., NullReferenceException, IOException, SQLException).
    public required string ExceptionType { get; set; }
    
    // ExceptionMessage: The detailed message associated with the exception, often describing the error.
    public string? ExceptionMessage { get; set; }
    
    // StackTrace: The stack trace provides the sequence of method calls that led to the exception, which is crucial for debugging.
    public string? StackTrace { get; set; }
    
    public string? ClassName { get; set; }
    public string? MethodName { get; set; }
    public string? LineNumber { get; set; }
    
    // InsertTime: The exact date and time when the exception inserted in the database.
    public DateTime InsertTime { get; set; }
    
    // ApplicationName: The specific application or service in which the exception occurred.
    public required string ApplicationName { get; set; }
    // ApplicationTime: The exact date and time when the exception occurred.
    public DateTime? ApplicationTime { get; set; }
    public string? Company { get; set; }
    
    // Environment: The environment where the exception occurred (e.g., Development, Testing, Production).
    public string? Environment { get; set; }
    
    // UserInfo: Details of the user who was logged in when the exception occurred (if applicable).
    public string? UserInfo { get; set; }
    
    // RequestDetails: Information about the request that triggered the exception, including URL, HTTP method, headers, and parameters.
    public string? RequestDetails { get; set; }
    
    // ServerInfo: Details about the server where the exception occurred, such as hostname, IP address, and OS version.
    public string? ServerInfo { get; set; }
    
    // CustomData: Any additional data that might be relevant, like transaction IDs, correlation IDs, or specific application context.
    public string? CustomData { get; set; }
    
    // SeverityLevel: A classification of the exception’s impact (e.g., Critical, Warning, Info).
    public ExceptionLogLevel? SecurityLevel { get; set; }
    
    // Resolution Status: Whether the exception has been resolved, is under investigation, or is a recurring issue.
    public string? ResolutionStatus { get; set; }
    
    // Exception Frequency: How often the exception occurs, which can help in identifying patterns or recurring issues.
    public long ExceptionFrequency { get; set; }
    
    // AssignedTo: The team or individual responsible for resolving the exception.
    public string? AssignedTo { get; set; }
    
    // Resolution Notes: Notes or steps taken to resolve the issue, which can be useful for future reference.
    public string? ResolutionNotes { get; set; }

}

namespace Net9Auth.BlazorWasm.Services.Logging;

public interface ISerilogService
{
    Task<CreateLogEntryResult> LogWarning(string message,  string? methodName);
    Task<CreateLogEntryResult> LogError(string message,  string? methodName);
    Task<CreateLogEntryResult> LogError(Exception exception,  string? methodName);
    Task<CreateLogEntryResult> LogCritical(string message,  string? methodName);
    Task<CreateLogEntryResult> LogTrace(string message,  string? methodName);
    Task<CreateLogEntryResult> LogDebug(string message,  string? methodName);
        
        
}
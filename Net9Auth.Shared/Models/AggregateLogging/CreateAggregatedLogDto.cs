using System.ComponentModel.DataAnnotations;

namespace Net9Auth.Shared.Models.AggregateLogging;

public class CreateAggregatedLogDto
{
    [Required] public string OriginProgram { get; set; } = "";
    public string? OriginCompany { get; set; } = "";
    [Required] public AggregatedLogLevel LogLevel { get; set; }
    public string? ExceptionName { get; set; }
    public string? ExceptionMessage { get; set; }
    public string? ClassName { get; set; }
    public string? MethodName { get; set; }
    public string? LineNumber { get; set; }
    public string? StackTrace { get; set; }
    public string? Description { get; set; }
    
}
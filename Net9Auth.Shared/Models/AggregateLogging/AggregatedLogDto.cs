namespace Net9Auth.Shared.Models.AggregateLogging;

public class AggregatedLogDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? OriginProgram { get; set; }
    public string? OriginCompany { get; set; }
    public string? ExceptionName { get; set; }
    public string? ExceptionMessage { get; set; }
    public string? ClassName { get; set; }
    public string? MethodName { get; set; }
    public string? LineNumber { get; set; }
    public string? StackTrace { get; set; }
    public string? Description { get; set; }
    public bool IsDeleted { get; set; }
}

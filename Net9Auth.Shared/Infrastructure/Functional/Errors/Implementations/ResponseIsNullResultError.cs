namespace Net9Auth.Shared.Infrastructure.Functional.Errors.Implementations;

public class ResponseIsNullResultError : BaseResultError
{
    public ResponseIsNullResultError(string? message) : base(message ?? "unkmown method name")
    {
    }
    
}
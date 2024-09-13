namespace Net9Auth.Shared.Infrastructure.Functional.Errors;

public class BasicResultError : BaseResultError
{
    public BasicResultError(string message = "something went wrong") : base(message)
    {
    }
}
using Net9Auth.Shared.Infrastructure.Extensions;

namespace Net9Auth.Shared.Infrastructure.Functional.Errors;

public abstract class BaseResultError
{
    protected BaseResultError(string message) => Message = message;
    protected BaseResultError() => Message 
        = GetType().Name.Replace("ResultError","").ToSentenceCase();

    public string Message { get; }
}
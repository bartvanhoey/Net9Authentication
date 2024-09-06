using Net9Auth.API.Infrastructure.Extensions;

namespace Net9Auth.API.Infrastructure.Functional.Errors;

public abstract class BaseResultError
{
    protected BaseResultError(string message) => Message = message;
    protected BaseResultError() => Message 
        = GetType().Name.Replace("ResultError","").ToSentenceCase();

    public string Message { get; }
}
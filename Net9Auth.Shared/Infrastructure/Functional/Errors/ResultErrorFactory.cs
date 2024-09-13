using System.Runtime.CompilerServices;
using Net9Auth.Shared.Infrastructure.Functional.Errors.Implementations;

namespace Net9Auth.Shared.Infrastructure.Functional.Errors;

public static class ResultErrorFactory
{
    public static BasicResultError BasicError(string? message = "something went wrong") => new(message);
    public static ApiServiceIsNullResultError ApiServiceIsNull(string apiServiceName) => new(apiServiceName);
    public static NotFoundResultError NotFound(string id, [CallerMemberName] string? callMemberName = null) => new(callMemberName == null ? id : $"{callMemberName} - id: {id}");
    
    public static ResponseIsNullResultError ResponseIsNull([CallerMemberName] string? callMemberName = "unknown method name") => new(callMemberName);
}
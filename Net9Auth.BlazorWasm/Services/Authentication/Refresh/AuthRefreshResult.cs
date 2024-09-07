using static Net9Auth.BlazorWasm.Services.Authentication.Refresh.AuthRefreshMessage;

namespace Net9Auth.BlazorWasm.Services.Authentication.Refresh;

public class AuthRefreshResult
{
    public AuthRefreshResult() => Message = Successful;
    public AuthRefreshResult(AuthRefreshMessage message) => Message = message;
    public AuthRefreshMessage Message { get; set; }
    public bool Succeeded => Message == Successful;
}
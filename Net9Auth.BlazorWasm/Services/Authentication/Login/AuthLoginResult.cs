using static Net9Auth.BlazorWasm.Services.Authentication.Login.AuthLoginMessage;

namespace Net9Auth.BlazorWasm.Services.Authentication.Login;

public class AuthLoginResult(AuthLoginMessage message)
{
    public AuthLoginResult() : this(LoginSuccess)
    {
    }

    public bool Succeeded => Message == LoginSuccess;
    public AuthLoginMessage Message { get; set; } = message;
}
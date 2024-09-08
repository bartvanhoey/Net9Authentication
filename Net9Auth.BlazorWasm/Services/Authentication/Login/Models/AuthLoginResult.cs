using static Net9Auth.BlazorWasm.Services.Authentication.Login.Models.AuthLoginMessage;

namespace Net9Auth.BlazorWasm.Services.Authentication.Login.Models;

public class AuthLoginResult(AuthLoginMessage message)
{
    public AuthLoginResult() : this(LoginSuccess)
    {
    }

    public bool Succeeded => Message == LoginSuccess;
    public AuthLoginMessage Message { get; set; } = message;
}
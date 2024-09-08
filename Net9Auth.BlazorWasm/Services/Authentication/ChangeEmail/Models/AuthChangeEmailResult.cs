namespace Net9Auth.BlazorWasm.Services.Authentication.ChangeEmail.Models;

public class AuthChangeEmailResult(AuthChangeEmailInfo message)
{
    public AuthChangeEmailResult() : this(AuthChangeEmailInfo.Successful)
    {
    }

    public bool Succeeded => Message == AuthChangeEmailInfo.Successful;
    public AuthChangeEmailInfo Message { get; } = message;

}
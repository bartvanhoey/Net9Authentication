namespace Net9Auth.BlazorWasm.Services.Authentication.ForgotPassword;

public class AuthForgotPasswordResult(AuthForgotPasswordInfo message)
{
    public AuthForgotPasswordResult() : this(AuthForgotPasswordInfo.Successful)
    {
    }

    public bool Succeeded => Message == AuthForgotPasswordInfo.Successful;
    private AuthForgotPasswordInfo Message { get; set; } = message;
}
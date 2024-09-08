using static Net9Auth.BlazorWasm.Services.Authentication.ResetPassword.Models.AuthResetPasswordInfo;

namespace Net9Auth.BlazorWasm.Services.Authentication.ResetPassword.Models;

public class AuthResetPasswordResult(AuthResetPasswordInfo message)
{
    public AuthResetPasswordResult() : this(ResetPasswordSuccessful)
    {
    }

    public bool Succeeded => Message == ResetPasswordSuccessful;
    private AuthResetPasswordInfo Message { get; set; } = message;
}
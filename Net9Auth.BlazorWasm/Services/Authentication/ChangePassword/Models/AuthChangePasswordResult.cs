namespace Net9Auth.BlazorWasm.Services.Authentication.ChangePassword.Models;

public class AuthChangePasswordResult(AuthChangePasswordInfo message)
{
    public AuthChangePasswordResult() : this(AuthChangePasswordInfo.Successful)
    {
    }

    public AuthChangePasswordResult(IEnumerable<ChangePasswordError>? errors) : this(AuthChangePasswordInfo.PasswordInvalid)
        => Errors = errors;


    public bool Succeeded => Message == AuthChangePasswordInfo.Successful;
    public AuthChangePasswordInfo Message { get; } = message;
    public IEnumerable<ChangePasswordError>? Errors { get; }
}
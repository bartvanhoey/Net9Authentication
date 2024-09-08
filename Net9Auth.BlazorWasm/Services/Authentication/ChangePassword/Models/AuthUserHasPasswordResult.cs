namespace Net9Auth.BlazorWasm.Services.Authentication.ChangePassword.Models;

public class AuthUserHasPasswordResult
{
    public AuthUserHasPasswordResult(bool userHasPassword)
    {
        Message = AuthUserHasPasswordMessage.Success;
        UserHasPassword = userHasPassword;
    }

    public AuthUserHasPasswordResult(AuthUserHasPasswordMessage message) => Message = message;

    public bool Succeeded => Message == AuthUserHasPasswordMessage.Success;
    public AuthUserHasPasswordMessage Message { get; set; }

    public bool UserHasPassword { get; set; }

}
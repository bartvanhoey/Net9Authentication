using Net9Auth.BlazorWasm.Services.Authentication.ChangePassword.Models;

namespace Net9Auth.BlazorWasm.Services.Authentication.ChangePassword;

public interface IUserHasPasswordService
{
    Task<AuthUserHasPasswordResult> UserHasPasswordAsync();
}
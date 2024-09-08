using Net9Auth.BlazorWasm.Services.Authentication.ChangePassword.Models;
using Net9Auth.Shared.Models.Authentication.ChangePassword;

namespace Net9Auth.BlazorWasm.Services.Authentication.ChangePassword;

public interface IChangePasswordService
{
    Task<AuthChangePasswordResult> ChangePasswordAsync(ChangePasswordInputModel input);
}
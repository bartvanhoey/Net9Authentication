using Net9Auth.Shared.Models.Authentication.ResetPassword;

namespace Net9Auth.BlazorWasm.Services.Authentication.ResetPassword;

public interface IResetPasswordService
{
    Task<AuthResetPasswordResult> ResetPasswordAsync(ResetPasswordInputModel input);
}
using Net9Auth.BlazorWasm.Services.Authentication.ForgotPassword.Models;
using Net9Auth.Shared.Models.Authentication.ForgotPassword;

namespace Net9Auth.BlazorWasm.Services.Authentication.ForgotPassword;

public interface IForgotPasswordService
{
    Task<AuthForgotPasswordResult> AskPasswordResetAsync(ForgotPasswordInputModel input);
}
using Net9Auth.BlazorWasm.Services.Authentication.ChangeEmail.Models;

namespace Net9Auth.BlazorWasm.Services.Authentication.ChangeEmail;

public interface IChangeEmailService
{
    Task<AuthIsEmailConfirmedResult> IsEmailConfirmedAsync();
    Task<AuthChangeEmailResult> ChangeEmailAsync(string newEmail);
    Task<AuthConfirmChangeEmailResult> ConfirmChangeEmailAsync(string email, string newEmail, string code);
}
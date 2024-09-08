using Net9Auth.BlazorWasm.Services.Authentication.ConfirmEmail.Models;
using Net9Auth.Shared.Models.Authentication.ConfirmEmail;

namespace Net9Auth.BlazorWasm.Services.Authentication.ConfirmEmail;

public interface IConfirmEmailService
{
    Task<AuthConfirmEmailResult> ConfirmEmailAsync(ConfirmEmailInputModel input);
}
using Net9Auth.BlazorWasm.Services.Authentication.ResendEmailConfirmation.Models;
using Net9Auth.Shared.Models.Authentication.ResendEmailConfirmation;

namespace Net9Auth.BlazorWasm.Services.Authentication.ResendEmailConfirmation;

public interface IResendEmailConfirmationService
{
    Task<AuthResendEmailConfirmationResult> ResendEmailConfirmationAsync(ResendEmailConfirmationInputModel input);
}
using static Net9Auth.BlazorWasm.Services.Authentication.ResendEmailConfirmation.Models.AuthResendConfirmEmailConfirmationInfo;

namespace Net9Auth.BlazorWasm.Services.Authentication.ResendEmailConfirmation.Models;

public class AuthResendEmailConfirmationResult(AuthResendConfirmEmailConfirmationInfo message)
{
    public AuthResendEmailConfirmationResult() : this(ResendEmailConfirmationSuccessful)
    {
    }

    public bool Succeeded => Message == ResendEmailConfirmationSuccessful;
    private AuthResendConfirmEmailConfirmationInfo Message { get; set; } = message;
}
using static Net9Auth.BlazorWasm.Services.Authentication.ConfirmEmail.AuthConfirmEmailInfo;

namespace Net9Auth.BlazorWasm.Services.Authentication.ConfirmEmail;

public class AuthConfirmEmailResult(AuthConfirmEmailInfo message)
{

    public AuthConfirmEmailResult() : this(ConfirmEmailSuccessful)
    {
    }

    public bool Succeeded => Message == ConfirmEmailSuccessful;
    private AuthConfirmEmailInfo Message { get; } = message;
}
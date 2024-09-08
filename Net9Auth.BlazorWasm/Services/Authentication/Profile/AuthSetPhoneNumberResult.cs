using static Net9Auth.BlazorWasm.Services.Authentication.Profile.AuthSetPhoneNumberInfo;

namespace Net9Auth.BlazorWasm.Services.Authentication.Profile;

public class AuthSetPhoneNumberResult(AuthSetPhoneNumberInfo message) 
{
    public AuthSetPhoneNumberResult() : this(SetPhoneNumberSuccessful)
    {
    }
    public bool Succeeded => Message == SetPhoneNumberSuccessful;
    private AuthSetPhoneNumberInfo Message { get; set; } = message;
}
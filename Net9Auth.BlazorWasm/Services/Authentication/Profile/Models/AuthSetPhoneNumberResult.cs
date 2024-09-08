using static Net9Auth.BlazorWasm.Services.Authentication.Profile.Models.AuthSetPhoneNumberInfo;

namespace Net9Auth.BlazorWasm.Services.Authentication.Profile.Models;

public class AuthSetPhoneNumberResult(AuthSetPhoneNumberInfo message) 
{
    public AuthSetPhoneNumberResult() : this(SetPhoneNumberSuccessful)
    {
    }
    public bool Succeeded => Message == SetPhoneNumberSuccessful;
    private AuthSetPhoneNumberInfo Message { get; set; } = message;
}
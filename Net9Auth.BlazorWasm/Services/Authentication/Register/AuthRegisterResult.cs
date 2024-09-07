using static Net9Auth.BlazorWasm.Services.Authentication.Register.AuthRegisterInfo;

namespace Net9Auth.BlazorWasm.Services.Authentication.Register;

public class AuthRegisterResult(AuthRegisterInfo message)
{
    public AuthRegisterResult() : this(RegistrationSuccessful){}
    
    public bool Succeeded => Message == RegistrationSuccessful;
    private AuthRegisterInfo Message { get; set; } = message;
}
using Net9Auth.Shared.Models.Authentication.Register;

namespace Net9Auth.BlazorWasm.Services.Authentication.Register;

public interface IRegisterService
{
    Task<AuthRegisterResult> RegisterAsync(RegisterInputModel input);
}
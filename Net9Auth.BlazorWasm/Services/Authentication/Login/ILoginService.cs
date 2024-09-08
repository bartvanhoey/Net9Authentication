using Net9Auth.BlazorWasm.Services.Authentication.Login.Models;
using Net9Auth.Shared.Models.Authentication.Login;

namespace Net9Auth.BlazorWasm.Services.Authentication.Login;

public interface ILoginService
{
    Task<AuthLoginResult> Login(LoginInputModel input);
}
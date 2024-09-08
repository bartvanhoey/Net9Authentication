using System.Net.Http.Json;
using Net9Auth.BlazorWasm.Services.Authentication.Register.Models;
using Net9Auth.Shared.Models.Authentication.Register;
using static Net9Auth.BlazorWasm.Services.Authentication.Register.Models.AuthRegisterInfo;

namespace Net9Auth.BlazorWasm.Services.Authentication.Register;

public class RegisterService(IHttpClientFactory clientFactory) : IRegisterService
{
    private readonly HttpClient _http = clientFactory.CreateClient("ServerAPI");

    public async Task<AuthRegisterResult> RegisterAsync(RegisterInputModel input)
    {
        RegisterResult? result;
        try
        {
            var response = await _http.PostAsJsonAsync("api/account/register", input);
            result = await response.Content.ReadFromJsonAsync<RegisterResult>();
        }
        catch (Exception exception)
        {
            Console.WriteLine(exception.Message);
            return new AuthRegisterResult(SomethingWentWrong);
        }

        return result is { Succeeded: true }
            ? new AuthRegisterResult()
            : new AuthRegisterResult(RegistrationUnsuccessful);
    }
}
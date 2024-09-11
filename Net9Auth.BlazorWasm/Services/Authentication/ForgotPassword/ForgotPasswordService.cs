using System.Net.Http.Json;
using Net9Auth.BlazorWasm.Services.Authentication.ForgotPassword.Models;
using Net9Auth.BlazorWasm.Services.Authentication.Register.Models;
using Net9Auth.Shared.Models.Authentication.ForgotPassword;
using static Net9Auth.BlazorWasm.Services.Authentication.ForgotPassword.Models.AuthForgotPasswordInfo;

namespace Net9Auth.BlazorWasm.Services.Authentication.ForgotPassword;

public class ForgotPasswordService(IHttpClientFactory clientFactory) : IForgotPasswordService
{
    private readonly HttpClient _http = clientFactory.CreateClient("ServerAPI");

    public async Task<AuthForgotPasswordResult> AskPasswordResetAsync(ForgotPasswordInputModel input)
    {
        RegisterResult? result;
        try
        {
            var response = await _http.PostAsJsonAsync("api/account/forgot-password", input);
            result = await response.Content.ReadFromJsonAsync<RegisterResult>();
        }
        catch (Exception)
        {
            return new AuthForgotPasswordResult(SomethingWentWrong);
        }

        return result is { Succeeded: true }
            ? new AuthForgotPasswordResult()
            : new AuthForgotPasswordResult(UnSuccessful);
    }
}
using System.Net.Http.Json;
using Net9Auth.Shared.Models.Authentication.ConfirmEmail;
using static Net9Auth.BlazorWasm.Services.Authentication.ConfirmEmail.AuthConfirmEmailInfo;

namespace Net9Auth.BlazorWasm.Services.Authentication.ConfirmEmail;

public class ConfirmEmailService(IHttpClientFactory clientFactory) : IConfirmEmailService
{
    private readonly HttpClient _http = clientFactory.CreateClient("ServerAPI");

    public async Task<AuthConfirmEmailResult> ConfirmEmailAsync(ConfirmEmailInputModel input)
    {
        ConfirmEmailResult? result;
        try
        {
            var response = await _http.PostAsJsonAsync("api/account/confirm-email",input);
            result = await response.Content.ReadFromJsonAsync<ConfirmEmailResult>();
        }
        catch (Exception)
        {
            return  new AuthConfirmEmailResult(SomethingWentWrong);    
        }

        return result is { Succeeded: true }
            ? new AuthConfirmEmailResult()
            : new AuthConfirmEmailResult(ConfirmEmailUnsuccessful);
    }
}
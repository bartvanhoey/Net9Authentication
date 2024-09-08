using System.Net.Http.Json;
using Net9Auth.BlazorWasm.Services.Authentication.ConfirmEmail;
using Net9Auth.BlazorWasm.Services.Authentication.ConfirmEmail.Models;
using Net9Auth.BlazorWasm.Services.Authentication.ResendEmailConfirmation.Models;
using Net9Auth.Shared.Models.Authentication.ConfirmEmail;
using Net9Auth.Shared.Models.Authentication.ResendEmailConfirmation;
using static Net9Auth.BlazorWasm.Services.Authentication.ResendEmailConfirmation.Models.AuthResendConfirmEmailConfirmationInfo;

namespace Net9Auth.BlazorWasm.Services.Authentication.ResendEmailConfirmation;

public class ResendEmailConfirmationService(IHttpClientFactory clientFactory) : IResendEmailConfirmationService
{
    private readonly HttpClient _http = clientFactory.CreateClient("ServerAPI");
    public async Task<AuthResendEmailConfirmationResult> ResendEmailConfirmationAsync(
        ResendEmailConfirmationInputModel input)
    {
        ConfirmEmailResult? result;
        try
        {
            var response = await _http.PostAsJsonAsync("api/account/resend-email-confirmation", input);
            result = await response.Content.ReadFromJsonAsync<ConfirmEmailResult>();
        }
        catch (Exception )
        {
            // TODO logging
            return new AuthResendEmailConfirmationResult(SomethingWentWrong);
        }

        return result is { Succeeded: true }
            ? new AuthResendEmailConfirmationResult()
            : new AuthResendEmailConfirmationResult(ResendEmailConfirmationUnsuccessful);
    }
}
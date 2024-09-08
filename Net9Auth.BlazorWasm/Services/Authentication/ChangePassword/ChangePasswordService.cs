using System.Net.Http.Json;
using Net9Auth.BlazorWasm.Services.Authentication.ChangePassword.Models;
using Net9Auth.BlazorWasm.Services.Logging;
using Net9Auth.Shared.Models.Authentication.ChangePassword;

namespace Net9Auth.BlazorWasm.Services.Authentication.ChangePassword;

public class ChangePasswordService(IHttpClientFactory clientFactory, ISerilogService serilogService) : IChangePasswordService
{
    private readonly HttpClient _http = clientFactory.CreateClient("ServerAPI");

    public async Task<AuthChangePasswordResult> ChangePasswordAsync(ChangePasswordInputModel input)
    {
        try
        {
            var response = await _http.PostAsJsonAsync("api/account/change-password", input);
            var result = await response.Content.ReadFromJsonAsync<ChangePasswordResult>();
            if (result != null && result.Succeeded) return new AuthChangePasswordResult();    
            await serilogService.LogError(result?.Errors?.FirstOrDefault()?.Code ?? "Change password went wrong", nameof(ChangePasswordAsync));
            return new AuthChangePasswordResult(result?.Errors);
        }
        catch (Exception exception)
        {
            await serilogService.LogError(exception, nameof(ChangePasswordAsync));
            return new AuthChangePasswordResult(AuthChangePasswordInfo.SomethingWentWrong);
        }

    }
}
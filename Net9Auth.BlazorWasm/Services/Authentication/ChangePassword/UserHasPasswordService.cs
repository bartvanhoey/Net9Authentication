using System.Net.Http.Json;
using Net9Auth.BlazorWasm.Services.Authentication.ChangePassword.Models;
using Net9Auth.BlazorWasm.Services.Logging;
using Net9Auth.Shared.Models.Authentication.ChangePassword;
using static Net9Auth.BlazorWasm.Services.Authentication.ChangePassword.Models.AuthUserHasPasswordMessage;

namespace Net9Auth.BlazorWasm.Services.Authentication.ChangePassword;

public class UserHasPasswordService(IHttpClientFactory clientFactory, ISerilogService serilogService) : IUserHasPasswordService
{
    private readonly HttpClient _http = clientFactory.CreateClient("ServerAPI");

    public async Task<AuthUserHasPasswordResult> UserHasPasswordAsync()
    {
        try
        {
            var response = await _http.GetFromJsonAsync<UserHasPasswordResult>("api/account/user-has-password");
            if (response is { Succeeded: true }) return new AuthUserHasPasswordResult(response.UserHasPassword);
            await serilogService.LogError(response?.Message ?? "something went wrong", nameof(UserHasPasswordAsync));
            return new AuthUserHasPasswordResult(SomethingWentWrong);
        }
        catch (Exception exception)
        {
            await serilogService.LogError(exception, nameof(UserHasPasswordAsync));
            return new AuthUserHasPasswordResult(SomethingWentWrong);
        }
    }
}
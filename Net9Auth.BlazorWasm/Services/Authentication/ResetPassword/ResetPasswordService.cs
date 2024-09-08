﻿using System.Net.Http.Json;
using Net9Auth.BlazorWasm.Services.Authentication.ResetPassword.Models;
using Net9Auth.Shared.Models.Authentication.ResetPassword;
using static Net9Auth.BlazorWasm.Services.Authentication.ResetPassword.Models.AuthResetPasswordInfo;

namespace Net9Auth.BlazorWasm.Services.Authentication.ResetPassword;

public class ResetPasswordService(IHttpClientFactory clientFactory) : IResetPasswordService
{
    private readonly HttpClient _http = clientFactory.CreateClient("ServerAPI");

    public async Task<AuthResetPasswordResult> ResetPasswordAsync(ResetPasswordInputModel input)
    {
        ResetPasswordResult? result;
        try
        {
            var response = await _http.PostAsJsonAsync("api/account/reset-password", input);
            result = await response.Content.ReadFromJsonAsync<ResetPasswordResult>();
        }
        catch (Exception)
        {
            // TODO logging
            return new AuthResetPasswordResult(SomethingWentWrong);
        }

        return result is { Succeeded: true }
            ? new AuthResetPasswordResult()
            : new AuthResetPasswordResult(ResetPasswordUnSuccessful);
    }
}
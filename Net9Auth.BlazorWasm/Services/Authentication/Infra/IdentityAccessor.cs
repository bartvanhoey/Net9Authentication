using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Components.Authorization;

namespace Net9Auth.BlazorWasm.Services.Authentication.Infra;

public class IdentityAccessor(AuthenticationStateProvider authenticationStateProvider) : IIdentityAccessor
{
    private async Task<IIdentity?> GetIdentityAsync()
        => (await authenticationStateProvider.GetAuthenticationStateAsync()).User.Identity;
    private async Task<ClaimsPrincipal?> GetUserAsync()
        => (await authenticationStateProvider.GetAuthenticationStateAsync()).User;

    public async Task<string?> GetUserNameAsync() => (await GetIdentityAsync())?.Name;

    public async Task<string?> GetUserIdAsync()
    {
        var claims = (await GetUserAsync())?.Claims;
        return claims == null ? string.Empty : claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    }
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Net9Auth.API.Models;
using Net9Auth.API.Models.Authentication;

namespace Net9Auth.API.Controllers.Authentication.Base;

public static class ApplicationUserExtensions
{
    public static async Task<JwtSecurityToken> GenerateJwtToken(this ApplicationUser user,
        UserManager<ApplicationUser> userManager, IConfiguration configuration, string jwtValidIssuer,
        string jwtValidAudience, string jwtSecurityKey)
    {
        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Email ?? throw new InvalidOperationException()),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var userRoles = await userManager.GetRolesAsync(user);
        if (userRoles is { Count: > 0 })
            authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

        var expiryInSeconds = configuration["Jwt:ExpiryInSeconds"] ??
                              throw new InvalidOperationException("ExpiryInSeconds not set");

        var token = new JwtSecurityToken(
            jwtValidIssuer,
            jwtValidAudience,
            expires: DateTime.UtcNow.AddSeconds(double.Parse(expiryInSeconds)), // 1 hour = 3600 sec
            claims: authClaims,
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecurityKey)),
                SecurityAlgorithms.HmacSha256)
        );
        return token;
    }

    public static async Task AddToUserRoleAsync(this ApplicationUser user, UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        if (!await roleManager.RoleExistsAsync(UserRoles.User))
            await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

        await userManager.AddToRoleAsync(user, UserRoles.User);
    }

    public static async Task AddToAdminRoleIfAdministratorAsync(this ApplicationUser user,
        UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
    {
        var admins = configuration.GetSection("ProgramAdministrators").Get<List<string>>();
        if (admins != null && admins.Contains(user.Email ?? throw new InvalidOperationException()))
        {
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));

            await userManager.AddToRoleAsync(user, UserRoles.Admin);
        }
    }
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Net9Auth.API.Controllers.Authentication.Base;
using Net9Auth.API.Models;
using Net9Auth.API.Models.Authentication.Responses.Login;
using Net9Auth.Shared.Models.Authentication.Login;
using static System.DateTime;
using static Microsoft.AspNetCore.Http.StatusCodes;
using Convert = System.Convert;

namespace Net9Auth.API.Controllers.Authentication;

[Route("api/account")]
[ApiController]
public class LoginController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IHostEnvironment environment, 
    IConfiguration configuration, ILogger<LoginController> logger)
#pragma warning disable CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
    : AuthControllerBase(userManager, roleManager, configuration, environment)
#pragma warning restore CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
{
    [HttpPost]
    [Route("login")]
    [ProducesResponseType(Status200OK, Type = typeof(LoginResponse))]
    [ProducesResponseType(Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] LoginInputModel? model)
    {
        try
        {
            var validationResult = ValidateControllerInputModel(model, logger, nameof(Login));
            if (validationResult.IsFailure) return Nok500<LoginResponse>(logger, validationResult.Error?.Message);
                
            var user = await userManager.FindByEmailAsync(model?.Email ?? string.Empty);

            if (user == null) return Nok404CouldNotFindUser<LoginResponse>(logger);
            
            if (IsNullOrWhiteSpace(user.Email)) return Nok400Email<LoginResponse>(logger);
            
            var isPasswordValid = await userManager.CheckPasswordAsync(user, model?.Password ?? throw new InvalidOperationException());
            if (!isPasswordValid) return Nok500<LoginResponse>(logger, "Invalid password");
            
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = UtcNow.AddHours(24);

            await userManager.UpdateAsync(user);

            var jwtSecurityToken = await user.GenerateJwtToken(UserManager, Configuration,
                validationResult.Value.ValidIssuer, validationResult.Value.Origin, validationResult.Value.SecurityKey);
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return Ok(new LoginResponse(accessToken, refreshToken, jwtSecurityToken.ValidTo));
        }
        catch (Exception exception)
        {
            return Nok500Exception<LoginResponse>(logger, exception);
        }
    }

    private static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var generator = RandomNumberGenerator.Create();
        generator.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}
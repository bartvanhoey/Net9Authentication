using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Net9Auth.API.Controllers.Authentication.Base;
using Net9Auth.API.Models;
using Net9Auth.API.Models.Authentication.Responses.IsEmailConfirmed;


namespace Net9Auth.API.Controllers.Authentication;

[ApiController]
[Route("api/account")]
[Authorize]
#pragma warning disable CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
public class IsEmailConfirmedController(UserManager<ApplicationUser> userManager, IHostEnvironment environment,
    IConfiguration configuration,
    ILogger<UserHasPasswordController> logger) : AuthControllerBase(userManager, configuration, environment)
#pragma warning restore CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
{
    [Authorize]
    [HttpGet]
    [Route("is-email-confirmed")]
    public async Task<IActionResult> IsEmailConfirmed()
    {
        try
        {
            var result = ValidateController(logger, nameof(IsEmailConfirmed));
            if (result.IsFailure) return Nok500<IsEmailConfirmedResponse>(logger, result.Error?.Message);

            var email = HttpContext.User.Identity?.Name;
            if (email == null) return Nok500EmailIsNull<IsEmailConfirmedResponse>(logger);

            var user = await userManager.FindByEmailAsync(email);
            if (user == null) return Nok500CouldNotFindUser<IsEmailConfirmedResponse>(logger);

            var isEmailConfirmed = await userManager.IsEmailConfirmedAsync(user);
            return Ok(new IsEmailConfirmedResponse("Success", isEmailConfirmed));
        }
        catch (Exception exception)
        {
            return Nok500<IsEmailConfirmedResponse>(logger, exception);
        }
    }
}
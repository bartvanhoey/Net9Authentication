using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Net9Auth.API.Controllers.Authentication.Base;
using Net9Auth.API.Models;
using Net9Auth.API.Models.Authentication.Responses.ConfirmChangeEmail;
using Net9Auth.Shared.Infrastructure.Extensions;
using Net9Auth.Shared.Models.Authentication.ConfirmChangeEmail;
using static System.Text.Encoding;
using static Microsoft.AspNetCore.WebUtilities.WebEncoders;

namespace Net9Auth.API.Controllers.Authentication;

[ApiController]
[Route("api/account")]
public class ConfirmChangeEmailController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IHostEnvironment environment,
#pragma warning disable CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
    IConfiguration configuration,
    ILogger<ChangeEmailController> logger) : AuthControllerBase(userManager, roleManager, configuration, environment)
#pragma warning restore CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
{
    [HttpPost]
    [Route("confirm-change-email")]
    public async Task<IActionResult> ConfirmChangeEmail([FromBody] ConfirmChangeEmailInputModel model)
    {
        try
        {
            var validationResult = ValidateControllerInputModel(model, logger, nameof(ConfirmChangeEmail));
            if (validationResult.IsFailure) return Nok500<ConfirmChangeEmailResponse>(logger, validationResult.Error?.Message);

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null) return Nok404CouldNotFindUser<ConfirmChangeEmailResponse>(logger);

            if (model.Code.IsNullOrWhiteSpace()) return Nok500CodeIsNull<ConfirmChangeEmailResponse>(logger);

            var code = UTF8.GetString(Base64UrlDecode(model.Code));

            var changeEmailResult = await userManager.ChangeEmailAsync(user, model.NewEmail, code);
            if (!changeEmailResult.Succeeded) return Nok500<ConfirmChangeEmailResponse>(logger, changeEmailResult.Errors);
           
            var setUserNameResult = await userManager.SetUserNameAsync(user, model.NewEmail); // change username also
            if (setUserNameResult is { Succeeded: true }) return Ok200<ConfirmChangeEmailResponse>("Email confirmed successfully");

            await userManager.ChangeEmailAsync(user, model.Email ?? throw new InvalidOperationException(),
                code); // if username could not be changed, set email back to old email

            return Nok500<ConfirmChangeEmailResponse>(logger, setUserNameResult.Errors);
        }
        catch (Exception exception)
        {
            return Nok500Exception<ConfirmChangeEmailResponse>(logger, exception);
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Net9Auth.API.Controllers.Authentication.Base;
using Net9Auth.API.Models;
using Net9Auth.API.Models.Authentication.Responses.SetPhoneNumber;
using Net9Auth.Shared.Infrastructure.Extensions;
using Net9Auth.Shared.Models.Authentication.SetPhoneNumber;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Net9Auth.API.Controllers.Authentication;

[Route("api/account")]
[ApiController]
public class SetPhoneNumberController(UserManager<ApplicationUser> userManager,  RoleManager<IdentityRole> roleManager, IHostEnvironment environment,
    ILogger<SetPhoneNumberController> logger,
    IConfiguration configuration)
#pragma warning disable CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
    : AuthControllerBase(userManager, roleManager, configuration, environment)
#pragma warning restore CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
{
    [Authorize]
    [HttpPost]
    [Route("set-phone-number")]
    public async Task<IActionResult> SetPhoneNumber([FromBody] SetPhoneNumberInputModel model)
    {
        try
        {
            var validationResult = ValidateControllerInputModel(model, logger, nameof(SetPhoneNumber));
            if (validationResult.IsFailure)
                return StatusCode(Status500InternalServerError,
                    new SetPhoneNumberResponse("Error", validationResult.Error?.Message ?? "something went wrong"));

            var email = HttpContext.User.Identity?.Name;
            if (email.IsNullOrWhiteSpace())
            {
                logger.LogError($"{nameof(SetPhoneNumber)}: Email was null");
                return StatusCode(Status500InternalServerError,
                    new SetPhoneNumberResponse("Error", "email was null"));
            }

            var user = email == null ? null : await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                logger.LogError($"{nameof(SetPhoneNumber)}: User retrieval went wrong");
                return StatusCode(Status500InternalServerError,
                    new SetPhoneNumberResponse("Error", "User retrieval went wrong"));
            }

            var result = await userManager.SetPhoneNumberAsync(user, model.PhoneNumber);
            if (result.Succeeded)
                return Ok(
                    new SetPhoneNumberResponse("Success", userName: user.UserName, phoneNumber: model.PhoneNumber));

            logger.LogError($"{nameof(SetPhoneNumber)}: Update phone number went wrong");
            return StatusCode(Status500InternalServerError,
                new SetPhoneNumberResponse("Error", "update phone number went wrong"));
        }
        catch (Exception exception)
        {
            logger.LogError(exception, nameof(SetPhoneNumber));
            return StatusCode(Status500InternalServerError,
                new SetPhoneNumberResponse("Error", "Update phone number went wrong"));
        }
    }
}
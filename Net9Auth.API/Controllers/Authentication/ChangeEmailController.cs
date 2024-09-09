using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Net9Auth.API.Controllers.Authentication.Base;
using Net9Auth.API.Models;
using Net9Auth.API.Models.Authentication.Responses.ChangeEmail;
using Net9Auth.Shared.Infrastructure.Extensions;
using Net9Auth.Shared.Models.Authentication.ChangeEmail;
using static System.Text.Encoding;
using static Microsoft.AspNetCore.WebUtilities.WebEncoders;

namespace Net9Auth.API.Controllers.Authentication;

[ApiController]
[Route("api/account")]
public class ChangeEmailController(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager, IHostEnvironment environment, IEmailSender<ApplicationUser> emailSender,
#pragma warning disable CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
    IConfiguration configuration, ILogger<ChangeEmailController> logger) : AuthControllerBase(userManager, roleManager, configuration, environment)
#pragma warning restore CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
{

    [HttpPost]
    [Authorize]
    [Route("change-email")]
    public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailInputModel model)
    {
        try
        {
            var result = ValidateControllerInputModel(model, logger, nameof(ChangeEmail));
            if (result.IsFailure) return Nok500<ChangeEmailResponse>(logger);
                
            var email = HttpContext.User.Identity?.Name;
            if (email.IsNullOrWhiteSpace()) return Nok400Email<ChangeEmailResponse>(logger );

            var user = email == null ? null : await userManager.FindByEmailAsync(email);
            if (user == null) return Nok404CouldNotFindUser<ChangeEmailResponse>(logger);
            
            var code = await userManager.GenerateChangeEmailTokenAsync(user, model.NewEmail ?? throw new InvalidOperationException("NewEmail was null"));
            code = Base64UrlEncode(UTF8.GetBytes(code));

            var callbackUrl = $"{HttpContext.Request.Headers.Origin}/Account/ConfirmEmailChange";
            var userId = await userManager.GetUserIdAsync(user);
            var confirmationLink = callbackUrl.AddUrlParameters(new Dictionary<string, object?>
                { ["userId"] = userId, ["email"] = user.Email, ["newEmail"] = model.NewEmail, ["code"] = code });

            await emailSender.SendConfirmationLinkAsync(user, model.NewEmail, HtmlEncoder.Default.Encode(confirmationLink));

            return Ok(new ChangeEmailResponse("Resend Email Confirmation successful"))  ;
        }
        catch (Exception exception)
        {
            return Nok500Exception<ChangeEmailResponse>(logger, exception);
        }

    }
}
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Net9Auth.API.Controllers.Authentication.Base;
using Net9Auth.API.Models;
using Net9Auth.API.Models.Authentication;
using Net9Auth.API.Models.Authentication.Responses.Register;
using Net9Auth.Shared.Infrastructure.Extensions;
using Net9Auth.Shared.Models.Authentication.Register;
using static System.Activator;
using static System.Text.Encoding;
using static Microsoft.AspNetCore.WebUtilities.WebEncoders;

namespace Net9Auth.API.Controllers.Authentication;

[ApiController]
[Route("api/account")]
public class RegisterController(UserManager<ApplicationUser> userManager, IHostEnvironment environment, IEmailSender<ApplicationUser> emailSender, IConfiguration configuration, ILogger<RegisterController> logger, 
    RoleManager<IdentityRole> roleManager)
#pragma warning disable CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
    : AuthControllerBase(userManager, configuration, environment)
#pragma warning restore CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
{
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterInputModel model)
    {
        try
        {

            var validationResult = ValidateControllerInputModel(model, logger, nameof(Register));
            if (validationResult.IsFailure) return Nok500<RegisterResponse>(logger, validationResult.Error?.Message);

            var callbackUrl = $"{HttpContext.Request.Headers.Origin.FirstOrDefault()}/Account/ConfirmEmail";
            if (IsNullOrEmpty(model.Email)) return Nok500<RegisterResponse>(logger, "Email is null");

            if (IsNullOrEmpty(model.Password)) return Nok500<RegisterResponse>(logger, "Password is null");

            var user = await userManager.FindByEmailAsync(model.Email);
            if (user != null) return Nok500<RegisterResponse>(logger, $"{nameof(Register)}: user '{model.Email}' already exists");

            var newUser = CreateApplicationUser();
            if (newUser == null) return Nok500<RegisterResponse>(logger, "New user is null");

            newUser.Email = model.Email;
            newUser.UserName = model.Email;

            var createUserResult = await userManager.CreateAsync(newUser, model.Password);
            if (!createUserResult.Succeeded) return Nok500<RegisterResponse>(logger, createUserResult.Errors);

            var userId = await userManager.GetUserIdAsync(newUser);
            var code = await userManager.GenerateEmailConfirmationTokenAsync(newUser);
            code = Base64UrlEncode(UTF8.GetBytes(code));

            var confirmationLink = callbackUrl.AddUrlParameters(new Dictionary<string, object?>
            { ["userId"] = userId, ["code"] = code, ["returnUrl"] = null });

            await emailSender.SendConfirmationLinkAsync(newUser, newUser.Email, confirmationLink);
            
            return Ok(new RegisterResponse("Success", code, userId));
        }
        catch (Exception exception)
        {
            return Nok500<RegisterResponse>(logger, exception);
        }
    }

    [HttpPost]
    [Route("register-admin")]
    public async Task<IActionResult> RegisterAdmin([FromBody] RegisterInputModel model)
    {
        var userExists = await userManager.FindByNameAsync(model.Email);
        if (userExists != null)
            return StatusCode(StatusCodes.Status500InternalServerError,
                new RegisterResponse() { Status = "Error", Message = "User already exists" });
    
        ApplicationUser user = new ApplicationUser
        {
            Email = model.Email,
            SecurityStamp =Guid.NewGuid().ToString(),
            UserName = model.Email
        };
        var result = await userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
            return StatusCode(StatusCodes.Status500InternalServerError,
                new RegisterResponse
                {
                    Status = "Error", Message = "User creation failed! Please check user details and try again."
                });

        if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
             await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
        if (!await roleManager.RoleExistsAsync(UserRoles.User))
            await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
    
        if (await roleManager.RoleExistsAsync(UserRoles.Admin))
        {
            await userManager.AddToRoleAsync(user, UserRoles.Admin);
        }
    
        return Ok(new RegisterResponse { Status = "Success", Message = "User created successfully" });
    }


    private static ApplicationUser? CreateApplicationUser()
    {
        try
        {
            return CreateInstance<ApplicationUser>();
        }
        catch
        {
            return null;
        }
    }
}
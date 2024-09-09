using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Net9Auth.API.Models;
using Net9Auth.API.Models.Authentication.Responses;
using Net9Auth.Shared.Infrastructure.Extensions;
using Net9Auth.Shared.Infrastructure.Functional;
using Net9Auth.Shared.Infrastructure.Functional.Errors;
using Net9Auth.Shared.Models.Authentication;
using static Net9Auth.Shared.Infrastructure.Functional.Result;

namespace Net9Auth.API.Controllers.Authentication.Base;

public class AuthControllerBase : CustomControllerBase
{
    private readonly IHostEnvironment _env;
    protected readonly IConfiguration Configuration;
    protected readonly RoleManager<IdentityRole> RoleManager;
    protected readonly UserManager<ApplicationUser> UserManager;

    public AuthControllerBase(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,
        IConfiguration configuration,
        IHostEnvironment env) : base(env)
    {
        UserManager = userManager;
        RoleManager = roleManager;
        Configuration = configuration;
        _env = env;
    }

    

    

    protected Result<ValidateControllerResult> ValidateControllerInputModel<T>(BaseInputModel? input, ILogger<T> logger,
        string methodName)
    {
        if (input is not null) return ValidateController(logger, methodName);
        logger.LogError($"{methodName}: input is null");
        return Fail<ValidateControllerResult>(new ResultError("input is null"));
    }

    protected Result<ValidateControllerResult> ValidateController<T>(ILogger<T> logger, string methodName)
    {
        var securityKey = Configuration["Jwt:SecurityKey"];
        if (IsNullOrEmpty(securityKey))
        {
            logger.LogError($"{methodName}: security key is null");
            return Fail<ValidateControllerResult>(new ResultError("security key is null"));
        }

        var validIssuer = Configuration["Jwt:ValidIssuer"];
        if (IsNullOrEmpty(validIssuer))
        {
            logger.LogError($"{methodName}: valid issuer is null");
            return Fail<ValidateControllerResult>(new ResultError("valid issuer is null"));
        }

        var originResult = ValidateOrigin(logger, methodName);
        return originResult.IsSuccess
            ? Result.Ok(new ValidateControllerResult(securityKey, validIssuer, originResult.Value.Origin))
            : Fail<ValidateControllerResult>(
                new ResultError(originResult.Error?.Message ?? "something went wrong"));
    }

    protected Result<ValidateOriginResult> ValidateOrigin<T>(ILogger<T> logger, string methodName)
    {
        var validAudiences = Configuration.GetSection("Jwt:ValidAudiences").Get<List<string>>();

        if (validAudiences == null || validAudiences.Count == 0)
        {
            logger.LogError($"{methodName}: audience is null");
            return Fail<ValidateOriginResult>(new ResultError("audience is null"));
        }

        var origin = HttpContext.Request.Headers.Origin.FirstOrDefault();
        if (origin.IsNotNullOrWhiteSpace() && validAudiences.Contains(origin ?? throw new InvalidOperationException()))
            return Result.Ok(new ValidateOriginResult(origin));

        logger.LogError($"{methodName}: origin is wrong: {origin}");
        return Fail<ValidateOriginResult>(new ResultError("origin is wrong"));
    }
    
    protected IActionResult Nok500CodeIsNull<T>(ILogger logger, [CallerMemberName] string memberName = "")
        where T : IControllerResponse  => Nok500<T>(logger, $"{memberName}-Code is null");
}
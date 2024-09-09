using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Net9Auth.API.Models.Authentication.Responses;
using Net9Auth.Shared.Infrastructure.Extensions;
using Net9Auth.Shared.Infrastructure.Functional.Errors;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace Net9Auth.API.Controllers.Authentication.Base;

public class CustomControllerBase : ControllerBase
{
    private readonly IHostEnvironment _env;

    public CustomControllerBase(IHostEnvironment env) => _env = env;
    
    protected IActionResult Ok200<T>(string? message = null)
    {
#pragma warning disable CA2263
        var controllerResponse = Activator.CreateInstance(typeof(T)) as IControllerResponse;
#pragma warning restore CA2263
        if (controllerResponse == null) return Ok();
        controllerResponse.Status = "Success";
        controllerResponse.Message = message.IsNullOrWhiteSpace() ? "success" : message;
        return Ok(controllerResponse);
    }
    protected IActionResult Nok500<T>(ILogger logger, string? error = "something went wrong",
        LogLevel level = LogLevel.Error,
        [CallerMemberName] string member = "") where T : IControllerResponse =>
        logger.Log<T>(_env, Status500InternalServerError, member, null, level, error);
    
    protected IActionResult Nok500Exception<T>(ILogger logger, Exception exception,
        [CallerMemberName] string member = "") where T : IControllerResponse =>
        logger.Log<T>(_env, Status500InternalServerError, member, exception);

    private IActionResult Nok404<T>(ILogger logger, string? error = "something went wrong",
        LogLevel logLevel = LogLevel.Error,
        [CallerMemberName] string member = "") where T : IControllerResponse =>
        logger.Log<T>(_env, Status404NotFound, member, null, logLevel, error);

    private IActionResult Nok400<T>(ILogger logger, string? error = "something went wrong",
        LogLevel level = LogLevel.Error,
        [CallerMemberName] string member = "") where T : IControllerResponse =>
        logger.Log<T>(_env, Status400BadRequest, member, null, level, error);

    protected IActionResult Nok500RoleIsNullOrWhiteSpace<T>(ILogger logger, [CallerMemberName] string member = "")
        where T : IControllerResponse =>
        Nok500<T>(logger, "Role is null or whitespace", LogLevel.Warning, member);

    protected IActionResult Nok404CouldNotFindUser<T>(ILogger logger, [CallerMemberName] string member = "")
        where T : IControllerResponse =>
        Nok404<T>(logger, "Could not find user", LogLevel.Warning, member);

    protected IActionResult Nok400Email<T>(ILogger logger, [CallerMemberName] string member = "")
        where T : IControllerResponse =>
        Nok400<T>(logger, "Email is null or white space", LogLevel.Warning, member);

    protected IActionResult Nok400Password<T>(ILogger logger, [CallerMemberName] string member = "")
        where T : IControllerResponse =>
        Nok400<T>(logger, "Password is null or white space", LogLevel.Warning, member);

    protected IActionResult Nok400UserIdsAreNotTheSame<T>(ILogger logger, [CallerMemberName] string member = "")
        where T : IControllerResponse =>
        Nok400<T>(logger, "User ids are not the same", LogLevel.Warning, member);

    protected IActionResult Nok500RoleDoesNotExist<T>(ILogger logger, [CallerMemberName] string member = "")
        where T : IControllerResponse =>
        Nok500<T>(logger, "User role does not exist", LogLevel.Warning, member);

    protected IActionResult Nok500AccessToken<T>(ILogger logger, [CallerMemberName] string member = "")
        where T : IControllerResponse =>
        Nok500<T>(logger, "Access token is null or white space", LogLevel.Warning, member);

    protected IActionResult Nok500RefreshToken<T>(ILogger logger, [CallerMemberName] string member = "")
        where T : IControllerResponse =>
        Nok500<T>(logger, "Refresh token is null or white space", LogLevel.Warning, member);

    protected IActionResult Nok500WrongRefreshToken<T>(ILogger logger, [CallerMemberName] string member = "")
        where T : IControllerResponse =>
        Nok500<T>(logger, "Something wrong with Refresh token", LogLevel.Warning, member);

    protected IActionResult Nok500Principal<T>(ILogger logger, [CallerMemberName] string member = "")
        where T : IControllerResponse =>
        Nok500<T>(logger, "Principal is null", LogLevel.Warning, member);

    protected IActionResult Nok500EmailFromRequestIsNullOrWhiteSpace<T>(ILogger logger,
        BaseResultError? error,
        [CallerMemberName] string member = "") where T : IControllerResponse =>
        Nok500<T>(logger, error?.Message, LogLevel.Warning, member);

    protected IActionResult Nok500CannotRemoveOwnAdminRole<T>(ILogger logger, [CallerMemberName] string member = "")
        where T : IControllerResponse =>
        Nok500<T>(logger, "An admin user cannot remove his own admin role", LogLevel.Warning, member);

    protected IActionResult Nok500<T>(ILogger logger, IEnumerable<IdentityError>? errors,
        [CallerMemberName] string member = "") where T : IControllerResponse =>
        logger.Log<T>(_env, Status500InternalServerError, member, null, LogLevel.Error, errors == null
            ? "Errors is null"
            : Join(";", errors.Select(x => $"{x.Code} - {x.Description}").ToList()));
}
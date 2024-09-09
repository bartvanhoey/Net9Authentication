using Microsoft.AspNetCore.Mvc;
using Net9Auth.API.Models.Authentication.Responses;

namespace Net9Auth.API.Controllers.Authentication.Base;

public static class LoggerExtensions
{
    public static IActionResult Log<T>(this ILogger logger, IHostEnvironment environment,
        int statusCode, string memberName,
        Exception? exception, LogLevel? logLevel = LogLevel.Error, string? errorMessage = "something went wrong")
        where T : IControllerResponse
    {
        if (exception != null)
            logger.LogError(exception, memberName);
        else
            switch (logLevel)
            {
                case LogLevel.Information:
                    logger.LogInformation($"{memberName} : {errorMessage}");
                    break;
                case LogLevel.Warning:
                    logger.LogWarning($"{memberName} : {errorMessage}");
                    break;
                case LogLevel.Error:
                    logger.LogError($"{memberName} : {errorMessage}");
                    break;
                case LogLevel.Critical:
                    logger.LogCritical($"{memberName} : {errorMessage}");
                    break;
                case LogLevel.Trace:
                    logger.LogTrace($"{memberName} : {errorMessage}");
                    break;
                case LogLevel.Debug:
                    logger.LogDebug($"{memberName} : {errorMessage}");
                    break;
                case LogLevel.None:
                    break;
                case null:
                    break;
                default:
                    logger.LogError($"{memberName} : {errorMessage}");
                    break;
            }

        if (Activator.CreateInstance(typeof(T)) is not IControllerResponse response)
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        response.Status = "Error";
        response.Message = errorMessage;

        return environment.IsDevelopment()
            ? new ObjectResult(response) { StatusCode = statusCode }
            : new StatusCodeResult(StatusCodes.Status500InternalServerError);
    }
}
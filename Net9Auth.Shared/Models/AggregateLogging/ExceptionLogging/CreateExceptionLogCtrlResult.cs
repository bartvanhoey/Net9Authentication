using Net9Auth.Shared.Infrastructure.Models;

namespace Net9Auth.Shared.Models.AggregateLogging.ExceptionLogging;

public class CreateExceptionLogCtrlResult : Infrastructure.Models.IHaveISuccessAndErrorMessage
{
    public ExceptionLogDto? ExceptionLogDto { get; }

    public CreateExceptionLogCtrlResult(ExceptionLogDto? apiKeyDto)
    {
        ExceptionLogDto = apiKeyDto;
        IsSuccess = true;
    }

    public CreateExceptionLogCtrlResult(string? errorMessage) => ErrorMessage = errorMessage;
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
}
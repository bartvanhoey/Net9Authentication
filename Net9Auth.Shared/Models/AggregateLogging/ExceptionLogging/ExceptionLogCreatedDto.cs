using Net9Auth.Shared.Infrastructure.Models;

namespace Net9Auth.Shared.Models.AggregateLogging.ExceptionLogging;

public class ExceptionLogCreatedDto : Infrastructure.Models.IHaveISuccessAndErrorMessage
{
    public ExceptionLogDto? ExceptionLogDto { get; }

    public ExceptionLogCreatedDto(ExceptionLogDto? apiKeyDto)
    {
        ExceptionLogDto = apiKeyDto;
        IsSuccess = true;
    }

    public ExceptionLogCreatedDto(string? errorMessage) => ErrorMessage = errorMessage;
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
}
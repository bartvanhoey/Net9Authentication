using Net9Auth.Shared.Infrastructure.Models;

namespace Net9Auth.Shared.Models.AggregateLogging.ExceptionLogging;

public class ExceptionLogUpdatedDto : IHaveISuccessAndErrorMessage
{
    public ExceptionLogUpdatedDto() => IsSuccess = true;

    public ExceptionLogUpdatedDto(string? errorMessage) => ErrorMessage = errorMessage;
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
}
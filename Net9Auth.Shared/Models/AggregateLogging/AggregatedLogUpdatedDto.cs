using Net9Auth.Shared.Infrastructure.Models;

namespace Net9Auth.Shared.Models.AggregateLogging;

public class AggregatedLogUpdatedDto : IHaveISuccessAndErrorMessage
{
    public AggregatedLogUpdatedDto() => IsSuccess = true;

    public AggregatedLogUpdatedDto(string? errorMessage) => ErrorMessage = errorMessage;
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
}
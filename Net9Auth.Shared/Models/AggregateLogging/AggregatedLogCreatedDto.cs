using Net9Auth.Shared.Infrastructure.Models;

namespace Net9Auth.Shared.Models.AggregateLogging;

public class AggregatedLogCreatedDto : IHaveISuccessAndErrorMessage
{
    public AggregatedLogDto? AggregatedLogDto { get; }

    public AggregatedLogCreatedDto(AggregatedLogDto? apiKeyDto)
    {
        AggregatedLogDto = apiKeyDto;
        IsSuccess = true;
    }

    public AggregatedLogCreatedDto(string? errorMessage) => ErrorMessage = errorMessage;
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
}
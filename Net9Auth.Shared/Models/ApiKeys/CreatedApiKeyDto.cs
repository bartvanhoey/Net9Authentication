namespace Net9Auth.Shared.Models.ApiKeys;

public class CreatedApiKeyDto : IHaveISuccessAndErrorMessage
{
    public ApiKeyDto? ApiKeyDto { get; }

    public CreatedApiKeyDto(ApiKeyDto? apiKeyDto)
    {
        ApiKeyDto = apiKeyDto;
        IsSuccess = true;
    }

    public CreatedApiKeyDto(string? errorMessage) => ErrorMessage = errorMessage;
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
}
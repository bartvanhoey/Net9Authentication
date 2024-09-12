namespace Net9Auth.Shared.Models.ApiKeys;

public class GetApiKeyByIdResultDto : IHaveISuccessAndErrorMessage
{
    public ApiKeyDto? ApiKeyDto { get; set; }

    public GetApiKeyByIdResultDto() {}
    
    public GetApiKeyByIdResultDto(ApiKeyDto? apiKeyDto)
    {
        ApiKeyDto = apiKeyDto;
        IsSuccess = true;
    }

    public GetApiKeyByIdResultDto(string? errorMessage) => ErrorMessage = errorMessage;
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
}
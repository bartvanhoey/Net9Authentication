namespace Net9Auth.Shared.Models.ApiKeys;

public class ApiKeyUpdatedDto : IHaveISuccessAndErrorMessage
{
    public ApiKeyUpdatedDto() => IsSuccess = true;

    public ApiKeyUpdatedDto(string? errorMessage) => ErrorMessage = errorMessage;
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
}
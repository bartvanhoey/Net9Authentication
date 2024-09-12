namespace Net9Auth.Shared.Models.ApiKeys;

public class UpdatedApiKeyDto : IHaveISuccessAndErrorMessage
{
    public UpdatedApiKeyDto() => IsSuccess = true;

    public UpdatedApiKeyDto(string? errorMessage) => ErrorMessage = errorMessage;
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
}




namespace Net9Auth.Shared.Models.ApiKeys;

public class UpdateApiKeyCtrlResult : ICtrlResult
{
    public UpdateApiKeyCtrlResult() => IsSuccess = true;

    public UpdateApiKeyCtrlResult(string? errorMessage) => ErrorMessage = errorMessage;
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
}




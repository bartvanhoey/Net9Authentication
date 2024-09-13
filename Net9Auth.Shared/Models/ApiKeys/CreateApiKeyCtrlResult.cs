using System.Text.Json.Serialization;

namespace Net9Auth.Shared.Models.ApiKeys;

public class CreateApiKeyCtrlResult : ICtrlResult
{
    public ApiKeyDto? ApiKeyDto { get; set; }
    
    
    public CreateApiKeyCtrlResult(ApiKeyDto? apiKeyDto = null, string? errorMessage = null)
    {
        if (errorMessage != null)
            IsSuccess = false;
        else
        {
            ApiKeyDto = apiKeyDto;
            IsSuccess = true;    
        }
        
    }

    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
}
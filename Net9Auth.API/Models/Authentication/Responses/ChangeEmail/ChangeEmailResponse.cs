namespace Net9Auth.API.Models.Authentication.Responses.ChangeEmail;

public class ChangeEmailResponse :IControllerResponse
{
    public ChangeEmailResponse(string message) => Message = message;

    public ChangeEmailResponse(string status, string message)
    {
        Status = status;
        Message = message;
    }
    
    
    public string? Status { get; set; } 

    public string? Message { get; set; } 
    public IEnumerable<ControllerResponseError>? Errors { get; set; }
}
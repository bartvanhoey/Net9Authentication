namespace Net9Auth.API.Models.Authentication.Responses.ChangePassword;

public class ChangePasswordResponse : IControllerResponse
{
    public ChangePasswordResponse()
    {
        
    }

    public ChangePasswordResponse(string? status, string? message)
    {
        Status = status;
        Message = message;
    }

    public ChangePasswordResponse(string? status, IEnumerable<ControllerResponseError> errors)
    {
        Status = status;
        Errors = errors;
    } 

    public string? Status { get; set; } 
    public string? Message { get; set; }
    public IEnumerable<ControllerResponseError>? Errors { get; set; }
}
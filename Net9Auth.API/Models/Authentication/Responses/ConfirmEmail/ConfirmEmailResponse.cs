namespace Net9Auth.API.Models.Authentication.Responses.ConfirmEmail;

public class ConfirmEmailResponse : IControllerResponse
{
    public ConfirmEmailResponse()
    {
    }

    public ConfirmEmailResponse(string? status, string? message)
    {
        Status = status;
        Message = message;
    }

    public string? Status { get; set; }
    public string? Message { get; set; }
    public IEnumerable<ControllerResponseError>? Errors { get; set; }
    public string? Code { get; set; }
    public string? UserId { get; set; }
}
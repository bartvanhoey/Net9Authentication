using Net9Auth.Shared.Models.Authentication;

namespace Net9Auth.API.Models.Authentication.Responses.ForgotPassword;

public class ForgotPasswordResponse : IControllerResponse
{
    public ForgotPasswordResponse()
    {
    }

    public ForgotPasswordResponse(string? status, string? message)
    {
        Status = status;
        Message = message;
    }

    public ForgotPasswordResponse(string? status, string? message, string? code) : this()
    {
        Status = status;
        Message = message;
        Code = code;
    }

    public string? Status { get; set; }
    public string? Code { get; set; }
    public string? Message { get; set; }
    public IEnumerable<ControllerResponseError>? Errors { get; set; }
}
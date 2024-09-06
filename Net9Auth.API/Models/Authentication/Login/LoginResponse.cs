namespace Net9Auth.API.Models.Authentication.Login;

public class LoginResponse : IControllerResponse
{
    public LoginResponse()
    {
    }

    public LoginResponse(string? status, string? message)
    {
        Status = status;
        Message = message;
    }

    public LoginResponse(string? accessToken, string refreshToken, DateTime validTo) : this()
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
        ValidTo = validTo;
        Successful = true;
    }

    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime ValidTo { get; set; }
    public bool Successful { get; set; }
    public string? Status { get; set; }
    public string? Message { get; set; }
    public IEnumerable<ControllerResponseError>? Errors { get; set; }
}
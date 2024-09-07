namespace Net9Auth.API.Models.Authentication.Responses.UserHasPassword;

public class UserHasPasswordResponse(string? status, bool userHasPassword)
{
    public UserHasPasswordResponse(string? status, string? message) : this(status, false) => Message = message;

    public string? Status { get; set; } = status;
    public string? Message { get; set; }
    public bool UserHasPassword { get; set; } = userHasPassword;
}
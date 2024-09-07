namespace Net9Auth.API.Models.Authentication.Responses;

public class ControllerResponseError(string code, string description)
{
    public string Code { get; } = code;
    public string Description { get; } = description;
}
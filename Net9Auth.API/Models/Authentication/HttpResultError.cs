namespace Net9Auth.API.Models.Authentication;

public class HttpResultError(string code, string description)
{
    public string Code { get; } = code;
    public string Description { get; } = description;
}
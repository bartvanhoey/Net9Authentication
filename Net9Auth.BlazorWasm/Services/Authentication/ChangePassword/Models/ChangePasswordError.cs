namespace Net9Auth.BlazorWasm.Services.Authentication.ChangePassword.Models;

public class ChangePasswordError(string code, string description)
{
    public string Code { get; } = code;
    public string Description { get; } = description;
}
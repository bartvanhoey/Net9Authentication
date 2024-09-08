namespace Net9Auth.BlazorWasm.Services.Authentication.ChangePassword.Models;

public class UserHasPasswordResult
{
    public string? Status { get; set; }
    public string? Message { get; set; }
    public bool Succeeded => Status == "Success";
    public bool UserHasPassword { get; set; }
}
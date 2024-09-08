namespace Net9Auth.BlazorWasm.Services.Authentication.ChangePassword.Models;

public class ChangePasswordResult
{
    public string? Message { get; set; }
    public bool Succeeded => Status == "Success";
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string? Status { get; set; }
        
    public IEnumerable<ChangePasswordError>? Errors { get; set; }
}
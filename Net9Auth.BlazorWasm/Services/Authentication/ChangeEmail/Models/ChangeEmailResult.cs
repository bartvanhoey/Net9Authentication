namespace Net9Auth.BlazorWasm.Services.Authentication.ChangeEmail.Models;

public class ChangeEmailResult : IResponseContentResult
{
    public string? Message { get; set; }
    public IEnumerable<HttpResultError>? Errors { get; set; }
    public bool Succeeded => Status == "Success";
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    public string? Status { get; set; }

}
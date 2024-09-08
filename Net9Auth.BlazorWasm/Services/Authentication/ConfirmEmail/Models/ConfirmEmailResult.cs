namespace Net9Auth.BlazorWasm.Services.Authentication.ConfirmEmail.Models;

public class ConfirmEmailResult : IResponseContentResult
{
    public string? Status { get; set; }
    public string? Message { get; set; }
    public IEnumerable<HttpResultError>? Errors { get; set; }
    public bool Succeeded => Status == "Success";
}
namespace Net9Auth.BlazorWasm.Services.Authentication;

public interface IResponseContentResult
{
    public string? Status { get; set; }
    public string? Message { get; set; }
    public IEnumerable<HttpResultError>? Errors { get; set; }
}
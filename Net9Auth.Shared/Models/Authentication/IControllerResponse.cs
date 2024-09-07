namespace Net9Auth.Shared.Models.Authentication;


public interface IControllerResponse
{
    public string? Status { get; set; }
    public string? Message { get; set; }
    public IEnumerable<ControllerResponseError>? Errors { get; set; }
}
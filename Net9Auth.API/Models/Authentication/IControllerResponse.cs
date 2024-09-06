namespace Net9Auth.API.Models.Authentication;


public interface IControllerResponse
{
    public string? Status { get; set; }
    public string? Message { get; set; }
    public IEnumerable<ControllerResponseError>? Errors { get; set; }
}
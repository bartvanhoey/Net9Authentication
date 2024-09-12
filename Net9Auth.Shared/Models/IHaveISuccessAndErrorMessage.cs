namespace Net9Auth.Shared.Models;

public interface IHaveISuccessAndErrorMessage
{
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
}
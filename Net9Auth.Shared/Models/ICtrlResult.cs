namespace Net9Auth.Shared.Models;

public interface ICtrlResult
{
    public bool IsSuccess { get; set; }
    public string? ErrorMessage { get; set; }
}
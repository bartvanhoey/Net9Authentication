namespace Net9Auth.Shared.Models.Authentication.ConfirmEmail;

public class ConfirmEmailInputModel(string userId, string code) : BaseInputModel
{
    public string UserId { get; } = userId;
    public string Code { get; } = code;
}
using System.ComponentModel.DataAnnotations;

namespace Net9Auth.Shared.Models.Authentication.ChangeEmail;

public class ChangeEmailInputModel : BaseInputModel
{
    public ChangeEmailInputModel()
    {
    }

    public ChangeEmailInputModel(string? newEmail)
    {
        NewEmail = newEmail;
    }

    [Required][EmailAddress] public string? NewEmail { get; set; } = "";
}
using System.ComponentModel.DataAnnotations;

namespace Net9Auth.Shared.Models.Authentication.ForgotPassword;

public class ForgotPasswordInputModel
{
    [Required] [EmailAddress] public string Email { get; set; } = "";
}
using System.ComponentModel.DataAnnotations;

// ReSharper disable PropertyCanBeMadeInitOnly.Global

namespace Net9Auth.Shared.Models.Authentication.ConfirmChangeEmail;

public class ConfirmChangeEmailInputModel(string email, string newEmail, string code) : BaseInputModel
{
    [Required][EmailAddress] public string Email { get; set; } = email;
    [Required][EmailAddress] public string NewEmail { get; set; } = newEmail;
    [Required] public string Code { get; set; } = code;
}
using System.ComponentModel.DataAnnotations;

namespace Net9Auth.Shared.Models.Authentication.ResendEmailConfirmation;

public class ResendEmailConfirmationInputModel : BaseInputModel
{
    [Required] [EmailAddress] public string Email { get; set; } = "";
   
}
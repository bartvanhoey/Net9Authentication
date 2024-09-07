using System.ComponentModel.DataAnnotations;

namespace Net9Auth.Shared.Models.Authentication.SetPhoneNumber;

public class SetPhoneNumberInputModel : BaseInputModel
{
    [Phone]
    [Display(Name = "Phone Number")]
    public string? PhoneNumber { get; set; } = "";
}
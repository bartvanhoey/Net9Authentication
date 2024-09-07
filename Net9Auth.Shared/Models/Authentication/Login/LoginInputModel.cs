using System.ComponentModel.DataAnnotations;

namespace Net9Auth.Shared.Models.Authentication.Login;

public sealed class LoginInputModel : BaseInputModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } = "";

    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = "";

    [Display(Name = "Remember me?")]
    public bool RememberMe { get; set; }
}


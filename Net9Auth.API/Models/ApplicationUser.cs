using Microsoft.AspNetCore.Identity;

// ReSharper disable EntityFramework.ModelValidation.UnlimitedStringLength

namespace Net9Auth.API.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiry { get; set; }
    }
}
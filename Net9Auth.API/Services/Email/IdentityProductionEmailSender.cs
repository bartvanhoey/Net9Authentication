using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Net9Auth.API.Models;

namespace Net9Auth.API.Services.Email;

public class IdentityProductionEmailSender : IEmailSender<ApplicationUser>
{
    private readonly IEmailSender _emailSender;

    public IdentityProductionEmailSender(IConfiguration configuration)
    {
        var googleEmail = configuration.GetRequiredSection("GoogleSmtp:GoogleEmail").Value ?? throw new ArgumentNullException();
        var googleAppPassword = configuration.GetRequiredSection("GoogleSmtp:GoogleAppPassword").Value ?? throw new ArgumentNullException();
        _emailSender = new ProductionEmailSender(googleEmail, googleAppPassword);
    }

    public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink) =>
        _emailSender.SendEmailAsync(email, "Confirm your email", $"Please confirm your account by <a href='{confirmationLink}'>clicking here</a>.");

    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink) =>
        _emailSender.SendEmailAsync(email, "Reset your password", $"Please reset your password by <a href='{resetLink}'>clicking here</a>.");

    public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode) =>
        _emailSender.SendEmailAsync(email, "Reset your password", $"Please reset your password using the following code: {resetCode}");
}
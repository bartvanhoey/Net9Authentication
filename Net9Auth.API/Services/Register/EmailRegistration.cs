using Microsoft.AspNetCore.Identity;
using Net9Auth.API.Models;
using Net9Auth.API.Services.Email;

namespace Net9Auth.API.Services.Register;

public static class EmailSetupRegistration
{
    public static void SetupEmailClient(this WebApplicationBuilder builder)
    {
        // if (builder.Configuration.GetRequiredSection("ASPNETCORE_ENVIRONMENT").Value == "Production")
        //     builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityProductionEmailSender>();
        // else
        builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityDevelopmentEmailSender>();

        
        // builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityProductionEmailSender>();
    }
}
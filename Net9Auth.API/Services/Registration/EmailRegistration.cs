﻿using Microsoft.AspNetCore.Identity;
using Net9Auth.API.Models;
using Net9Auth.API.Services.Email;

namespace Net9Auth.API.Services.Registration;

public static class EmailSetupRegistration
{
    public static void SetupEmailClient(this WebApplicationBuilder builder)
    {
        // if (builder.Configuration.GetRequiredSection("ASPNETCORE_ENVIRONMENT").Value == "Production")
        //     builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityProductionEmailSender>();
        // else
        // builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityDevelopmentEmailSender>();

        // TODO set up correctly in production
        builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityProductionEmailSender>();
    }
}
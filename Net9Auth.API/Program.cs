using System.Security.Claims;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Net9Auth.API.Database;
using Net9Auth.API.Models;
using Net9Auth.API.Services.Register;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

try
{
    builder.Configuration
        .AddJsonFile("appsettings.json", false, true)
        .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", false, true);

    builder.SetupSerilog();
    Log.Information("Starting the web host");
    
    builder.Services.AddControllers();

    builder.Services.AddOpenApi();

    builder.RegisterDatabase();

    builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddSignInManager()
        .AddDefaultTokenProviders();

    builder.SetupEmailClient();
    builder.AddCorsPolicy();
    builder.RegisterJwtAuthentication();
    
    Log.Information("Services registered");
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}






var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
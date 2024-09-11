using Microsoft.AspNetCore.Identity;
using Net9Auth.API.Database;
using Net9Auth.API.Infrastructure.ApiKeys;
using Net9Auth.API.Infrastructure.RateLimiting;
using Net9Auth.API.Models;
using Net9Auth.API.Services.Register;
using Net9Auth.API.Services.Registration;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

try
{
    builder.Configuration
        .AddJsonFile("appsettings.json", false, true)
        .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", false, true);

    builder.SetupSerilog();
    Log.Information("Starting the web host");
    
    // builder.Services.AddControllers(x => x.Filters.Add<StaticApiKeyWeatherForecastAuthorizationFilter>());
    builder.Services.AddControllers();

    builder.Services.AddOpenApi();

    builder.SetupSwagger();
    builder.SetupDatabase();

    builder.Services.AddAutoMapper(typeof(Program));
    
    builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddSignInManager()
        .AddDefaultTokenProviders();

    builder.SetupEmailClient();
    builder.AddCorsPolicy();
    
    builder.RegisterServices();
    
    builder.SetupJwtAuthentication();

    builder.Services.SetupApiKeyFiltering();
    builder.SetupRateLimiting();
    
    Log.Information("Services registered");
}
catch (Exception exception)
{
    Log.Fatal(exception, "host terminated unexpectedly");
}

var app = builder.Build();

app.UseSerilogRequestLogging();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseMiddleware<ApiKeyAuthMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseCors("CorsPolicy");

app.UseRateLimiter();

app.Run();
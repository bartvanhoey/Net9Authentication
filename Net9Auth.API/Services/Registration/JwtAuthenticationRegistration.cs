global using static System.String;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using static System.Console;
using static System.Text.Encoding;
using static System.Threading.Tasks.Task;
using static Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults;
using ILogger = Serilog.ILogger;

// using ILogger = Serilog.ILogger;
// ReSharper disable TemplateIsNotCompileTimeConstantProblem

namespace Net9Auth.API.Services.Registration;

public static class JwtAuthenticationRegistration
{
    public static void SetupJwtAuthentication(this WebApplicationBuilder builder)
    {
        var validAudiences = builder.Configuration.GetSection("Jwt:ValidAudiences").Get<List<string>>()
                             ?? throw new InvalidOperationException("'Audience' not found.");

        var validIssuer = builder.Configuration["Jwt:ValidIssuer"]
                          ?? throw new InvalidOperationException("'Issuer' not found.");

        var securityKey = builder.Configuration["Jwt:SecurityKey"]
                          ?? throw new InvalidOperationException("'SecurityKey' not found.");

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = AuthenticationScheme;
            options.DefaultChallengeScheme = AuthenticationScheme;
            options.DefaultScheme = AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudiences = validAudiences,
                ValidIssuer = validIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(UTF8.GetBytes(securityKey)),
                ClockSkew = new TimeSpan(0, 0, 5)
            };
            options.Events = new JwtBearerEvents
            {
                OnChallenge = ctx => LogAttempt(ctx.Request.Headers, "OnChallenge: 401 NotAuthorized", Log.Logger),
                OnTokenValidated = ctx => LogAttempt(ctx.Request.Headers, "OnTokenValidated: Authorized", Log.Logger)
            };
        });
    }
    
    static Task LogAttempt(IHeaderDictionary headers, string eventType, ILogger logger)
    {
        var authorizationHeader = headers.Authorization.FirstOrDefault();
        if (authorizationHeader is null)
        {
            Out.WriteLine($"{eventType}. AccessToken not present");
            // logger.Information($"{eventType}. AccessToken not present");
        }
        else
        {
            var jwtString = authorizationHeader.Substring("Bearer ".Length);
            var jwt = new JwtSecurityToken(jwtString);

             logger.Information(
                $"{eventType}. Expiration: {jwt.ValidTo.ToLongTimeString()}. System time: {DateTime.UtcNow.ToLongTimeString()}");
            Out.WriteLine(
                $"{eventType}. Expiration: {jwt.ValidTo.ToLongTimeString()}. System time: {DateTime.UtcNow.ToLongTimeString()}");
        }

        return CompletedTask;
    }
}
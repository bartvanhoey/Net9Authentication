using static System.Console;

namespace Net9Auth.API.Services.Register;

public static class CorsRegistration
{
    public static void AddCorsPolicy(this WebApplicationBuilder builder)
    {
        var validAudiences = builder.Configuration.GetSection("Jwt:ValidAudiences").Get<List<string>>()
                             ?? throw new InvalidOperationException("'Audience' not found.");

        foreach (var validAudience in validAudiences) WriteLine($"Cors validAudience: {validAudience}");

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", policy =>
            {
                policy
                    .WithOrigins(validAudiences.ToArray())
                    .SetIsOriginAllowed(_ => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();
            });
        });
    }
}
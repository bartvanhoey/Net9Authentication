using Microsoft.OpenApi.Models;

namespace Net9Auth.API.Services.Register;

public static class SwaggerRegistration
{
    public static void RegisterSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter 'Bearer [access-token] in the Value input field'",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            var scheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };
            options.AddSecurityRequirement(new OpenApiSecurityRequirement { { scheme, Array.Empty<string>() } });
        });

    }
}
using Microsoft.OpenApi.Models;

namespace Net9Auth.API.Services.Registration;

public static class SwaggerRegistration
{
    public static void SetupSwagger(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter 'Bearer [access-token] in the Value input field'",
                Name = "bearer access-token",
                Type = SecuritySchemeType.ApiKey
            });
            
            options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
            {
                Description = "The 'API Key to access the API'",
                Type = SecuritySchemeType.ApiKey,
                Name = "x-api-key",
                In = ParameterLocation.Header,
                Scheme = "ApiKeyScheme"
            });

            var bearerScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };
            
            var apiKeyScheme = new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            };
            options.AddSecurityRequirement(new OpenApiSecurityRequirement { { bearerScheme, Array.Empty<string>() }, {apiKeyScheme, Array.Empty<string>()} });
        });

    }
}
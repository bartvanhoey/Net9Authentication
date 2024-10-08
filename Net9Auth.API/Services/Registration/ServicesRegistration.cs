﻿using Net9Auth.API.Services.AggregateLogging.ExceptionLogging;
using Net9Auth.API.Services.ApiKeyService;

namespace Net9Auth.API.Services.Registration;

public static class ServicesRegistration
{
    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IApiKeyApiService, ApiKeyApiService>();
        builder.Services.AddScoped<IExceptionLogApiService, ExceptionLogApiService>();



    }
}
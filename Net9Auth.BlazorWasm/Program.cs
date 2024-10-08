using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Net9Auth.BlazorWasm;
using Net9Auth.BlazorWasm.Services.Administration.AggregatedLogging.ExceptionLogging;
using Net9Auth.BlazorWasm.Services.Administration.ApiKeys;
using Net9Auth.BlazorWasm.Services.Authentication;
using Net9Auth.BlazorWasm.Services.Authentication.Infra;
using Net9Auth.BlazorWasm.Services.Clipboard;
using Net9Auth.BlazorWasm.Services.Logging;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped<IdentityRedirectManager>();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddAuthorizationCore();

var serverBaseAddress = builder.Configuration["ServerUrl"] ?? "";
builder.Services.AddTransient<CustomAuthenticationHandler>();
builder.Services.AddHttpClient("ServerAPI",
        client =>
        {
            client.BaseAddress = new Uri(serverBaseAddress);
        })
    .AddHttpMessageHandler<CustomAuthenticationHandler>();

builder.Services.AddScoped<ISerilogService, SerilogService>();
builder.Services.RegisterAuthenticationServices();
builder.Services.AddScoped<IClipboardService, ClipboardService>();
builder.Services.AddScoped<IApiKeyService, ApiKeyService>();
builder.Services.AddScoped<IExceptionLogService, ExceptionLogService>();


await builder.Build().RunAsync();
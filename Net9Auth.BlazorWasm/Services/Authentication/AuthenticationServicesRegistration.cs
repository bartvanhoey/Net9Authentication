using Net9Auth.BlazorWasm.Services.Authentication.ChangeEmail;
using Net9Auth.BlazorWasm.Services.Authentication.ChangePassword;
using Net9Auth.BlazorWasm.Services.Authentication.ConfirmEmail;
using Net9Auth.BlazorWasm.Services.Authentication.ForgotPassword;
using Net9Auth.BlazorWasm.Services.Authentication.Infra;
using Net9Auth.BlazorWasm.Services.Authentication.Login;
using Net9Auth.BlazorWasm.Services.Authentication.Logout;
using Net9Auth.BlazorWasm.Services.Authentication.Profile;
using Net9Auth.BlazorWasm.Services.Authentication.Register;
using Net9Auth.BlazorWasm.Services.Authentication.ResendEmailConfirmation;
using Net9Auth.BlazorWasm.Services.Authentication.ResetPassword;
using Net9Auth.BlazorWasm.Services.Authentication.Token;

namespace Net9Auth.BlazorWasm.Services.Authentication;

public static class AuthenticationServicesRegistration
{
    public static void RegisterAuthenticationServices(this IServiceCollection services)
    {
        services.AddScoped<IIdentityAccessor, IdentityAccessor>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();
        services.AddScoped<ILoginService, LoginService>();
        services.AddScoped<IRegisterService, RegisterService>();
        services.AddScoped<IConfirmEmailService, ConfirmEmailService>();
        services.AddScoped<IResendEmailConfirmationService, ResendEmailConfirmationService>();
        services.AddScoped<ILogoutService, LogoutService>();
        services.AddScoped<IForgotPasswordService, ForgotPasswordService>();
        services.AddScoped<IResetPasswordService, ResetPasswordService>();
        services.AddScoped<IProfileService, ProfileService>();
        services.AddScoped<IUserHasPasswordService, UserHasPasswordService>();
        services.AddScoped<IChangePasswordService, ChangePasswordService>();
        services.AddScoped<IChangeEmailService, ChangeEmailService>();
    }
}
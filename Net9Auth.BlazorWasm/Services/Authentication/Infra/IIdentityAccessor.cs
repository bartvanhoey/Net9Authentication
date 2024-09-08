namespace Net9Auth.BlazorWasm.Services.Authentication.Infra;

public interface IIdentityAccessor
{
    Task<string?> GetUserNameAsync();
    Task<string?> GetUserIdAsync();
}
using Net9Auth.BlazorWasm.Services.Authentication.Profile.Models;
using Net9Auth.Shared.Models.Authentication.SetPhoneNumber;

namespace Net9Auth.BlazorWasm.Services.Authentication.Profile;

public interface IProfileService
{
    public Task<ProfileResult?> GetProfileAsync();
    Task<AuthSetPhoneNumberResult> SetPhoneNumberAsync(SetPhoneNumberInputModel model);
}
namespace Net9Auth.BlazorWasm.Services.Authentication.Refresh.Models;

public enum AuthRefreshMessage
{
    Successful = 0,
    UnSuccessful = 1,
    ContentIsNull = 2,
    AccessTokenNull = 3,
    RefreshTokenNull = 4,
    AccessTokenInvalid = 5,
}
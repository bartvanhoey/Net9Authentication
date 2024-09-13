using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Net9Auth.Shared.Infrastructure.Extensions;
using Net9Auth.Shared.Infrastructure.Functional;
using Net9Auth.Shared.Infrastructure.Functional.Errors;
using static Net9Auth.Shared.Infrastructure.Functional.Result;

namespace Net9Auth.API.Controllers.Authentication.Base;

public static class HttpRequestExtensions
{
    private static string GetAccessTokenFromRequest(this HttpRequest request)
    {
        var authorizationHeader = request.Headers.Authorization.FirstOrDefault();
        if (authorizationHeader != null && authorizationHeader.StartsWith("Bearer "))
            return authorizationHeader["Bearer ".Length..].Trim();

        return string.Empty;
    }

    public static Result<string> GetEmailAddress(this HttpRequest request)
    {
        try
        {
            var token = GetAccessTokenFromRequest(request);
            if (token.IsNullOrWhiteSpace()) return Fail<string>(new BasicResultError("accessToken is null"));
            var email = new JwtSecurityTokenHandler().ReadJwtToken(token).Claims
                .First(claim => claim.Type == ClaimTypes.Name);
            if (email == null || email.Value.IsNullOrWhiteSpace())
                return Fail<string>(new BasicResultError("email address is null"));
            return email.Value.IsValidEmailAddress()
                ? Ok(email.Value)
                : Fail<string>(new BasicResultError("email address is invalid"));
        }
        catch (Exception exception)
        {
            return Fail<string>(new BasicResultError(exception.Message));
        }
    }

    public static Result<string> GetUserId(this HttpRequest request)
    {
        try
        {
            var accessToken = request.GetAccessTokenFromRequest();
            if (accessToken.IsNullOrWhiteSpace()) return Fail<string>(new BasicResultError("accessToken is null"));
            var nameId = new JwtSecurityTokenHandler().ReadJwtToken(accessToken).Claims
                .First(claim => claim.Type == ClaimTypes.NameIdentifier);
            if (nameId == null || nameId.Value.IsNullOrWhiteSpace())
                return Fail<string>(new BasicResultError("userId is null"));
            return Ok(nameId.Value);
        }
        catch (Exception exception)
        {
            return Fail<string>(new BasicResultError(exception.Message));
        }
    }
}
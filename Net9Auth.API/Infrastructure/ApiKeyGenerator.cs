using static System.Security.Cryptography.RandomNumberGenerator;

namespace Net9Auth.API.Infrastructure;

public static class ApiKeyGenerator
{
    public static string GenerateApiKey(int byteLength = 64)
    {
        var buffer = byteLength > 4096
            ? new byte[byteLength]
            : stackalloc byte[byteLength];
        Fill(buffer);
        return Convert.ToHexString(buffer);
    }
}
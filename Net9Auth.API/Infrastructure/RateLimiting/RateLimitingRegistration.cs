using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

namespace Net9Auth.API.Infrastructure.RateLimiting;

public static class RateLimitingRegistration
{
    public static void SetupRateLimiting(this IServiceCollection services)
    {
        services.AddRateLimiter(opts =>
        {
            opts.AddFixedWindowLimiter("fixed", opts =>
            {
                opts.PermitLimit = 20;
                opts.Window = TimeSpan.FromSeconds(10);
                opts.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                opts.QueueLimit = 10;
            });

        });

    }
}
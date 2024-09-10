using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

namespace Net9Auth.API.Infrastructure.RateLimiting;

public static class RateLimitingRegistration
{
    public static void SetupRateLimiting(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<MyRateLimitOptions>(
            builder.Configuration.GetSection(MyRateLimitOptions.MyRateLimit));

        var myOptions = new MyRateLimitOptions();
        builder.Configuration.GetSection(MyRateLimitOptions.MyRateLimit).Bind(myOptions);
        const string fixedPolicy = "fixed";

        builder.Services.AddRateLimiter(x => x
            .AddFixedWindowLimiter(policyName: fixedPolicy, options =>
            {
                options.PermitLimit = myOptions.PermitLimit;
                options.Window = TimeSpan.FromSeconds(myOptions.Window);
                options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                options.QueueLimit = myOptions.QueueLimit;
            }));
    }
}
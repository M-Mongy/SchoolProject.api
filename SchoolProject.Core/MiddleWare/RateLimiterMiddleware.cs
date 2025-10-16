using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;

namespace SchoolProject.Core.Middleware
{
    public static class RateLimiterMiddleware
    {
        public static IServiceCollection AddCustomRateLimiter(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                // Global rate limiter - per user or IP
                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(context =>
                {
                    var userIdentifier = context.User.Identity?.Name
                                         ?? context.Connection.RemoteIpAddress?.ToString()
                                         ?? "anonymous";

                    return RateLimitPartition.GetFixedWindowLimiter(userIdentifier, _ => new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 3, // allowed requests
                        Window = TimeSpan.FromSeconds(5), // per 10 seconds
                        QueueLimit = 0,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                    });
                });

                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                // Optional: Custom response when rate limit exceeded
                options.OnRejected = async (context, cancellationToken) =>
                {
                    context.HttpContext.Response.ContentType = "application/json";
                    await context.HttpContext.Response.WriteAsync(
                        "{\"message\":\"Too many requests, please try again later.\"}", cancellationToken);
                };
            });

            return services;
        }

        public static IApplicationBuilder UseCustomRateLimiter(this IApplicationBuilder app)
        {
            app.UseRateLimiter();
            return app;
        }
    }
}

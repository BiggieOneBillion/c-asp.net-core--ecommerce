using System.Collections.Concurrent;
using System.Net;

namespace Ecommerce.API.Middleware;

public class RateLimitingMiddleware
{
    private readonly RequestDelegate _next;
    private static readonly ConcurrentDictionary<string, (int Count, DateTime WindowStart)> Clients = new();
    private const int Limit = 10; // Requests per minute
    private static readonly TimeSpan Window = TimeSpan.FromMinutes(1);

    public RateLimitingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/auth"))
        {
            var ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            var key = $"{ip}:{context.Request.Path}";

            var now = DateTime.UtcNow;
            var clientInfo = Clients.GetOrAdd(key, _ => (0, now));

            if (now - clientInfo.WindowStart > Window)
            {
                clientInfo = (1, now);
            }
            else
            {
                clientInfo.Count++;
            }

            Clients[key] = clientInfo;

            if (clientInfo.Count > Limit)
            {
                context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                await context.Response.WriteAsync("Rate limit exceeded. Please try again later.");
                return;
            }
        }

        await _next(context);
    }
}

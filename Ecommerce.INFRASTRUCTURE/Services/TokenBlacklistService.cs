using Ecommerce.APPLICATION.Common.Interfaces;
using System.Collections.Concurrent;

namespace Ecommerce.INFRASTRUCTURE.Services;

public class TokenBlacklistService : ITokenBlacklistService
{
    // Placeholder implementation using ConcurrentDictionary
    // In production, this should use Redis for distributed blacklisting
    private static readonly ConcurrentDictionary<string, DateTime> BlacklistedTokens = new();

    public Task BlacklistTokenAsync(string token, TimeSpan expiry)
    {
        BlacklistedTokens.TryAdd(token, DateTime.UtcNow.Add(expiry));
        return Task.CompletedTask;
    }

    public Task<bool> IsTokenBlacklistedAsync(string token)
    {
        if (BlacklistedTokens.TryGetValue(token, out var expiry))
        {
            if (DateTime.UtcNow > expiry)
            {
                BlacklistedTokens.TryRemove(token, out _);
                return Task.FromResult(false);
            }
            return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }
}

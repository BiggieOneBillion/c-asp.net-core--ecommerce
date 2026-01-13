using Ecommerce.APPLICATION.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace Ecommerce.INFRASTRUCTURE.Services;

public class MockEmailService : IEmailService
{
    private readonly ILogger<MockEmailService> _logger;

    public MockEmailService(ILogger<MockEmailService> logger)
    {
        _logger = logger;
    }

    public Task SendVerificationEmailAsync(string email, string name, string token)
    {
        _logger.LogInformation("Verification email sent to {Email} for {Name}. Token: {Token}", email, name, token);
        return Task.CompletedTask;
    }

    public Task SendPasswordResetEmailAsync(string email, string name, string token)
    {
        _logger.LogInformation("Password reset email sent to {Email} for {Name}. Token: {Token}", email, name, token);
        return Task.CompletedTask;
    }

    public Task SendLoginNotificationEmailAsync(string email, string name, string deviceInfo)
    {
        _logger.LogInformation("Login notification sent to {Email} for {Name}. Device: {DeviceInfo}", email, name, deviceInfo);
        return Task.CompletedTask;
    }
}

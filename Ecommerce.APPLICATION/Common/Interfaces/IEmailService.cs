namespace Ecommerce.APPLICATION.Common.Interfaces;

public interface IEmailService
{
    Task SendVerificationEmailAsync(string email, string name, string token);
    Task SendPasswordResetEmailAsync(string email, string name, string token);
    Task SendLoginNotificationEmailAsync(string email, string name, string deviceInfo);
}

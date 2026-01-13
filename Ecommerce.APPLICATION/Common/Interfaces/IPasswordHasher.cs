namespace Ecommerce.APPLICATION.Common.Interfaces;

public interface IPasswordHashers
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string passwordHash);
}

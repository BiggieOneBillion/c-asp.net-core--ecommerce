
using BCrypt.Net;
using Ecommerce.APPLICATION.Common.Interfaces;

public class BCryptPasswordHasher : IPasswordHashers
{
    private const int CostFactor = 12;

    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password, CostFactor);
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash);
    }
}

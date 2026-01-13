using Ecommerce.APPLICATION.Common.Interfaces;
using BCrypt.Net;

namespace Ecommerce.INFRASTRUCTURE.Services;

public class BCryptPasswordHasher : IPasswordHasher
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

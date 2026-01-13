using Ecommerce.CORE.Entity;

namespace Ecommerce.APPLICATION.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateAccessToken(Users user);
    string GenerateRefreshToken();
}

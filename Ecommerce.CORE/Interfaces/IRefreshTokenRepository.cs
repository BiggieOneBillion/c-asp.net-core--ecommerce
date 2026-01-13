using Ecommerce.CORE.Entity;
using Ecommerce.CORE.ValueObjects;

namespace Ecommerce.CORE.Interfaces;

public interface IRefreshTokenRepository : IRepository<RefreshToken>
{
    Task<RefreshToken?> GetByTokenAsync(string token);
    Task<List<RefreshToken>> GetByUserIdAsync(UserId userId);
    Task<List<RefreshToken>> GetByFamilyIdAsync(string familyId);
    Task RevokeFamilyAsync(string familyId, string revokedByIp);
}

using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.CORE.ValueObjects;
using Ecommerce.INFRASTRUCTURE.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.INFRASTRUCTURE.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly ApplicationDbContext _context;

    public RefreshTokenRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<RefreshToken?> GetByIdAsync(Guid id)
    {
        return await _context.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<RefreshToken>> GetAllAsync()
    {
        return await _context.RefreshTokens.ToListAsync();
    }

    public async Task AddAsync(RefreshToken entity)
    {
        await _context.RefreshTokens.AddAsync(entity);
    }

    public void Update(RefreshToken entity)
    {
        _context.RefreshTokens.Update(entity);
    }

    public void Delete(RefreshToken entity)
    {
        _context.RefreshTokens.Remove(entity);
    }

    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        return await _context.RefreshTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Token == token);
    }

    public async Task<List<RefreshToken>> GetByUserIdAsync(UserId userId)
    {
        return await _context.RefreshTokens
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }

    public async Task<List<RefreshToken>> GetByFamilyIdAsync(string familyId)
    {
        return await _context.RefreshTokens
            .Where(x => x.FamilyId == familyId)
            .ToListAsync();
    }

    public async Task RevokeFamilyAsync(string familyId, string revokedByIp)
    {
        var familyTokens = await GetByFamilyIdAsync(familyId);
        foreach (var token in familyTokens)
        {
            if (token.IsActive)
            {
                token.IsRevoked = true;
                token.Revoked = DateTime.UtcNow;
                token.RevokedByIp = revokedByIp;
            }
        }
    }
}

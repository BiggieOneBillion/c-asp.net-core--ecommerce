using System;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;
using Ecommerce.INFRASTRUCTURE.Data;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.INFRASTRUCTURE.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateAsync(Users entity)
    {
        await _context.Users.AddAsync(entity);
    }

    public async Task DeleteAsync(Users entity)
    {
        _context.Users.Remove(entity);
        await Task.CompletedTask;
    }

    public async Task<Users?> GetByIdAsync(Guid id)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id.Id == id);
    }

    public async Task<Users?> GetUserByEmailAsync(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task UpdateAsync(Users entity)
    {
        _context.Users.Update(entity);
        await Task.CompletedTask;
    }

    public async Task AddAsync(Users entity)
    {
        await CreateAsync(entity);
    }

    public async Task<Users?> GetUserByVerificationTokenAsync(string token)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.EmailVerificationToken == token);
    }

    public async Task<Users?> GetUserByPasswordResetTokenAsync(string token)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == token);
    }
}

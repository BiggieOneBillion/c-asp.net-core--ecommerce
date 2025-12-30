using System;
using Ecommerce.CORE.Entity;
using Ecommerce.CORE.Interfaces;

namespace Ecommerce.INFRASTRUCTURE.Repositories;

public class UserRepository : IUserRepository
{
    public Task CreateAsync(Users entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Users entity)
    {
        throw new NotImplementedException();
    }

    public Task<Users?> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Users?> GetUserByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Users entity)
    {
        throw new NotImplementedException();
    }
}

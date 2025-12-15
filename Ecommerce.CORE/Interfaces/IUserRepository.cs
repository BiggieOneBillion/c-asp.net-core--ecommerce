using System;
using Ecommerce.CORE.Entity;

namespace Ecommerce.CORE.Interfaces;

public interface IUserRepository : IRepository<Users>
{
    Task<Users?> GetUserByEmailAsync(string email);

    
}

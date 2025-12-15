using System;

namespace Ecommerce.CORE.Interfaces;

public interface IRepository<T> where T : class
{
    // here we would define common repository methods like Add, Get, Update, Delete etc.
    Task AddAsync(T entity);

    Task<T?> GetByIdAsync(Guid id);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);

}

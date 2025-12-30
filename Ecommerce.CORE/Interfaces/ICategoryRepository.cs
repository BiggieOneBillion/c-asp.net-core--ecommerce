using System;
using Ecommerce.CORE.Entity;

namespace Ecommerce.CORE.Interfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Task <List<Category>> GetAllAsync();
}

using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.CORE.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

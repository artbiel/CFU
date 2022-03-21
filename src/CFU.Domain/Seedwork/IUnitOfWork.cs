using System.Threading;

namespace CFU.Domain.Seedwork;

public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

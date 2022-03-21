using System.Threading;

namespace CFU.Domain.Seedwork;

public interface IRepository<T, TId> where T : IAggregateRoot<TId> where TId : struct
{
    public IUnitOfWork UnitOfWork { get; }

    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<IEnumerable<T>> GetAllAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
    Task<T> GetAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);
    Task<T> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
    Task<bool> ContainsAsync(ISpecification<T> specification, CancellationToken cancellationToken = default);

    ValueTask Add(T aggregate);
    ValueTask Update(T aggregate);
}

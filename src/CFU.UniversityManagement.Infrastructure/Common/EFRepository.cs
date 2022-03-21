namespace CFU.UniversityManagement.Infrastructure.Common;

public class EFRepository<T, TId> : IRepository<T, TId> where T : Entity<TId>, IAggregateRoot<TId> where TId : struct
{
    private readonly UniversityManagementDBContext _context;

    public EFRepository(UniversityManagementDBContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default) =>
      await _context.Set<T>()
          .ToArrayAsync(cancellationToken);

    public async Task<IEnumerable<T>> GetAllAsync(ISpecification<T> specification, CancellationToken cancellationToken = default) =>
        await _context.Set<T>()
            .Where(specification.ToExpression())
            .ToArrayAsync(cancellationToken);

    public Task<T> GetAsync(ISpecification<T> specification, CancellationToken cancellationToken = default) =>
        _context.Set<T>()
            .Where(specification.ToExpression())
            .SingleOrDefaultAsync(cancellationToken)!;

    public Task<T> GetByIdAsync(TId id, CancellationToken cancellationToken = default) =>
        _context.Set<T>()
            .Where(b => b.Id.Equals(id))
            .SingleOrDefaultAsync(cancellationToken)!;

    public ValueTask Add(T aggregate)
    {
        _context.Set<T>().Add(aggregate);
        return ValueTask.CompletedTask;
    }

    public ValueTask Update(T aggregate)
    {
        _context.Set<T>().Update(aggregate);
        return ValueTask.CompletedTask;
    }

    public void Remove(T aggregate) => _context.Set<T>().Remove(aggregate);

    public Task<bool> ContainsAsync(ISpecification<T> specification, CancellationToken cancellationToken = default) =>
         _context.Set<T>()
             .AnyAsync(specification.ToExpression(), cancellationToken);
}

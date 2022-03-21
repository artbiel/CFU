using CFU.Domain.SupplyContext.BuildingAggregate;
using CFU.UniversityManagement.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using Academy = CFU.Domain.StructureContext.AcademyAggregate.Academy;
using Department = CFU.Domain.StructureContext.DepartmentAggregate.Department;
using Faculty = CFU.Domain.StructureContext.FacultyAggregate.Faculty;
using Institute = CFU.Domain.StructureContext.InstituteAggregate.Institute;

namespace CFU.UniversityManagement.Infrastructure;

public class UniversityManagementDBContext : DbContext, IUnitOfWork
{
    private readonly IMediator _mediator = default!;
    private IDbContextTransaction _currentTransaction = default!;

    public UniversityManagementDBContext(DbContextOptions<UniversityManagementDBContext> options, IMediator mediator) : base(options)
    {
        _mediator = mediator;
    }

    public DbSet<Academy> Academies { get; set; }
    public DbSet<Institute> Institutes { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    public DbSet<Building> Buildings { get; set; }
    public DbSet<Department> Departments { get; set; }

    public UniversityManagementDBContext(DbContextOptions<UniversityManagementDBContext> options) : base(options) { }

    public IDbContextTransaction GetCurrentTransaction() => _currentTransaction;

    public bool HasActiveTransaction => _currentTransaction != null;

    protected override void OnModelCreating(ModelBuilder modelBuilder) => 
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UniversityManagementDBContext).Assembly);

    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        await _mediator.DispatchDomainEventsAsync(this);

        await base.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
    {
        if (_currentTransaction is not null) return default!;

        _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken);

        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction, CancellationToken cancellationToken)
    {
        if (transaction is null) throw new ArgumentNullException(nameof(transaction));
        if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

        try {
            await SaveChangesAsync(cancellationToken);
            transaction.Commit();
        }
        catch {
            RollbackTransaction();
            throw;
        }
        finally {
            if (_currentTransaction is not null) {
                _currentTransaction.Dispose();
                _currentTransaction = null!;
            }
        }
    }

    public void RollbackTransaction()
    {
        try {
            _currentTransaction?.Rollback();
        }
        finally {
            if (_currentTransaction is not null) {
                _currentTransaction.Dispose();
                _currentTransaction = null!;
            }
        }
    }
}

using System.Threading;

namespace CFU.Domain.HRContext.DepartmentAggregate;

public interface IDepartmentRepository
{
    Task<IEnumerable<Department>> GetByIdAsync(DepartmentId id, CancellationToken cancellationToken = default);
}

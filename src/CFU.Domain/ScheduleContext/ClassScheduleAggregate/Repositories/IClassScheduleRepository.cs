using System.Threading;

namespace CFU.Domain.ScheduleContext.ClassScheduleAggregate;

public interface IClassScheduleRepository : IRepository<ClassSchedule, ClassScheduleId>
{
    Task<IEnumerable<ClassSchedule>> GetAllAsync(ISpecification<Pair> specification, CancellationToken cancellationToken = default);
    void Remove(ISpecification<ClassSchedule> specification);
}

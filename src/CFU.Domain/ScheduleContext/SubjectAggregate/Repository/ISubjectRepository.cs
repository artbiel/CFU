using System.Threading;

namespace CFU.Domain.ScheduleContext.SubjectAggregate.Repository;

public interface ISubjectRepository
{
    public Task<IEnumerable<Subject>> GetAllByIdsAsync(IEnumerable<SubjectId> subjectId, CancellationToken cancellationToken);
}

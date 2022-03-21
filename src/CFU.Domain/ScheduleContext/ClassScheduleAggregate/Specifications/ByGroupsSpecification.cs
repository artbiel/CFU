using System.Linq.Expressions;

namespace CFU.Domain.ScheduleContext.ClassScheduleAggregate.Specifications;

public record ByGroupsSpecification : Specification<ClassSchedule>
{
    private readonly IEnumerable<GroupId> _groups;

    public ByGroupsSpecification(IEnumerable<GroupId> groups)
    {
        _groups = groups;
    }

    public override Expression<Func<ClassSchedule, bool>> ToExpression() =>
        classSchedule => _groups.Contains(classSchedule.GroupId);
}

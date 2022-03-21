using System.Linq.Expressions;

namespace CFU.Domain.ScheduleContext.ClassScheduleAggregate.Specifications;

public record ByGroupSpecification : Specification<ClassSchedule>
{
    private readonly GroupId _groupId;

    public ByGroupSpecification(GroupId groupId)
    {
        _groupId = groupId;
    }

    public override Expression<Func<ClassSchedule, bool>> ToExpression() =>
        classSchedule => classSchedule.GroupId == _groupId;
}

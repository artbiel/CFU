using System.Linq.Expressions;

namespace CFU.Domain.ScheduleContext.ClassScheduleAggregate;

public record UniqueClassScheduleSpecification : Specification<ClassSchedule>
{
    private readonly GroupId _groupId;

    public UniqueClassScheduleSpecification(GroupId groupId) => _groupId = groupId;

    public override Expression<Func<ClassSchedule, bool>> ToExpression() =>
        cs => cs.GroupId == _groupId;

}

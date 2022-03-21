using System.Linq.Expressions;

namespace CFU.Domain.ScheduleContext.PairsTimetableAggregate;

public record UniquePairsTimetableSpecification : Specification<PairsTimetable>
{
    private readonly string _title;

    public UniquePairsTimetableSpecification(string title) => _title = title;

    public override Expression<Func<PairsTimetable, bool>> ToExpression() =>
        timetable => timetable.Title == _title;
}

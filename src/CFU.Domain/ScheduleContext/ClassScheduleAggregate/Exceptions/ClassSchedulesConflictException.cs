namespace CFU.Domain.ScheduleContext.ClassScheduleAggregate;

public class ClassSchedulesConflictException : DomainException
{
    public ClassSchedulesConflictException(IEnumerable<ClassSchedule> classSchedules)
        : base("There are conflicts")
    {
        ClassSchedules = classSchedules;
    }

    public IEnumerable<ClassSchedule> ClassSchedules { get; private init; }
}

namespace CFU.Domain.ScheduleContext.ClassScheduleAggregate;

public class ClassScheduleAlreadyExistsException : DomainException
{
    public ClassScheduleAlreadyExistsException(ClassSchedule classSchedule)
        : base($"Class schedule for group {classSchedule.GroupId} already exist") { }
}

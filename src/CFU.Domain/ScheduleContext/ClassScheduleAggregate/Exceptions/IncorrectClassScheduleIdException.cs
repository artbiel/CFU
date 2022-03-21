namespace CFU.Domain.ScheduleContext.ClassScheduleAggregate;

public class IncorrectClassScheduleIdException : DomainException
{
    public IncorrectClassScheduleIdException(Pair pair, ClassSchedule classSchedule)
        : base($"Pair {pair.Id.Order} at {pair.Id.DayOfWeek} is already added to class schedule '{classSchedule.Id}'") { }
}

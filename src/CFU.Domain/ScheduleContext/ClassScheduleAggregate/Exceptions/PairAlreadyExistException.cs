namespace CFU.Domain.ScheduleContext.ClassScheduleAggregate;

public class PairAlreadyExistException : DomainException
{
    public PairAlreadyExistException(Pair pair) : base($"Pair {pair.Id.Order} at {pair.Id.DayOfWeek} already exist") { }
}

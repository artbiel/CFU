namespace CFU.Domain.ScheduleContext.ClassScheduleAggregate;

public class PairNotFoundException : DomainException
{
    public PairNotFoundException(PairId pairId) : base($"Pair {pairId.Order} at {pairId.DayOfWeek} not found") { }
}

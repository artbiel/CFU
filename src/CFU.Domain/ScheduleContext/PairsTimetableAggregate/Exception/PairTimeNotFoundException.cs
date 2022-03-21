namespace CFU.Domain.ScheduleContext.PairsTimetableAggregate;

public class PairTimeNotFoundException : DomainException
{
    public PairTimeNotFoundException(PairOrder order) : base($"Pair {order} not found") { }
}

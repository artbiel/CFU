namespace CFU.Domain.ScheduleContext.PairsTimetableAggregate;

public class PairTimeAlreadyExistsException : DomainException
{
    public PairTimeAlreadyExistsException(PairTime pairTime)
        : base($"Pair {pairTime.Order} already exists") { }
}

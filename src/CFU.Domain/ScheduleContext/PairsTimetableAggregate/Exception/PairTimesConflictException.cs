namespace CFU.Domain.ScheduleContext.PairsTimetableAggregate;

public class PairTimesConflictException : DomainException
{
    public PairTimesConflictException(PairTime first, PairTime second)
        : base($"Pair {first.Order} ({first.StartTime:HH:mm} - {first.EndTime:HH:mm}) inteesects with pair {second.Order} ({second.StartTime:HH:mm} - {second.EndTime:HH:mm})") { }
}

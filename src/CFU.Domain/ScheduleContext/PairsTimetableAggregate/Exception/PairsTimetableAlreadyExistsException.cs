namespace CFU.Domain.ScheduleContext.PairsTimetableAggregate;

public class PairsTimetableAlreadyExistsException : DomainException
{
    public PairsTimetableAlreadyExistsException(string title)
        : base($"Timetable {title} already exists") { }
}

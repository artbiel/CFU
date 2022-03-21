namespace CFU.Domain.ScheduleContext.PairsTimetableAggregate;

public record PairTimeChangedEvent(PairTime PairTime) : DomainEvent;

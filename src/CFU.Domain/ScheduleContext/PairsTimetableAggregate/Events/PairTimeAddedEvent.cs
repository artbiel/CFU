namespace CFU.Domain.ScheduleContext.PairsTimetableAggregate;

public record PairTimeAddedEvent(PairTime PairTime) : DomainEvent;

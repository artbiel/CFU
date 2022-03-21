namespace CFU.Domain.ScheduleContext.PairsTimetableAggregate;

public record PairTimeRemovedEvent(PairTime PairTime) : DomainEvent;

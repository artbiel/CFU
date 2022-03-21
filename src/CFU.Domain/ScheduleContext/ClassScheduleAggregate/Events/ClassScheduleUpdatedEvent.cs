namespace CFU.Domain.ScheduleContext.ClassScheduleAggregate;

public record ClassScheduleUpdatedEvent(ClassSchedule ClassSchedule) : DomainEvent;

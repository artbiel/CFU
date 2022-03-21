namespace CFU.Domain.ScheduleContext.ClassScheduleAggregate;

public record ClassScheduleCreatedEvent(ClassSchedule ClassSchedule) : DomainEvent;

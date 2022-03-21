namespace CFU.Domain.ScheduleContext.PairsTimetableAggregate;

public record TimetableCreatedEvent(PairsTimetable Timetable) : DomainEvent;

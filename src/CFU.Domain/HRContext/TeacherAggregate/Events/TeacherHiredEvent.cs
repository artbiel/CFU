namespace CFU.Domain.HRContext.TeacherAggregate.Events;

public record TeacherHiredEvent(Teacher Teacher) : DomainEvent;

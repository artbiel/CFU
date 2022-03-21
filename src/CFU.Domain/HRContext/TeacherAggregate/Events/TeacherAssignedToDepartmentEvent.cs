namespace CFU.Domain.HRContext.TeacherAggregate;

public record TeacherAssignedToDepartmentEvent(Teacher Teacher, DepartmentId DepartmentId) : DomainEvent;

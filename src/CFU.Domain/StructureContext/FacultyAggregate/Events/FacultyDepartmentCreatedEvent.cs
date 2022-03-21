namespace CFU.Domain.StructureContext.FacultyAggregate;

public record FacultyDepartmentCreatedEvent(Department Department) : DomainEvent;

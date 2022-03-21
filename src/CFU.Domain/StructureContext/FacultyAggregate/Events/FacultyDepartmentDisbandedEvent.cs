namespace CFU.Domain.StructureContext.FacultyAggregate;

public record FacultyDepartmentDisbandedEvent(Department Department) : DomainEvent;

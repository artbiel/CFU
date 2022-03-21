namespace CFU.Domain.StructureContext.FacultyAggregate;

public record FacultyDisbandedEvent(Faculty Faculty) : DomainEvent;

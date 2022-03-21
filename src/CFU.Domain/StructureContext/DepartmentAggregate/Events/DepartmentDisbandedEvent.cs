namespace CFU.Domain.StructureContext.DepartmentAggregate;

public record DepartmentDisbandedEvent(Department Department) : DomainEvent;

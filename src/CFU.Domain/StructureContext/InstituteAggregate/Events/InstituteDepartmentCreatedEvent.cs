namespace CFU.Domain.StructureContext.InstituteAggregate;

public record InstituteDepartmentCreatedEvent(Department Department) : DomainEvent;

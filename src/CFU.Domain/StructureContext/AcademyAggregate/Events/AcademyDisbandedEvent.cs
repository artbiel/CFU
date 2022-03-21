namespace CFU.Domain.StructureContext.AcademyAggregate;

public record AcademyDisbandedEvent(Academy Academy) : DomainEvent;

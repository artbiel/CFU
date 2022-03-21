namespace CFU.Domain.StructureContext.InstituteAggregate;

public record InstituteDisbandedEvent(Institute Institute) : DomainEvent;

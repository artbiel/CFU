namespace CFU.Domain.SupplyContext.BuildingAggregate;

public record BuildingCreatedEvent(Building Building) : DomainEvent;

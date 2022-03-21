namespace CFU.Domain.SupplyContext.BuildingAggregate;

public record AuditoriumAddedEvent(Auditorium Auditorium) : DomainEvent;

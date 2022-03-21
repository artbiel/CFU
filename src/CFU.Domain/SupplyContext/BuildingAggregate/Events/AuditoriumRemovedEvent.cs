namespace CFU.Domain.SupplyContext.BuildingAggregate;

public record AuditoriumRemovedEvent(AuditoriumNumber Auditorium) : DomainEvent;

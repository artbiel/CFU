namespace CFU.Domain.SupplyContext.BuildingAggregate;

public record AuditoriumTypeChangedEvent(Auditorium OldAuditorium, Auditorium NewAuditorium, AuditoriumType NewType) : DomainEvent;

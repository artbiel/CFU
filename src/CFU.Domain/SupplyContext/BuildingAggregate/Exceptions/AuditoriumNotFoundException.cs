namespace CFU.Domain.SupplyContext.BuildingAggregate;

public class AuditoriumNotFoundException : DomainException
{
    public AuditoriumNotFoundException(AuditoriumNumber number) : base($"Auditorium {number} not found") { }
}

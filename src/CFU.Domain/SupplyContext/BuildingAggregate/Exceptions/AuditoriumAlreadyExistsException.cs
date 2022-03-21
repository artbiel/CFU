namespace CFU.Domain.SupplyContext.BuildingAggregate;

public class AuditoriumAlreadyExistsException : DomainException
{
    public AuditoriumAlreadyExistsException(AuditoriumNumber number)
        : base($"Auditorium {number} already exists") { }
}

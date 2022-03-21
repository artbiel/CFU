namespace CFU.Domain.SupplyContext.BuildingAggregate;

public class AuditoriumTypeException : DomainException
{
    public AuditoriumTypeException(AuditoriumNumber number, AuditoriumType auditoriumType)
        : base($"Auditorium {number} sholud be {Enum.GetName(auditoriumType).ToLower()}") { }
}

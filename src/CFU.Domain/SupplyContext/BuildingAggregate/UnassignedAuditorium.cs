namespace CFU.Domain.SupplyContext.BuildingAggregate;

public class UnassignedAuditorium : Auditorium
{
    private UnassignedAuditorium() { }

    internal UnassignedAuditorium(AuditoriumNumber number, Building building) : base(number, building) { }
}

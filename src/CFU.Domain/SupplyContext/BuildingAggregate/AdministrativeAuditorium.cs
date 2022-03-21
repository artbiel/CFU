using CFU.Domain.SupplyContext.StructureUnitAggregate;

namespace CFU.Domain.SupplyContext.BuildingAggregate;

public class AdministrativeAuditorium : Auditorium
{
    private AdministrativeAuditorium() { }

    internal AdministrativeAuditorium(AuditoriumNumber number, Building building, StructureUnitId administrativeUnit) : base(number, building)
    {
        SetAdministrativeUnit(administrativeUnit);
    }

    public StructureUnitId StructureUnitId { get; private set; }

    internal void SetAdministrativeUnit(StructureUnitId administrativeUnit)
    {
        StructureUnitId = Guard.Against.Default(administrativeUnit, nameof(administrativeUnit));
    }
}


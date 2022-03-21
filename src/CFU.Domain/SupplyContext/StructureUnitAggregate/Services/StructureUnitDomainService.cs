using CFU.Domain.SupplyContext.BuildingAggregate;

namespace CFU.Domain.SupplyContext.StructureUnitAggregate;

public class StructureUnitDomainService
{
    public void AssignToAuditorium(StructureUnit structureUnit, Building building, AuditoriumNumber number)
    {
        Guard.Against.Default(structureUnit, nameof(structureUnit));
        Guard.Against.Default(building, nameof(building));
        Guard.Against.Default(number, nameof(number));
        var auditorium = building.ChangeAuditoriumTypeToAdmitistrative(number, structureUnit.Id);
        structureUnit.AssignToAuditorium(number, building.Id);
    }

    public void UnasssignFromAuditorium(StructureUnit structureUnit, Building building)
    {
        Guard.Against.Default(structureUnit, nameof(structureUnit));
        Guard.Against.Default(building, nameof(building));
        if (building.Id != structureUnit.BuildingId)
            structureUnit.UnasssignFromAuditorium();
        building.UnassingAdministrativeAuditorium(structureUnit.AuditoriumNumber);
    }
}

using CFU.Domain.SupplyContext.BuildingAggregate;

namespace CFU.Domain.SupplyContext.StructureUnitAggregate;

public record struct StructureUnitId(Guid Id);

public class StructureUnit : Entity<StructureUnitId>, IAggregateRoot<StructureUnitId>
{
    internal StructureUnit(StructureUnitId id) : base(id) { }

    public AuditoriumNumber AuditoriumNumber { get; private set; }
    public BuildingId BuildingId { get; private set; }

    public void AssignToAuditorium(AuditoriumNumber number, BuildingId buildingId)
    {
        AuditoriumNumber = Guard.Against.Default(number, nameof(number));
        BuildingId = Guard.Against.Default(buildingId, nameof(buildingId));
    }

    public void UnasssignFromAuditorium()
    {
        if (AuditoriumNumber == default || BuildingId == default)
            throw new StructureUnitAlreadyUnassignedFromAuditoriumException(Id);
        AuditoriumNumber = default;
        BuildingId = default;
    }
}

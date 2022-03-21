namespace CFU.Domain.SupplyContext.BuildingAggregate;

public class BuildingIsAlreadyDecommissionedException : DomainException
{
    public BuildingIsAlreadyDecommissionedException(BuildingId buildingId)
        : base($"Building {buildingId} is already decommisioned") { }
}

using System.Threading;

namespace CFU.Domain.SupplyContext.BuildingAggregate;

public class BuildingFactory
{
    private readonly IBuildingRepository _buildingRepository;

    public BuildingFactory(IBuildingRepository buildingRepository)
    {
        _buildingRepository = Guard.Against.Default(buildingRepository, nameof(buildingRepository));
    }

    public async Task<Building> CreateAsync(
            BuildingId id,
            Address address,
            CancellationToken cancellationToken = default)
    {
        var building = new Building(id, address);

        var existingBuilding = await _buildingRepository.GetAsync(new UniqueBuildingSpecification(address), cancellationToken);
        if (existingBuilding is not null) throw new BuildingAlreadyExistsException(address);

        building.AddDomainEvent(new BuildingCreatedEvent(building));
        return building;
    }
}


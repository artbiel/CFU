using CFU.Domain.SupplyContext.BuildingAggregate;

namespace CFU.UniversityManagement.Infrastructure.Supply.Repositories;

public class BuildingRepository : EFRepository<Building, BuildingId>, IBuildingRepository
{
    public BuildingRepository(UniversityManagementDBContext context) : base(context) { }
}

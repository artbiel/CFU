using CFU.Domain.StructureContext.InstituteAggregate;
namespace CFU.UniversityManagement.Infrastructure.Structure.Repositories;

public class InstituteRepository : EFRepository<Institute, InstituteId>, IInstituteRepository
{
    public InstituteRepository(UniversityManagementDBContext context) : base(context) { }
}

using CFU.Domain.StructureContext.AcademyAggregate;

namespace CFU.UniversityManagement.Infrastructure.Structure.Repositories;

public class AcademyRepository : EFRepository<Academy, AcademyId>, IAcademyRepository
{
    public AcademyRepository(UniversityManagementDBContext context) : base(context) { }
}

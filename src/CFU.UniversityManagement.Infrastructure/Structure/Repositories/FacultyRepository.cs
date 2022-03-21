using CFU.Domain.StructureContext.FacultyAggregate;

namespace CFU.UniversityManagement.Infrastructure.Structure.Repositories;

public class FacultyRepository : EFRepository<Faculty, FacultyId>, IFacultyRepository
{
    public FacultyRepository(UniversityManagementDBContext context) : base(context) { }
}

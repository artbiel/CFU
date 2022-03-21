using CFU.Domain.StructureContext.DepartmentAggregate;
namespace CFU.UniversityManagement.Infrastructure.Structure.Repositories;

public class DepartmentRepository : EFRepository<Department, DepartmentId>, IDepartmentRepository
{
    public DepartmentRepository(UniversityManagementDBContext context) : base(context) { }
}

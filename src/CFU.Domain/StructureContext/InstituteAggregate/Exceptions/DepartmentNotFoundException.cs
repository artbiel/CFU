namespace CFU.Domain.StructureContext.InstituteAggregate;

public class DepartmentNotFoundException : DomainException
{
    public DepartmentNotFoundException(DepartmentId department, Institute institute)
        : base($"Department {department} not found at institute {institute.Title}") { }
}

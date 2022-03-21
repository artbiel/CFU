namespace CFU.Domain.StructureContext.FacultyAggregate;

public class DepartmentNotFoundException : DomainException
{
    public DepartmentNotFoundException(DepartmentId department, Faculty faculty)
        : base($"Department {department} not found at faculty {faculty.Title}") { }
}

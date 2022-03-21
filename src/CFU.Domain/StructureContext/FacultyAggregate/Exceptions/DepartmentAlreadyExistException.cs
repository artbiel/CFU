namespace CFU.Domain.StructureContext.FacultyAggregate;

public class DepartmentAlreadyExistException : DomainException
{
    public DepartmentAlreadyExistException(string departmentTitle, Faculty faculty)
        : base($"Department {departmentTitle} already exists at faculty {faculty.Title}") { }
}

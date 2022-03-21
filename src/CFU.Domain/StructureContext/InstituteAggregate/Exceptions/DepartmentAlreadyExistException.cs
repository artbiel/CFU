namespace CFU.Domain.StructureContext.InstituteAggregate;

public class DepartmentAlreadyExistException : DomainException
{
    public DepartmentAlreadyExistException(string departmentTitle, Institute institute)
        : base($"Department {departmentTitle} already exists at institute {institute.Title}") { }
}

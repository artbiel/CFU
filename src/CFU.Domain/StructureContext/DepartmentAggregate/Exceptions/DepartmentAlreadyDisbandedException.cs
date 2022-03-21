namespace CFU.Domain.StructureContext.DepartmentAggregate;

public class DepartmentAlreadyDisbandedException : DomainException
{
    public DepartmentAlreadyDisbandedException(Department department)
        : base($"Department with title {department.Title} already disbanded") { }
}

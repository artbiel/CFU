using CFU.Domain.StructureContext.Entities;

namespace CFU.Domain.StructureContext.FacultyAggregate;

public class Faculty : StructureUnit<FacultyId>, IAggregateRoot<FacultyId>
{
    internal Faculty(FacultyId id, string title) : base(id, title) { }

    private readonly List<Department> _departments = new List<Department>();
    public IReadOnlyCollection<Department> Departments => _departments.AsReadOnly();

    public bool IsDisbanded { get; private set; }

    public Department CreateDepartment(DepartmentId id, string title)
    {
        if (IsDisbanded) throw new FacultyAlreadyDisbandedException(this);
        var department = new Department(id, this, title);
        if (_departments.Any(d => d.Equals(department)))
            throw new DepartmentAlreadyExistException(title, this);
        _departments.Add(department);
        AddDomainEvent(new FacultyDepartmentCreatedEvent(department));
        return department;
    }

    public void Disband()
    {
        if (IsDisbanded) throw new FacultyAlreadyDisbandedException(this);
        IsDisbanded = true;
        AddDomainEvent(new FacultyDisbandedEvent(this));
    }

    internal void DisbandDepartment(DepartmentId id)
    {
        var department = _departments.SingleOrDefault(b => b.Id == id);
        if (department is null) throw new DepartmentNotFoundException(id, this);
        _departments.Remove(department);
    }

}

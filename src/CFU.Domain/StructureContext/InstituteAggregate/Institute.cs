using CFU.Domain.StructureContext.Entities;

namespace CFU.Domain.StructureContext.InstituteAggregate;

public class Institute : StructureUnit<InstituteId>, IAggregateRoot<InstituteId>
{
    internal Institute(InstituteId id, string title) : base(id, title) { }

    public bool IsDisbanded { get; private set; }

    private readonly List<Department> _departments = new List<Department>();
    public IReadOnlyCollection<Department> Departments => _departments.AsReadOnly();

    public Department CreateDepartment(DepartmentId id, string title)
    {
        if (IsDisbanded) throw new InstituteAlreadyDisbandedException(this);
        var department = new Department(id, this, title);
        if (_departments.Any(d => d.Equals(department)))
            throw new DepartmentAlreadyExistException(title, this);
        _departments.Add(department);
        AddDomainEvent(new InstituteDepartmentCreatedEvent(department));
        return department;
    }


    public void Disband()
    {
        if (IsDisbanded) throw new InstituteAlreadyDisbandedException(this);
        IsDisbanded = true;
        AddDomainEvent(new InstituteDisbandedEvent(this));
    }

    internal void DisbandDepartment(DepartmentId id)
    {
        if (IsDisbanded) throw new InstituteAlreadyDisbandedException(this);
        var department = _departments.SingleOrDefault(b => b.Id == id);
        _departments.Remove(department);
    }
}

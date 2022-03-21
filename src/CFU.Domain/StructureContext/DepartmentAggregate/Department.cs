using CFU.Domain.StructureContext.Entities;

namespace CFU.Domain.StructureContext.DepartmentAggregate;

public class Department : StructureUnit<DepartmentId>, IAggregateRoot<DepartmentId>
{
    internal Department(DepartmentId id, string title) : base(id, title) { }

    public bool IsDisbanded { get; private set; }

    public void Disband()
    {
        if (IsDisbanded) throw new DepartmentAlreadyDisbandedException(this);
        IsDisbanded = true;
        AddDomainEvent(new DepartmentDisbandedEvent(this));
    }
}

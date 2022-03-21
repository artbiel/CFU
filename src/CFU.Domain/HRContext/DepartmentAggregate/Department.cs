namespace CFU.Domain.HRContext.DepartmentAggregate;

public class Department : Entity<DepartmentId>, IAggregateRoot<DepartmentId>
{
    public Department(DepartmentId id) : base(id) { }
}

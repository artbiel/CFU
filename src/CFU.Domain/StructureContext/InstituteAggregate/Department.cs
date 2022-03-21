using CFU.Domain.StructureContext.Entities;

namespace CFU.Domain.StructureContext.InstituteAggregate;

public class Department : StructureUnit<DepartmentId>
{
    private Department(DepartmentId id, string title) : base(id, title) { }

    internal Department(DepartmentId id, Institute institute, string title) : base(id, title)
    {
        InstituteId = Guard.Against.Default(institute, nameof(institute)).Id;
    }

    public InstituteId InstituteId { get; }
}

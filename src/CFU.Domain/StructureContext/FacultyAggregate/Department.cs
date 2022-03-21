using CFU.Domain.StructureContext.Entities;

namespace CFU.Domain.StructureContext.FacultyAggregate;

public class Department : StructureUnit<DepartmentId>
{
    private Department(DepartmentId id, string title) : base(id, title) { }

    internal Department(DepartmentId id, Faculty faculty, string title) : base(id, title)
    {
        FacultyId = Guard.Against.Default(faculty, nameof(faculty)).Id;
    }

    public FacultyId FacultyId { get; }
}

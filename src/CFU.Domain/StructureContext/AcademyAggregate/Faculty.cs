using CFU.Domain.StructureContext.Entities;

namespace CFU.Domain.StructureContext.AcademyAggregate;

public class Faculty : StructureUnit<FacultyId>
{
    private Faculty(FacultyId id, string title) : base(id, title) { }

    internal Faculty(FacultyId id, Academy academy, string title) : base(id, title)
    {
        AcademyId = Guard.Against.Default(academy, nameof(academy)).Id;
    }

    public AcademyId AcademyId { get; }

}

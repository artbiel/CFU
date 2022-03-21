namespace CFU.Domain.StructureContext.AcademyAggregate;

public class FacultyNotFoundException : DomainException
{
    public FacultyNotFoundException(FacultyId faculty, Academy academy)
        : base($"Faculty {faculty} not found at academy {academy.Title}") { }
}

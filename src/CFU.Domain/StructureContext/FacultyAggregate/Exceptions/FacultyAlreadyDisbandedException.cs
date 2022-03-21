namespace CFU.Domain.StructureContext.FacultyAggregate;

public class FacultyAlreadyDisbandedException : DomainException
{
    public FacultyAlreadyDisbandedException(Faculty faculty)
        : base($"Faculty with title {faculty.Title} already disbanded") { }
}

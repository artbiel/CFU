namespace CFU.Domain.StructureContext.AcademyAggregate;

public class FacultyAlreadyExistException : DomainException
{
    public FacultyAlreadyExistException(string facultyTitle, Academy academy)
        : base($"Faculty {facultyTitle} already exists at academy {academy.Title}") { }
}

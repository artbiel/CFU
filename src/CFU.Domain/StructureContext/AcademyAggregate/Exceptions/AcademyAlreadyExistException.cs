namespace CFU.Domain.StructureContext.AcademyAggregate;

public class AcademyAlreadyExistException : DomainException
{
    public AcademyAlreadyExistException(Academy academy)
        : base($"Academy with title {academy.Title} already exists") { }
}

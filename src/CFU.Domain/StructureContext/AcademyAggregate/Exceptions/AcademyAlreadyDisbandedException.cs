namespace CFU.Domain.StructureContext.AcademyAggregate;

public class AcademyAlreadyDisbandedException : DomainException
{
    public AcademyAlreadyDisbandedException(Academy academy)
        : base($"Academy with title {academy.Title} already disbanded") { }
}

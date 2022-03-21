namespace CFU.Domain.StructureContext.InstituteAggregate;

public class InstituteAlreadyExistException : DomainException
{
    public InstituteAlreadyExistException(Institute institute)
        : base($"Institute with title {institute.Title} already exists") { }
}

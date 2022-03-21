namespace CFU.Domain.StructureContext.InstituteAggregate;

public class InstituteAlreadyDisbandedException : DomainException
{
    public InstituteAlreadyDisbandedException(Institute institute)
        : base($"Institute with title {institute.Title} already disbanded") { }
}

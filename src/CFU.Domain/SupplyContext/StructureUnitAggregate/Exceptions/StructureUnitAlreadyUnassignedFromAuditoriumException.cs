namespace CFU.Domain.SupplyContext.StructureUnitAggregate;

public class StructureUnitAlreadyUnassignedFromAuditoriumException : DomainException
{
    public StructureUnitAlreadyUnassignedFromAuditoriumException(StructureUnitId structureUnitId)
        : base($"Structure unit {structureUnitId} already unassigned from auditorium") { }
}

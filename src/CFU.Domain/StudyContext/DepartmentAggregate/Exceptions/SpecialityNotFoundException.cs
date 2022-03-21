namespace CFU.Domain.StudyContext.DepartmentAggregate;

public class SpecialityNotFoundException : DomainException
{
    public SpecialityNotFoundException(SpecialityId specialityId) : base($"Speciality {specialityId} not found") { }
}

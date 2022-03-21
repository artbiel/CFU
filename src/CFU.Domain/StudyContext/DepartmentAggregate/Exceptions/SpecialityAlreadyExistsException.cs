namespace CFU.Domain.StudyContext.DepartmentAggregate;

public class SpecialityAlreadyExistsException : DomainException
{
    public SpecialityAlreadyExistsException(Speciality speciality) : base($"Speciality {speciality.Title} already exists")
    {
    }
}

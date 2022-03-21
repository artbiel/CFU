namespace CFU.Domain.StudyContext.SpecialityAggregate;

public class SubjectNotFoundException : DomainException
{
    public SubjectNotFoundException(SubjectId subjectId)
        : base($"Subject {subjectId} not found") { }
}

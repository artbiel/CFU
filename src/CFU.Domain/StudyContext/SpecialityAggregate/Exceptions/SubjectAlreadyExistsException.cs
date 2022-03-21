namespace CFU.Domain.StudyContext.SpecialityAggregate;

public class SubjectAlreadyExistsException : DomainException
{
    public SubjectAlreadyExistsException(Subject subject)
        : base($"Subject {subject.Title} already exists") { }
}

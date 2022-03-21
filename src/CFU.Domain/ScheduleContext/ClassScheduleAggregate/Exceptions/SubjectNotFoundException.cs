namespace CFU.Domain.ScheduleContext.ClassScheduleAggregate;

public class SubjectNotFoundException : DomainException
{
    public SubjectNotFoundException(SubjectId subjectId) : base($"Subject {subjectId} not found")
    {
    }
}

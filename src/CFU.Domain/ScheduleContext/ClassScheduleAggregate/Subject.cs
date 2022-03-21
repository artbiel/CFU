namespace CFU.Domain.ScheduleContext.ClassScheduleAggregate;

public class Subject : Entity<SubjectId>
{
    internal Subject(SubjectId id, TeacherId teacherId) : base(id)
    {
        SetTeacher(teacherId);
    }

    public TeacherId TeacherId { get; private set; }

    internal void SetTeacher(TeacherId teacherId)
    {
        TeacherId = Guard.Against.Default(teacherId, nameof(teacherId));
    }
}

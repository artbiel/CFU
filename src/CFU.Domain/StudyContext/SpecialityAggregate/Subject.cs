namespace CFU.Domain.StudyContext.SpecialityAggregate;

public class Subject : Entity<SubjectId>
{
    internal Subject(SubjectId id, Speciality speciality, string title, TeacherId teacherId) : base(id)
    {
        Title = Guard.Against.NullOrWhiteSpace(title, nameof(title));
        SpecialityId = Guard.Against.Default(speciality, nameof(speciality)).Id;
        SetTeacher(teacherId);
    }

    public SpecialityId SpecialityId { get; private init; }
    public string Title { get; private init; }
    public TeacherId TeacherId { get; private set; }

    internal void SetTeacher(TeacherId teacherId)
    {
        TeacherId = Guard.Against.Default(teacherId, nameof(teacherId));
    }
}

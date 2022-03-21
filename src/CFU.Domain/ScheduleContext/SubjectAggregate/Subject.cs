namespace CFU.Domain.ScheduleContext.SubjectAggregate;

public class Subject : Entity<SubjectId>
{
    internal Subject(SubjectId id, List<GroupId> groups, TeacherId teacherId) : base(id)
    {
        _groups = Guard.Against.Default(groups, nameof(groups));
        SetTeacher(teacherId);
    }

    public SpecialityId SpecialityId { get; private init; }
    public TeacherId TeacherId { get; private set; }

    private readonly List<GroupId> _groups = new List<GroupId>();
    public IReadOnlyCollection<GroupId> Groups => _groups.AsReadOnly();

    internal void SetTeacher(TeacherId teacherId)
    {
        TeacherId = Guard.Against.Default(teacherId, nameof(teacherId));
    }

    internal void AddGroup(GroupId groupId)
    {
        if (!_groups.Contains(groupId))
            _groups.Add(groupId);
    }

    internal void RemoveGroup(GroupId groupId) => _groups.Remove(groupId);
}

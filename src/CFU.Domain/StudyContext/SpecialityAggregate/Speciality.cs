namespace CFU.Domain.StudyContext.SpecialityAggregate;

public class Speciality : Entity<SpecialityId>, IAggregateRoot<SpecialityId>
{
    internal Speciality(SpecialityId id, string title, AttendanceType attendanceType, Degree degree) : base(id)
    {
        Title = Guard.Against.NullOrWhiteSpace(title, nameof(title));
        AttendanceType = Guard.Against.Default(attendanceType, nameof(attendanceType));
        Degree = Guard.Against.Default(degree, nameof(degree));
        Abbreviation = GetAbbreviation(title);
    }

    public string Title { get; }
    public string Abbreviation { get; }
    public AttendanceType AttendanceType { get; }
    public Degree Degree { get; }

    private readonly List<Group> _groups = new List<Group>();
    public IReadOnlyCollection<Group> Groups => _groups.AsReadOnly();

    private readonly List<Subject> _subjects = new List<Subject>();
    public IReadOnlyCollection<Subject> Subjects => _subjects.AsReadOnly();

    public Group AddGroup(GroupId id, YearOfAdmission yearOfAdmission)
    {
        var group = new Group(id, this, yearOfAdmission);
        if (_groups.Contains(group)) throw new GroupAlreadyExistsException(group);
        _groups.Add(group);
        AddDomainEvent(new GroupAddedToSpecialityEvent(this, group));
        return group;
    }

    public void RemoveGroup(GroupId groupId)
    {
        var group = _groups.FirstOrDefault(g => g.Id == groupId);
        if (group is null) throw new GroupNotFoundException(groupId);
        _groups.Remove(group);
        AddDomainEvent(new GroupAddedToSpecialityEvent(this, group));
    }

    public Subject AddSubject(SubjectId id, string title, TeacherId teacherId)
    {
        var subject = new Subject(id, this, title, teacherId);
        if (_subjects.Contains(subject)) throw new SubjectAlreadyExistsException(subject);
        _subjects.Add(subject);
        AddDomainEvent(new SubjectAddedToSpecialityEvent(this, subject));
        return subject;
    }

    public void RemoveSubject(SubjectId subjectId)
    {
        var subject = _subjects.FirstOrDefault(s => s.Id == subjectId);
        if (subject is null) throw new SubjectNotFoundException(subjectId);
        _subjects.Remove(subject);
        AddDomainEvent(new SubjectRemovedFromSpecialityEvent(this, subject));
    }

    private static string GetAbbreviation(string title)
    {
        var words = title.Split(' ');
        var firstLetters = words.Select(w => w.FirstOrDefault()).Where(l => char.IsUpper(l)).ToArray();
        return string.Join("", firstLetters);
    }

    public override string ToString() => Title;
}


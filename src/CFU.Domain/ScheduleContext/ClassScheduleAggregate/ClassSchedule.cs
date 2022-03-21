namespace CFU.Domain.ScheduleContext.ClassScheduleAggregate;

public class ClassSchedule : Entity<ClassScheduleId>, IAggregateRoot<ClassScheduleId>
{
    internal ClassSchedule(ClassScheduleId id, GroupId groupId) : base(id)
    {
        GroupId = Guard.Against.Default(groupId, nameof(groupId));
    }

    public GroupId GroupId { get; private init; }

    private readonly List<Pair> _pairs = new List<Pair>();
    public IReadOnlyCollection<Pair> Pairs => _pairs.AsReadOnly();

    private readonly List<Subject> _subjects = new List<Subject>();
    public IReadOnlyCollection<Subject> Subjects => _subjects.AsReadOnly();

    internal void AddPair(Pair pair)
    {
        Guard.Against.Default(pair, nameof(pair));
        if (_pairs.Contains(pair)) throw new PairAlreadyExistException(pair);
        _pairs.Add(pair);
    }

    internal void RemovePair(PairId pairId)
    {
        var pair = _pairs.FirstOrDefault(p => p.Id == pairId);
        if (pair is null) throw new PairNotFoundException(pairId);
        _pairs.Remove(pair);
    }

    internal void AddSubject(Subject subject)
    {
        Guard.Against.Default(subject, nameof(subject));
        if (!_subjects.Contains(subject))
            _subjects.Add(subject);
    }

    internal void RemoveSubject(SubjectId subjectId)
    {
        var subject = _subjects.FirstOrDefault(s => s.Id == subjectId);
        if (subject is not null)
            _subjects.Remove(subject);
        _pairs.RemoveAll(p => p.Subject.Id == subjectId);
    }
}


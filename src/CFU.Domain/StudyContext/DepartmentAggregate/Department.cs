namespace CFU.Domain.StudyContext.DepartmentAggregate;

public class Department : Entity<DepartmentId>, IAggregateRoot<DepartmentId>
{
    internal Department(DepartmentId id) : base(id) { }

    private readonly List<Speciality> _specialities = new List<Speciality>();
    public IReadOnlyCollection<Speciality> Specialities => _specialities.AsReadOnly();

    private readonly List<TeacherId> _teachers = new List<TeacherId>();
    public IReadOnlyCollection<TeacherId> Teachers => _teachers.AsReadOnly();

    public Speciality AddSpeciality(SpecialityId id, string title, AttendanceType attendanceType, Degree degree)
    {
        var speciality = new Speciality(id, this, title, attendanceType, degree);
        if (_specialities.Contains(speciality))
            throw new SpecialityAlreadyExistsException(speciality);
        _specialities.Add(speciality);
        AddDomainEvent(new SpecialityAddedEvent(speciality));
        return speciality;
    }

    public void RemoveSpeciality(SpecialityId specialityId)
    {
        var speciality = _specialities.FirstOrDefault(s => s.Id == specialityId);
        if (speciality is null) throw new SpecialityNotFoundException(specialityId);
        _specialities.Remove(speciality);
        AddDomainEvent(new SpecialityRemovedEvent(speciality));
    }

    internal void AddTeacher(TeacherId teacherId)
    {
        Guard.Against.Default(teacherId, nameof(teacherId));
        if (_teachers.Contains(teacherId)) throw new TeacherAlreadyExistsException(teacherId);
        _teachers.Add(teacherId);
    }

    internal void RemoveTeacher(TeacherId teacherId)
    {
        Guard.Against.Default(teacherId, nameof(teacherId));
        if (!_teachers.Contains(teacherId)) throw new TeacherNotFoundException(teacherId);
        _teachers.Remove(teacherId);
    }
}

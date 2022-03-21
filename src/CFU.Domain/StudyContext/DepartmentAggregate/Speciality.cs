namespace CFU.Domain.StudyContext.DepartmentAggregate;

public class Speciality : Entity<SpecialityId>
{
    internal Speciality(SpecialityId id, Department department, string title, AttendanceType attendanceType, Degree degree) : base(id)
    {
        Title = Guard.Against.NullOrWhiteSpace(title, nameof(title));
        DepartmentId = Guard.Against.Default(department, nameof(department)).Id;
        AttendanceType = Guard.Against.Default(attendanceType, nameof(attendanceType));
        Degree = Guard.Against.Default(degree, nameof(degree));
    }

    public DepartmentId DepartmentId { get; }
    public string Title { get; }
    public AttendanceType AttendanceType { get; }
    public Degree Degree { get; }

    public override bool Equals(object other) =>
        other is Speciality speciality &&
        Title == speciality.Title &&
        DepartmentId == speciality.DepartmentId &&
        AttendanceType == speciality.AttendanceType &&
        Degree == speciality.Degree;

    public override int GetHashCode() => Title.GetHashCode() ^
        DepartmentId.GetHashCode() ^
        AttendanceType.GetHashCode() ^
        Degree.GetHashCode();
}

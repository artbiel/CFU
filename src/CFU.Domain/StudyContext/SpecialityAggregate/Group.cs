namespace CFU.Domain.StudyContext.SpecialityAggregate;

public class Group : Entity<GroupId>
{
    internal Group(GroupId id, Speciality speciality, YearOfAdmission yearOfAdmission) : base(id)
    {
        SpecialityId = Guard.Against.Default(speciality, nameof(speciality)).Id;
        YearOfAdmission = Guard.Against.Default(yearOfAdmission, nameof(yearOfAdmission));
        Title = GetTitle(speciality, yearOfAdmission);
    }

    public string Title { get; }
    public SpecialityId SpecialityId { get; }
    public YearOfAdmission YearOfAdmission { get; }

    private string GetTitle(Speciality speciality, YearOfAdmission yearOfAdmission)
    {
        return $"{speciality.Abbreviation}-{speciality.Degree.ShortName}-{speciality.AttendanceType.ShortName}-{yearOfAdmission.Value.ToString()[^2..]}";
    }

    public override string ToString() => Title;
}

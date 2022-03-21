namespace CFU.Domain.StudyContext;

public class AttendanceType : Enumeration
{
    private AttendanceType(int id, string name, string shortName) : base(id, name)
    {
        ShortName = shortName;
    }

    public static AttendanceType Attended => new(1, nameof(Attended), "о");
    public static AttendanceType Distance => new(2, nameof(Distance), "з");
    public static AttendanceType Hybrid => new(3, nameof(Hybrid), "оз");

    public string ShortName { get; set; }
}

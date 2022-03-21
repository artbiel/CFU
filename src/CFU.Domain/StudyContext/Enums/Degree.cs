namespace CFU.Domain.StudyContext;

public class Degree : Enumeration
{
    private Degree(int id, string name, string shortName) : base(id, name)
    {
        ShortName = shortName;
    }

    public static Degree Baccalaureate => new(1, nameof(Baccalaureate), "б");
    public static Degree Magistracy => new(2, nameof(Magistracy), "м");

    public string ShortName { get; set; }
}
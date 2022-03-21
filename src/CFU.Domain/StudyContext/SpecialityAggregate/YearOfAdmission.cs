namespace CFU.Domain.StudyContext.SpecialityAggregate;

public record struct YearOfAdmission
{
    public const int MIN_YEAR = 2000;

    public YearOfAdmission(int year)
    {
        int now = DateTime.UtcNow.Year;
        Value = Guard.Against.OutOfRange(year, nameof(year), MIN_YEAR, now);
    }

    public int Value { get; }
}

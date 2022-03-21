namespace CFU.Domain.SupplyContext.BuildingAggregate;

public record struct AuditoriumNumber
{
    public int Value { get; }

    public AuditoriumNumber(int number)
    {
        Value = Guard.Against.NegativeOrZero(number, nameof(number));
    }

    public static bool operator >(AuditoriumNumber first, AuditoriumNumber second) => first.Value > second.Value;
    public static bool operator <(AuditoriumNumber first, AuditoriumNumber second) => first.Value < second.Value;

    public override string ToString() => Value.ToString();
}

namespace CFU.Domain.SupplyContext.BuildingAggregate;

public record struct ClassRoomCapacity
{
    public const int MIN_CAPACITY = 1;
    public const int MAX_CAPACITY = 150;

    public int Value { get; }

    public ClassRoomCapacity(int number)
    {
        Value = Guard.Against.OutOfRange(number, nameof(number), MIN_CAPACITY, MAX_CAPACITY);
    }

    public bool Equals(AuditoriumNumber other) => Value == other.Value;
    public static bool operator >(ClassRoomCapacity first, ClassRoomCapacity second) => first.Value > second.Value;
    public static bool operator <(ClassRoomCapacity first, ClassRoomCapacity second) => first.Value < second.Value;

    public override string ToString() => Value.ToString();
}


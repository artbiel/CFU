namespace CFU.Domain.ScheduleContext.PairsTimetableAggregate;

public struct PairOrder : IEquatable<PairOrder>
{
    public const int MIN_ORDER = 1;
    public const int MAX_ORDER = 8;

    public int Value { get; }

    public PairOrder(int order)
    {
        if (order < MIN_ORDER || order > MAX_ORDER)
            throw new ArgumentOutOfRangeException(nameof(order));
        Value = order;
    }

    public bool Equals(PairOrder other) => Value == other.Value;
    public static bool operator ==(PairOrder first, PairOrder second) => first.Equals(second);
    public static bool operator !=(PairOrder first, PairOrder second) => !first.Equals(second);
    public static bool operator >(PairOrder first, PairOrder second) => first.Value > second.Value;
    public static bool operator <(PairOrder first, PairOrder second) => first.Value < second.Value;

    public override bool Equals(object obj) => obj is PairOrder order && Equals(order);
    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value.ToString();
}

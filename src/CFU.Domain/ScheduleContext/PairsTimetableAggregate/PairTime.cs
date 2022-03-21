namespace CFU.Domain.ScheduleContext.PairsTimetableAggregate;

public class PairTime : Entity, IEquatable<PairTime>
{
    internal PairTime(PairOrder order, TimeOnly startTime, PairsTimetableId timetableId)
    {
        Order = order;
        SetPairTime(startTime);
        TimetableId = timetableId;
    }

    public PairOrder Order { get; private init; }
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
    public PairsTimetableId TimetableId { get; }

    internal void SetPairTime(TimeOnly startTime)
    {
        StartTime = startTime;
        EndTime = startTime.AddMinutes(90);
    }

    public bool Equals(PairTime other) => Order == other?.Order;
    public override bool Equals(object other) => other is PairTime pt && Equals(pt);
    public override int GetHashCode() => Order.GetHashCode();


    internal void CheckConflicts(PairTime other)
    {
        if (Order == other.Order)
            throw new PairTimeAlreadyExistsException(other);
        if (Order > other.Order && StartTime <= other.EndTime || Order < other.Order && EndTime >= other.StartTime)
            throw new PairTimesConflictException(this, other);
    }
}

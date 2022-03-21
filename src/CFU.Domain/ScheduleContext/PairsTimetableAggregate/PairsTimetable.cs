namespace CFU.Domain.ScheduleContext.PairsTimetableAggregate;


public class PairsTimetable : Entity<PairsTimetableId>, IAggregateRoot<PairsTimetableId>
{
    internal PairsTimetable(PairsTimetableId id, string title) : base(id)
    {
        Title = Guard.Against.NullOrWhiteSpace(title, nameof(title));
    }

    public string Title { get; private set; }

    private readonly List<PairTime> _pairTimes = new List<PairTime>();
    public IReadOnlyCollection<PairTime> PairTimes => _pairTimes.AsReadOnly();

    public PairTime AddPairTime(PairOrder order, TimeOnly startTime)
    {
        var pairTime = new PairTime(order, startTime, Id);
        CheckConflicts(pairTime);
        _pairTimes.Add(pairTime);
        AddDomainEvent(new PairTimeAddedEvent(pairTime));
        return pairTime;
    }

    public void RemovePairTime(PairOrder order)
    {
        var pairTime = _pairTimes.FirstOrDefault(p => p.Order == order);
        if (pairTime is null) throw new PairTimeNotFoundException(order);
        AddDomainEvent(new PairTimeRemovedEvent(pairTime));
        _pairTimes.Remove(pairTime);
    }

    public PairTime SetPairTime(PairOrder order, TimeOnly startTime)
    {
        var pairtTime = _pairTimes.FirstOrDefault(p => p.Order == order);
        if (pairtTime is null) throw new PairTimeNotFoundException(order);
        pairtTime.SetPairTime(startTime);
        CheckConflicts(pairtTime);
        AddDomainEvent(new PairTimeChangedEvent(pairtTime));
        return pairtTime;
    }

    private void CheckConflicts(PairTime pairTime)
    {
        foreach (var pt in _pairTimes) {
            if (pt != pairTime)
                pt.CheckConflicts(pairTime);
        }
    }
}

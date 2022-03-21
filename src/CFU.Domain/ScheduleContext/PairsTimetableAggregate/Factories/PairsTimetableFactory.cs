using System.Threading;

namespace CFU.Domain.ScheduleContext.PairsTimetableAggregate;

public class PairsTimetableFactory
{
    private readonly IPairsTimetableRepository _repository;

    public PairsTimetableFactory(IPairsTimetableRepository repository)
    {
        _repository = Guard.Against.Default(repository, nameof(repository));
    }

    public async Task<PairsTimetable> CreateAsync(PairsTimetableId id,
       string title,
       CancellationToken cancellationToken = default)
    {
        var timetable = new PairsTimetable(id, title);

        var existingTimetable = await _repository.GetAsync(new UniquePairsTimetableSpecification(title), cancellationToken);
        if (existingTimetable is not null)
            throw new PairsTimetableAlreadyExistsException(title);

        timetable.AddDomainEvent(new TimetableCreatedEvent(timetable));
        return timetable;
    }
}

using System.Threading;

namespace CFU.Domain.ScheduleContext.ClassScheduleAggregate;

public class ClassScheduleDomainService
{
    private readonly IClassScheduleRepository _classSchedulerRepository;

    public ClassScheduleDomainService(IClassScheduleRepository classSchedulerRepository)
    {
        _classSchedulerRepository = Guard.Against.Default(classSchedulerRepository, nameof(classSchedulerRepository));
    }

    public async Task UpdateClassScheduleAsync(ClassSchedule classSchedule,
        (PairId Id, SubjectId SubjectId, AuditoriumId ClassRoomId)[] attendedPairs,
        (PairId Id, SubjectId SubjectId, Url Url)[] remotePairs,
        CancellationToken cancellationToken = default)
    {
        Guard.Against.Default(classSchedule, nameof(classSchedule));
        //Guard.Against.NullOrEmpty(newClassSchedulePairs, nameof(newClassSchedulePairs));
        attendedPairs ??= Array.Empty<(PairId Id, SubjectId Subject, AuditoriumId ClassRoomId)>();
        remotePairs ??= Array.Empty<(PairId Id, SubjectId Subject, Url Url)>();

        var newPairs = new List<Pair>(attendedPairs.Length + remotePairs.Length);

        foreach (var pair in attendedPairs) {
            var subject = classSchedule.Subjects.FirstOrDefault(s => s.Id == pair.SubjectId);
            newPairs.Add(new AttendedPair(pair.Id, classSchedule, subject, pair.ClassRoomId));
        }
        foreach (var pair in remotePairs) {
            var subject = classSchedule.Subjects.FirstOrDefault(s => s.Id == pair.SubjectId);
            newPairs.Add(new RemotePair(pair.Id, classSchedule, subject, pair.Url));
        }

        var pairsToAdd = newPairs.Except(classSchedule.Pairs).ToArray();
        var pairsToRemove = classSchedule.Pairs.Except(newPairs).ToArray();

        foreach (var pair in pairsToRemove)
            classSchedule.RemovePair(pair.Id);

        foreach (var pair in pairsToAdd)
            classSchedule.AddPair(pair);

        var conflictingClassSchedules = await _classSchedulerRepository.GetAllAsync(new ConflictingPairsSpecification(pairsToAdd), cancellationToken);
        if (conflictingClassSchedules.Any())
            throw new ClassSchedulesConflictException(conflictingClassSchedules);

        classSchedule.AddDomainEvent(new ClassScheduleUpdatedEvent(classSchedule));
    }
}

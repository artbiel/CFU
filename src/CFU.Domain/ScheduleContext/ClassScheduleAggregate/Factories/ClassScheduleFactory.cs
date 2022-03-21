using System.Threading;

namespace CFU.Domain.ScheduleContext.ClassScheduleAggregate;

public class ClassScheduleFactory
{
    private readonly IClassScheduleRepository _repository;

    public ClassScheduleFactory(IClassScheduleRepository repository)
    {
        _repository = Guard.Against.Default(repository, nameof(repository));
    }

    public async Task<ClassSchedule> CreateAsync(
            ClassScheduleId id,
            GroupId groupId,
            CancellationToken cancellationToken = default)
    {
        var classSchedule = new ClassSchedule(id, groupId);

        var existingClassSchedule = await _repository.GetAsync(new UniqueClassScheduleSpecification(groupId), cancellationToken);
        if (existingClassSchedule is not null) throw new ClassScheduleAlreadyExistsException(existingClassSchedule);

        classSchedule.AddDomainEvent(new ClassScheduleCreatedEvent(classSchedule));
        return classSchedule;
    }

}

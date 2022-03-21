using CFU.Domain.Contracts.Identifiers;
using CFU.Domain.ScheduleContext.ClassScheduleAggregate;
using CFU.Domain.StudyContext.SpecialityAggregate;

namespace CFU.EventHandlers.ScheduleContext;

public class GroupAddedToSpecialityEventHandler : INotificationHandler<GroupAddedToSpecialityEvent>
{
    private readonly IClassScheduleRepository _classScheduleRepository;

    public GroupAddedToSpecialityEventHandler(IClassScheduleRepository classScheduleRepository)
    {
        _classScheduleRepository = classScheduleRepository;
    }

    public Task Handle(GroupAddedToSpecialityEvent notification, CancellationToken cancellationToken)
    {
        var classSchedule = new ClassSchedule(new ClassScheduleId(Guid.NewGuid()), notification.Group.Id);
        _classScheduleRepository.Update(classSchedule);
        return Task.CompletedTask;
    }
}

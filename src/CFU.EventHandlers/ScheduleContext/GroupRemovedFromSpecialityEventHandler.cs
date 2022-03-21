using CFU.Domain.ScheduleContext.ClassScheduleAggregate;
using CFU.Domain.ScheduleContext.ClassScheduleAggregate.Specifications;
using CFU.Domain.StudyContext.SpecialityAggregate;

namespace CFU.EventHandlers.ScheduleContext;

public class GroupRemovedFromSpecialityEventHandler : INotificationHandler<GroupRemovedFromSpecialityEvent>
{
    private readonly IClassScheduleRepository _classScheduleRepository;

    public GroupRemovedFromSpecialityEventHandler(IClassScheduleRepository classScheduleRepository)
    {
        _classScheduleRepository = classScheduleRepository;
    }

    public Task Handle(GroupRemovedFromSpecialityEvent notification, CancellationToken cancellationToken)
    {
        _classScheduleRepository.Remove(new ByGroupSpecification(notification.Group.Id));
        return Task.CompletedTask;
    }
}

using CFU.Domain.ScheduleContext.ClassScheduleAggregate;
using CFU.Domain.ScheduleContext.ClassScheduleAggregate.Specifications;
using CFU.Domain.StudyContext.SpecialityAggregate;

namespace CFU.EventHandlers.ScheduleContext;

public class SubjectRemovedFromSpecialityEventHandler : INotificationHandler<SubjectRemovedFromSpecialityEvent>
{
    private readonly IClassScheduleRepository _classScheduleRepository;

    public SubjectRemovedFromSpecialityEventHandler(IClassScheduleRepository classScheduleRepository)
    {
        _classScheduleRepository = classScheduleRepository;
    }

    public async Task Handle(SubjectRemovedFromSpecialityEvent notification, CancellationToken cancellationToken)
    {
        var groups = notification.Speciality.Groups.Select(g => g.Id);
        var classSchedules = await _classScheduleRepository.GetAllAsync(new ByGroupsSpecification(groups), cancellationToken);
        foreach (var classSchedule in classSchedules) {
            classSchedule.RemoveSubject(notification.Subject.Id);
            _classScheduleRepository.Update(classSchedule);
        }
    }
}

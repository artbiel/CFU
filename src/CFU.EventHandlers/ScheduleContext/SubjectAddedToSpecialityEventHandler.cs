using CFU.Domain.ScheduleContext.ClassScheduleAggregate;
using CFU.Domain.ScheduleContext.ClassScheduleAggregate.Specifications;
using CFU.Domain.StudyContext.SpecialityAggregate;
using Subject = CFU.Domain.ScheduleContext.ClassScheduleAggregate.Subject;

namespace CFU.EventHandlers.ScheduleContext;

public class SubjectAddedToSpecialityEventHandler : INotificationHandler<SubjectAddedToSpecialityEvent>
{
    private readonly IClassScheduleRepository _classScheduleRepository;

    public SubjectAddedToSpecialityEventHandler(IClassScheduleRepository classScheduleRepository)
    {
        _classScheduleRepository = classScheduleRepository;
    }

    public async Task Handle(SubjectAddedToSpecialityEvent notification, CancellationToken cancellationToken)
    {
        var groups = notification.Speciality.Groups.Select(g => g.Id);
        var subject = new Subject(notification.Subject.Id, notification.Subject.TeacherId);
        var classSchedules = await _classScheduleRepository.GetAllAsync(new ByGroupsSpecification(groups), cancellationToken);
        foreach (var classSchedule in classSchedules) {
            classSchedule.AddSubject(subject);
            _classScheduleRepository.Update(classSchedule);
        }
    }
}

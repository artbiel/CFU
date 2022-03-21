using CFU.Domain.StudyContext.DepartmentAggregate;
using CFU.Domain.StudyContext.SpecialityAggregate.Repositories;
using Speciality = CFU.Domain.StudyContext.SpecialityAggregate.Speciality;

namespace CFU.EventHandlers.StudyContext;

public class SpecialityAddedEventHandler : INotificationHandler<SpecialityAddedEvent>
{
    private readonly ISpecialityRepository _specialityRepository;

    public SpecialityAddedEventHandler(ISpecialityRepository specialityRepository)
    {
        _specialityRepository = specialityRepository;
    }

    public Task Handle(SpecialityAddedEvent notification, CancellationToken cancellationToken)
    {
        var speciality = new Speciality(notification.Speciality.Id,
            notification.Speciality.Title,
            notification.Speciality.AttendanceType,
            notification.Speciality.Degree);
        _specialityRepository.Add(speciality);
        return Task.CompletedTask;
    }
}

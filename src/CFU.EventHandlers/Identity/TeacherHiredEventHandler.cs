using CFU.Domain.HRContext.TeacherAggregate.Events;
using CFU.Domain.IdentityContext.UserAggregate;

namespace CFU.EventHandlers.Identity;

public class TeacherHiredEventHandler : INotificationHandler<TeacherHiredEvent>
{
    private readonly IUserRepository _userRepository;

    public TeacherHiredEventHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task Handle(TeacherHiredEvent notification, CancellationToken cancellationToken)
    {
        var id = new UserId(notification.Teacher.Id.Id);
        var user = new User(id);
        await _userRepository.Add(user);
    }
}

using CFU.Domain.HRContext.TeacherAggregate;
using CFU.Domain.StudyContext.DepartmentAggregate;

namespace CFU.EventHandlers.StudyContext;

public class TeacherAssignedToDepartmentEventHandler : INotificationHandler<TeacherAssignedToDepartmentEvent>
{
    private readonly IDepartmentRepository _departmentRepository;

    public TeacherAssignedToDepartmentEventHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task Handle(TeacherAssignedToDepartmentEvent notification, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetByIdAsync(notification.DepartmentId, cancellationToken);
        department.AddTeacher(notification.Teacher.Id);
        _departmentRepository.Update(department);
    }
}

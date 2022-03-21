using CFU.Domain.StructureContext.FacultyAggregate;
using CFU.Domain.StudyContext.DepartmentAggregate;
using Department = CFU.Domain.StudyContext.DepartmentAggregate.Department;

namespace CFU.EventHandlers.StudyContext;

public class FacultyDepartmentAddedEventHandler : INotificationHandler<FacultyDepartmentCreatedEvent>
{
    private readonly IDepartmentRepository _departmentRepository;

    public FacultyDepartmentAddedEventHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public Task Handle(FacultyDepartmentCreatedEvent notification, CancellationToken cancellationToken)
    {
        var department = new Department(notification.Department.Id);
        _departmentRepository.Add(department);
        return Task.CompletedTask;
    }
}

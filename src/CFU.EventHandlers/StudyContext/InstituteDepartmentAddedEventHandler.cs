using CFU.Domain.StructureContext.InstituteAggregate;
using CFU.Domain.StudyContext.DepartmentAggregate;
using Department = CFU.Domain.StudyContext.DepartmentAggregate.Department;

namespace CFU.EventHandlers.StudyContext;

public class InstituteDepartmentAddedEventHandler : INotificationHandler<InstituteDepartmentCreatedEvent>
{
    private readonly IDepartmentRepository _departmentRepository;

    public InstituteDepartmentAddedEventHandler(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public Task Handle(InstituteDepartmentCreatedEvent notification, CancellationToken cancellationToken)
    {
        var department = new Department(notification.Department.Id);
        _departmentRepository.Add(department);
        return Task.CompletedTask;
    }
}

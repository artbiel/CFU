using CFU.Domain.StructureContext.AcademyAggregate;
using CFU.Domain.StructureContext.FacultyAggregate;
using CFU.Domain.StructureContext.InstituteAggregate;
using CFU.Domain.SupplyContext.StructureUnitAggregate;

namespace CFU.EventHandlers.SupplyContext;

public class StructureUnitAddedEventsHandler :
    INotificationHandler<AcademyCreatedEvent>,
    INotificationHandler<FacultyCreatedEvent>,
    INotificationHandler<FacultyDepartmentCreatedEvent>,
    INotificationHandler<InstituteDepartmentCreatedEvent>
{
    private readonly IStructureUnitRepository _repository;

    public StructureUnitAddedEventsHandler(IStructureUnitRepository repository)
    {
        _repository = repository;
    }

    public Task Handle(AcademyCreatedEvent notification, CancellationToken cancellationToken)
    {
        var newUnit = new StructureUnit(new StructureUnitId(notification.Academy.Id.Id));
        _repository.Add(newUnit);
        return Task.CompletedTask;
    }

    public Task Handle(FacultyCreatedEvent notification, CancellationToken cancellationToken)
    {
        var newUnit = new StructureUnit(new StructureUnitId(notification.Faculty.Id.Id));
        _repository.Add(newUnit);
        return Task.CompletedTask;
    }

    public Task Handle(FacultyDepartmentCreatedEvent notification, CancellationToken cancellationToken)
    {
        var newUnit = new StructureUnit(new StructureUnitId(notification.Department.Id.Id));
        _repository.Add(newUnit);
        return Task.CompletedTask;
    }

    public Task Handle(InstituteDepartmentCreatedEvent notification, CancellationToken cancellationToken)
    {
        var newUnit = new StructureUnit(new StructureUnitId(notification.Department.Id.Id));
        _repository.Add(newUnit);
        return Task.CompletedTask;
    }
}

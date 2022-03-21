using CFU.Domain.HRContext.DepartmentAggregate;

namespace CFU.Domain.HRContext.TeacherAggregate;

public class Teacher : Employe<TeacherId>, IAggregateRoot<TeacherId>
{
    public Teacher(TeacherId id, Department department, string surname, string name, string patronymic)
        : base(id, surname, name, patronymic)
    {
        AssingToDepartment(department);
    }

    public DepartmentId DepartmentId { get; private set; }

    internal void AssingToDepartment(Department department)
    {
        DepartmentId = Guard.Against.Default(department, nameof(department)).Id;
        AddDomainEvent(new TeacherAssignedToDepartmentEvent(this, DepartmentId));
    }
}

namespace CFU.Domain.StudyContext.DepartmentAggregate;

public class TeacherNotFoundException : DomainException
{
    public TeacherNotFoundException(TeacherId teacherId) : base($"Teacher {teacherId} not found") { }
}

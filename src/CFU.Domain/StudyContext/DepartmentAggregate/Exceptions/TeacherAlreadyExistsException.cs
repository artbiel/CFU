namespace CFU.Domain.StudyContext.DepartmentAggregate;

public class TeacherAlreadyExistsException : DomainException
{
    public TeacherAlreadyExistsException(TeacherId teacherId) : base($"Teacher {teacherId} already exists")
    {
    }
}

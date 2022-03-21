namespace CFU.Domain.StudyContext.SpecialityAggregate;

public class GroupAlreadyExistsException : DomainException
{
    public GroupAlreadyExistsException(Group group)
        : base($"Group {group.Title} already exists") { }
}

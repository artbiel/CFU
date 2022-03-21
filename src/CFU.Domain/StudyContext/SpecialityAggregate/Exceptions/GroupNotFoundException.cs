namespace CFU.Domain.StudyContext.SpecialityAggregate;

public class GroupNotFoundException : DomainException
{
    public GroupNotFoundException(GroupId groupId)
        : base($"Group {groupId} not found") { }
}

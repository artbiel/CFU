namespace CFU.Domain.IdentityContext.UserAggregate;

public class UserIsNotInRoleException : DomainException
{
    public UserIsNotInRoleException(UserId userId, Role role)
        : base($"User {userId} is not in role {role}") { }
}

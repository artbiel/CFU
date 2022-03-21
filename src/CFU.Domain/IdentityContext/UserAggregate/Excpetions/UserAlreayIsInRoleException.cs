namespace CFU.Domain.IdentityContext.UserAggregate;

public class UserAlreayIsInRoleException : DomainException
{
    public UserAlreayIsInRoleException(UserId userId, Role role)
        : base($"User {userId} already is in role {role}") { }
}

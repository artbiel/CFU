namespace CFU.Domain.IdentityContext.UserAggregate;

public record struct UserId(Guid Id);

public class User : Entity<UserId>, IAggregateRoot<UserId>
{
    internal User(UserId id) : base(id) { }

    public string Password { get; set; }

    private readonly List<Role> _roles = new List<Role>();
    public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();

    public void AssignToRole(Role role)
    {
        Guard.Against.Default(role, nameof(role));
        if (_roles.Contains(role))
            throw new UserAlreayIsInRoleException(Id, role);
        _roles.Add(role);
    }

    public void UnassignFromRole(Role role)
    {
        Guard.Against.Default(role, nameof(role));
        if (!_roles.Contains(role))
            throw new UserIsNotInRoleException(Id, role);
        _roles.Remove(role);
    }
}

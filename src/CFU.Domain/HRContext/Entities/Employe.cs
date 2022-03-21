namespace CFU.Domain.HRContext;

public abstract class Employe<TId> : Entity<TId>
{
    public Employe(TId id, string surname, string name, string patronymic) : base(id)
    {
        SetSurname(surname);
        SetName(name);
        SetPatronymic(patronymic);
    }

    public string Surname { get; private set; }
    public string Name { get; private set; }
    public string Patronymic { get; private set; }
    public Email Email { get; private set; }
    public PhoneNumber PhoneNumber { get; private set; }

    public string FullName => $"{Surname} {Name} {Patronymic}";

    public void SetSurname(string surname)
    {
        Surname = Guard.Against.NullOrWhiteSpace(surname, nameof(surname));
    }

    public void SetName(string name)
    {
        Name = Guard.Against.NullOrWhiteSpace(name, nameof(name));
    }

    public void SetPatronymic(string patronymic)
    {
        Patronymic = Guard.Against.NullOrWhiteSpace(patronymic, nameof(patronymic));
    }

    public void SetEmail(Email email)
    {
        Email = Guard.Against.Default(email, nameof(email));
    }

    public void SetPhoneNumber(PhoneNumber phoneNumber)
    {
        PhoneNumber = Guard.Against.Default(phoneNumber, nameof(phoneNumber));
    }
}

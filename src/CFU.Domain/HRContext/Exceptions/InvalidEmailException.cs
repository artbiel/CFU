namespace CFU.Domain.HRContext;

public class InvalidEmailException : ArgumentException
{
    public InvalidEmailException(string email) : base($"Email ({email}) has incorrect format") { }
}

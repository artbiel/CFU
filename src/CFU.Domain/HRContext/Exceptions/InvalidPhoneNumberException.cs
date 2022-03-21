namespace CFU.Domain.HRContext;

public class InvalidPhoneNumberException : ArgumentException
{
    public InvalidPhoneNumberException(string number) : base($"Number ({number}) has incorrect format") { }
}

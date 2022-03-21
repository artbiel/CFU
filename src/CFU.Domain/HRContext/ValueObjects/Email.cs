using System.Text.RegularExpressions;

namespace CFU.Domain.HRContext;

public record struct Email
{
    private const string EMAIL_REGEX = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

    public Email(string value)
    {
        if (!Regex.Match(value, EMAIL_REGEX).Success)
            throw new InvalidEmailException(value);
        Value = value;
    }

    public string Value { get; }
}

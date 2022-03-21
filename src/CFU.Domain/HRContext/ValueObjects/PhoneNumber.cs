using System.Text.RegularExpressions;

namespace CFU.Domain.HRContext;

public record struct PhoneNumber
{
    private const string PHONE_REGEX = @"^((\+7|7|8)+([0-9]){10})$"; // for Russia

    public PhoneNumber(string value)
    {
        if (!Regex.Match(value, PHONE_REGEX).Success)
            throw new InvalidPhoneNumberException(value);
        Value = value;
    }

    public string Value { get; }
}

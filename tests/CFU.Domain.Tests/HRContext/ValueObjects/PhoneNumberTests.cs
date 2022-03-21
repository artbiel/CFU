using CFU.Domain.HRContext;

namespace CFU.Domain.UnitTests.HRContext.ValueObjects;

public class PhoneNumberTests
{
    [Theory]
    [InlineData("+79712345678")]
    [InlineData("79712345678")]
    [InlineData("89712345678")]
    public void Constructor_ShouldCreatePhoneNumber_WhenPhoneNumberIsValid(string value)
    {
        // Act
        var phoneNumber = new PhoneNumber(value);

        // Assert
        phoneNumber.Should().NotBeNull();
        phoneNumber.Value.Should().Be(value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("+7971234567")]
    [InlineData("+797123456789")]
    [InlineData("+89712345678")]
    [InlineData("89+ra712345678")]
    public void Constructor_ShouldThrowArgumentException_WhenPhoneNumberIsInvalid(string value)
    {
        // Act
        Action act = () => _ = new PhoneNumber(value); ;

        // Assert
        act.Should().Throw<InvalidPhoneNumberException>();
    }
}

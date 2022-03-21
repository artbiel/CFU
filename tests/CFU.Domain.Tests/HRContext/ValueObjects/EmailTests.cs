using CFU.Domain.HRContext;

namespace CFU.Domain.UnitTests.HRContext.ValueObjects;

public class EmailTests
{
    [Theory]
    [InlineData("abc@mail.ru")]
    [InlineData("q1@bk.ru")]
    public void Constructor_ShouldCreateEmail_WhenEmailIsValid(string value)
    {
        // Act
        var email = new Email(value);

        // Assert
        email.Should().NotBeNull();
        email.Value.Should().Be(value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    [InlineData("a@mail")]
    [InlineData("@mail.ru")]
    [InlineData("qwe@.ru")]
    public void Constructor_ShouldThrowArgumentException_WhenEmailIsInvalid(string value)
    {
        // Act
        Action act = () => _ = new Email(value); ;

        // Assert
        act.Should().Throw<InvalidEmailException>();
    }
}

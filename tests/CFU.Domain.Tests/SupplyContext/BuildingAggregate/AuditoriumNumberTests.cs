using CFU.Domain.SupplyContext.BuildingAggregate;

namespace CFU.Domain.UnitTests.SupplyContext.BuildingAggregate;

public class AuditoriumNumberTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(99)]
    public void Constructor_ShouldCreateAuditoriumNumber_WhenAllParametersAreValid(int number)
    {
        // Act
        var auditoriumNumber = new AuditoriumNumber(number);

        // Assert
        auditoriumNumber.Should().NotBeNull();
        auditoriumNumber.Value.Should().Be(number);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_ShouldThrowArgumentException_WhenNumberIsZeroOrNegative(int number)
    {
        // Act
        Action act = () => { var auditoriumNumber = new AuditoriumNumber(number); };

        // Assert
        act.Should().Throw<ArgumentException>();
    }
}

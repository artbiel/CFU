using CFU.Domain.SupplyContext.BuildingAggregate;

namespace CFU.Domain.UnitTests.SupplyContext.BuildingAggregate;

public class BuildingNumberTests
{
    [Theory]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(99)]
    public void Constructor_ShouldCreateBuildingNumber_WhenAllParametersAreValid(int number)
    {
        // Act
        var buildingNumber = new BuildingNumber(number);

        // Assert
        buildingNumber.Should().NotBeNull();
        buildingNumber.Value.Should().Be(number);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Constructor_ShouldThrowArgumentException_WhenNumberIsZeroOrNegative(int number)
    {
        // Act
        Action act = () => { var buildingNumber = new BuildingNumber(number); };

        // Assert
        act.Should().Throw<ArgumentException>();
    }
}

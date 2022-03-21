using CFU.Domain.SupplyContext.BuildingAggregate;

namespace CFU.Domain.UnitTests.SupplyContext.BuildingAggregate;

public class BuildingBlockTests
{
    [Fact]
    public void Constructor_ShouldCreateBuildingBlock_WhenAllParametersAreValid()
    {
        // Arrange
        var blockName = "block";

        // Act
        var block = new BuildingBlock(blockName);

        // Assert
        block.Should().NotBeNull();
        block.Value.Should().Be(blockName);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Constructor_ShouldThrowArgumentException_WhenBlockIsNullOrWhitespace(string blockName)
    {
        // Act
        Action act = () => _ = new BuildingBlock(blockName);

        // Assert
        act.Should().Throw<ArgumentException>();
    }
}

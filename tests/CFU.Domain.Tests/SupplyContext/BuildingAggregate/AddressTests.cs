using CFU.Domain.SupplyContext.BuildingAggregate;

namespace CFU.Domain.UnitTests.SupplyContext.BuildingAggregate;

public class AddressTests
{
    [Fact]
    public void Constructor_ShouldCreateAddress_WhenAllParametersAreValid()
    {
        // Arrange
        var city = "city";
        var street = "street";
        var buildingNumber = new BuildingNumber(1);
        var block = new BuildingBlock("block");

        // Act
        var adress = new Address(city, street, buildingNumber, block);

        // Assert
        adress.Should().NotBeNull();
        adress.City.Should().Be(city);
        adress.Street.Should().Be(street);
        adress.BuildingNumber.Should().Be(buildingNumber);
        adress.Block.Should().Be(block);
    }

    [Theory]
    [MemberData(nameof(GetDefaultParameters))]
    public void Constructor_ShouldThrowArgumentException_WhenParametersAreDefault(
        string city,
        string street,
        BuildingNumber number,
        BuildingBlock block)
    {
        // Act
        Action act = () => { var adress = new Address(city, street, number, block); };

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    public static TheoryData<string, string, BuildingNumber, BuildingBlock> GetDefaultParameters()
    {
        var city = "city";
        var street = "street";
        var buildingNumber = new BuildingNumber(1);
        var block = new BuildingBlock("block");

        return new TheoryData<string, string, BuildingNumber, BuildingBlock>
            {
                { default!, street, buildingNumber, block },
                { city, default!, buildingNumber, block },
                { city, street, default!, block },
                { city, street, default!, block }
            };
    }
}

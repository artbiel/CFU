using CFU.Domain.SupplyContext.BuildingAggregate;
using System.Linq;
using System.Threading.Tasks;

namespace CFU.Domain.UnitTests.SupplyContext.BuildingAggregate;

public class BuidingFactoryTests
{
    [Fact]
    public void Constructor_ShouldCreateBuildingFactory_WhenAllParametersAreValid()
    {
        // Arrange
        var repository = Substitute.For<IBuildingRepository>();

        // Act
        var factory = new BuildingFactory(repository);

        // Assert
        factory.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_WhenParametersAreDefault()
    {
        // Act
        var act = () => new BuildingFactory(default);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateBuilding_WhenAllParametersAreValid()
    {
        // Arrange
        var repository = Substitute.For<IBuildingRepository>();
        var id = new BuildingId(Guid.NewGuid());
        var address = new Address("fake_city", "fake_street", new BuildingNumber(1), new BuildingBlock("block"));

        Building? existingBuilding = default;
        repository.GetAsync(Arg.Is(new UniqueBuildingSpecification(address))).Returns(existingBuilding);
        var factory = new BuildingFactory(repository);

        // Act
        var building = await factory.CreateAsync(id, address);

        // Assert
        building.Should().NotBeNull();
        building.Id.Should().Be(id);
        building.Address.Should().Be(address);
        building.Auditoriums.Should().BeEmpty();
        building.DomainEvents.Should().NotBeEmpty();
        building.DomainEvents.First().Should().BeOfType<BuildingCreatedEvent>();
    }

    [Theory]
    [MemberData(nameof(GetBuildingDefaultParameters))]
    public async Task Create_ShouldThrowArgumentException_WhenParametersAreDefault(
        BuildingId id,
        Address address)
    {
        var repository = Substitute.For<IBuildingRepository>();
        var factory = new BuildingFactory(repository);

        // Act
        var act = () => factory.CreateAsync(id, address);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    [Fact]
    public async Task Create_ShouldThrowBuildingAlreadyExistsException_WhenBuildingAlreadyExists()
    {
        // Arrange
        var repository = Substitute.For<IBuildingRepository>();
        var id = new BuildingId(Guid.NewGuid());
        var address = new Address("fake_city", "fake_street", new BuildingNumber(1), new BuildingBlock("block"));

        var existingBuilding = new Building(id, address);
        repository.GetAsync(Arg.Is(new UniqueBuildingSpecification(address))).Returns(existingBuilding);
        var factory = new BuildingFactory(repository);

        // Act
        var act = () => factory.CreateAsync(id, address);

        // Assert
        await act.Should().ThrowAsync<BuildingAlreadyExistsException>();
    }

    public static TheoryData<BuildingId, Address> GetBuildingDefaultParameters()
    {
        var id = new BuildingId(Guid.NewGuid());
        var address = new Address("fake_city", "fake_street", new BuildingNumber(1), new BuildingBlock("block"));
        return new TheoryData<BuildingId, Address>
            {
                { default!, address },
                { id, default! }
            };
    }
}

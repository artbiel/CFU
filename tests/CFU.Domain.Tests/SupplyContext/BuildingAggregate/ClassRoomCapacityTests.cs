using CFU.Domain.SupplyContext.BuildingAggregate;

namespace CFU.Domain.UnitTests.SupplyContext.BuildingAggregate;

public class ClassRoomCapacityTests
{
    [Theory]
    [InlineData(ClassRoomCapacity.MIN_CAPACITY)]
    [InlineData(50)]
    [InlineData(100)]
    [InlineData(ClassRoomCapacity.MAX_CAPACITY)]
    public void Constructor_ShouldCreateBuildingNumber_WhenAllParametersAreValid(int capacity)
    {
        // Act
        var classRoomCapacity = new ClassRoomCapacity(capacity);

        // Assert
        classRoomCapacity.Should().NotBeNull();
        classRoomCapacity.Value.Should().Be(capacity);
    }

    [Theory]
    [InlineData(-10)]
    [InlineData(ClassRoomCapacity.MIN_CAPACITY - 1)]
    [InlineData(ClassRoomCapacity.MAX_CAPACITY + 1)]
    public void Constructor_ShouldThrowArgumentException_WhenCapacityIsOutOfRange(int capacity)
    {
        // Act
        Action act = () => _ = new ClassRoomCapacity(capacity);

        // Assert
        act.Should().Throw<ArgumentException>();
    }
}

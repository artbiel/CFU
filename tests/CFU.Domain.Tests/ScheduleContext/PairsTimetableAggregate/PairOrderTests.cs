using CFU.Domain.ScheduleContext.PairsTimetableAggregate;

namespace CFU.Domain.UnitTests.ScheduleContext.PairsTimetableAggregate;

public class PairOrderTests
{
    [Theory]
    [InlineData(PairOrder.MIN_ORDER)]
    [InlineData(3)]
    [InlineData(PairOrder.MAX_ORDER)]
    public void Constructor_ShouldCreateAddress_WhenAllParametersAreValid(int order)
    {
        // Act
        var pairOrder = new PairOrder(order);

        // Assert
        pairOrder.Should().NotBeNull();
        pairOrder.Value.Should().Be(order);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(PairOrder.MIN_ORDER - 1)]
    [InlineData(PairOrder.MAX_ORDER + 1)]
    [InlineData(100)]
    public void Constructor_ShouldThrowArgumentOutOfRangeException_WhenOrderIsInvalid(int order)
    {
        // Act
        Action act = () => { var pairOrder = new PairOrder(order); };

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }
}

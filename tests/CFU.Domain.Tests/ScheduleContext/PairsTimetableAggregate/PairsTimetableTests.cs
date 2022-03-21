using CFU.Domain.ScheduleContext.PairsTimetableAggregate;
using System.Linq;

namespace CFU.Domain.UnitTests.ScheduleContext.PairsTimetableAggregate;

public class PairsTimetableTests
{
    [Theory, PairsTimetableAutoData]
    public void AddPairTime_ShouldAddPair_WhenAllParametersAreValid(PairsTimetable timetable)
    {
        // Arrange
        var order = new PairOrder(PairOrder.MIN_ORDER);
        var startTime = new TimeOnly(08, 00);
        var expectedEndTime = new TimeOnly(09, 30);

        // Act
        var pairTime = timetable.AddPairTime(order, startTime);

        // Assert
        pairTime.Should().NotBeNull();
        pairTime.Order.Should().Be(order);
        pairTime.StartTime.Should().Be(startTime);
        pairTime.EndTime.Should().Be(expectedEndTime);
        timetable.PairTimes.Should().ContainSingle();
        timetable.PairTimes.Should().Contain(pairTime);
        timetable.DomainEvents.Should().ContainEquivalentOf(new PairTimeAddedEvent(pairTime));
    }

    [Theory, PairsTimetableAutoData]
    public void AddPairTime_ShouldThrowPairTimeAlreadyExistsException_WhenPairTimeAlreadyExists(PairsTimetable timetable)
    {
        // Arrange
        var order = new PairOrder(PairOrder.MIN_ORDER);
        var startTime = new TimeOnly(08, 00);
        var pairTime = timetable.AddPairTime(order, startTime);

        // Act
        Action act = () => timetable.AddPairTime(order, default);

        // Assert
        act.Should().Throw<PairTimeAlreadyExistsException>();
    }

    [Theory]
    [PairsTimetableMemberAutoData(nameof(AddPairTimeConflictedPairTimesParameters))]
    public void AddPairTime_ShouldThrowInvalidPairTimeException_WhenPairTimesIntersects(
        (PairOrder, TimeOnly)[] existingPairTimes, (PairOrder Order, TimeOnly StartTime) pairTime, PairsTimetable timetable)
    {
        // Arrange
        foreach (var (order, startTime) in existingPairTimes)
            timetable.AddPairTime(order, startTime);

        // Act
        Action act = () => timetable.AddPairTime(pairTime.Order, pairTime.StartTime);

        // Assert
        act.Should().Throw<PairTimesConflictException>();
    }

    [Theory, PairsTimetableAutoData]
    public void RemovePairTime_ShouldRemovePair_WhenPairTimeExists(PairsTimetable timetable)
    {
        // Arrange
        var order = new PairOrder(PairOrder.MIN_ORDER);
        var startTime = new TimeOnly(08, 00);
        var pairTime = timetable.AddPairTime(order, startTime);

        // Act
        timetable.RemovePairTime(order);

        // Assert
        timetable.PairTimes.Should().NotContain(pairTime);
        timetable.DomainEvents.Should().ContainEquivalentOf(new PairTimeRemovedEvent(pairTime));
    }

    [Theory]
    [PairsTimetableAutoData]
    public void RemovePairTime_ShouldThrowPairTimeNotFoundException_WhenPairTimeNotExists(PairsTimetable timetable)
    {
        // Arrange
        var notExistingPairOrder = new PairOrder(1);

        // Act
        Action act = () => timetable.RemovePairTime(notExistingPairOrder);

        // Assert
        act.Should().Throw<PairTimeNotFoundException>();
    }

    [Theory]
    [PairsTimetableMemberAutoData(nameof(GetSetPairTimeParameters))]
    public void SetPairTime_ShouldSetPairTime_WhenPairTimeExistsAndParametersAreValid(
        (PairOrder, TimeOnly)[] existingPairTimes, (PairOrder Order, TimeOnly StartTime) pairTime, PairsTimetable timetable)
    {
        // Arrange
        foreach (var (order, startTime) in existingPairTimes)
            timetable.AddPairTime(order, startTime);
        var editingPairTime = timetable.PairTimes.First(pt => pt.Order == pairTime.Order);
        var expectedEndTime = pairTime.StartTime.AddMinutes(90);

        // Act
        timetable.SetPairTime(pairTime.Order, pairTime.StartTime);

        // Assert
        timetable.PairTimes.Should().Contain(editingPairTime);
        editingPairTime.Order.Should().Be(pairTime.Order);
        editingPairTime.StartTime.Should().Be(pairTime.StartTime);
        editingPairTime.EndTime.Should().Be(expectedEndTime);
        timetable.DomainEvents.Should().ContainEquivalentOf(new PairTimeChangedEvent(editingPairTime));
    }

    [Theory]
    [PairsTimetableAutoData]
    public void SetPairTime_ShouldThrowPairTimeNotFoundException_WhenPairTimeNotExists(PairsTimetable timetable)
    {
        // Arrange
        var order = new PairOrder(PairOrder.MIN_ORDER);
        var startTime = new TimeOnly(08, 00);

        // Act
        Action act = () => timetable.SetPairTime(order, startTime);

        // Assert
        act.Should().Throw<PairTimeNotFoundException>();
    }

    [Theory]
    [PairsTimetableMemberAutoData(nameof(SetPairTimeConflictedPairTimesParameters))]
    public void SetPairTime_ShouldThrowPairTimesConflictException_WhenPairTimesIntersects(
        (PairOrder, TimeOnly)[] existingPairTimes, (PairOrder Order, TimeOnly StartTime) pairTime, PairsTimetable timetable)
    {
        // Arrange
        foreach (var (order, startTime) in existingPairTimes)
            timetable.AddPairTime(order, startTime);

        // Act
        Action act = () => timetable.SetPairTime(pairTime.Order, pairTime.StartTime);

        // Assert
        act.Should().Throw<PairTimesConflictException>();
    }

    public static TheoryData<PairsTimetableId, string, IPairsTimetableRepository> GetPairsTimetableDefaultParameters()
    {
        var id = new PairsTimetableId(Guid.NewGuid());
        var title = "title";
        var repository = Substitute.For<IPairsTimetableRepository>();

        return new TheoryData<PairsTimetableId, string, IPairsTimetableRepository>
            {
                { default!, title, repository },
                { id, default!, repository },
                { id, title, default! }
            };
    }

    public static TheoryData<(PairOrder, TimeOnly)[], (PairOrder, TimeOnly)> AddPairTimeConflictedPairTimesParameters
        => new TheoryData<(PairOrder, TimeOnly)[], (PairOrder, TimeOnly)>
        {
                {
                    new (PairOrder, TimeOnly)[]
                    {
                        (new PairOrder(1), new TimeOnly(8, 00)),
                        (new PairOrder(3), new TimeOnly(11, 30)),
                        (new PairOrder(4), new TimeOnly(13, 20))
                    },
                    (new PairOrder(2), new TimeOnly(10, 00))
                },
                {
                    new (PairOrder, TimeOnly)[]
                    {
                        (new PairOrder(1), new TimeOnly(8, 00)),
                        (new PairOrder(3), new TimeOnly(11, 30)),
                        (new PairOrder(4), new TimeOnly(13, 20))
                    },
                    (new PairOrder(2), new TimeOnly(10, 10))
                },
                {
                    new (PairOrder, TimeOnly)[]
                    {
                        (new PairOrder(2), new TimeOnly(9, 50)),
                        (new PairOrder(3), new TimeOnly(11, 30)),
                        (new PairOrder(4), new TimeOnly(13, 20))
                    },
                    (new PairOrder(1), new TimeOnly(15, 00))
                },
                {
                    new (PairOrder, TimeOnly)[]
                    {
                        (new PairOrder(1), new TimeOnly(8, 00)),
                        (new PairOrder(2), new TimeOnly(9, 50)),
                        (new PairOrder(3), new TimeOnly(11, 30))
                    },
                    (new PairOrder(4), new TimeOnly(08, 00))
                }
        };

    public static TheoryData<(PairOrder, TimeOnly)[], (PairOrder, TimeOnly)> GetSetPairTimeParameters()
    {
        var existingTimetable = new (PairOrder, TimeOnly)[]
        {
                (new PairOrder(1), new TimeOnly(8, 00)),
                (new PairOrder(2), new TimeOnly(9, 50)),
                (new PairOrder(3), new TimeOnly(11, 30)),
                (new PairOrder(4), new TimeOnly(13, 20))
        };

        return new TheoryData<(PairOrder, TimeOnly)[], (PairOrder, TimeOnly)>
            {
                { existingTimetable, (new PairOrder(1), new TimeOnly(7, 50)) },
                { existingTimetable, (new PairOrder(2), new TimeOnly(10, 55)) },
                { existingTimetable, (new PairOrder(4), new TimeOnly(13, 10)) }
            };
    }

    public static TheoryData<(PairOrder, TimeOnly)[], (PairOrder, TimeOnly)> SetPairTimeConflictedPairTimesParameters
        => new TheoryData<(PairOrder, TimeOnly)[], (PairOrder, TimeOnly)>
        {
                {
                    new (PairOrder, TimeOnly)[]
                    {
                        (new PairOrder(1), new TimeOnly(8, 00)),
                        (new PairOrder(2), new TimeOnly(09, 50)),
                        (new PairOrder(3), new TimeOnly(11, 30)),
                        (new PairOrder(4), new TimeOnly(13, 20))
                    },
                    (new PairOrder(2), new TimeOnly(10, 00))
                },
                {
                    new (PairOrder, TimeOnly)[]
                    {
                        (new PairOrder(1), new TimeOnly(8, 00)),
                        (new PairOrder(2), new TimeOnly(09, 50)),
                        (new PairOrder(3), new TimeOnly(11, 30)),
                        (new PairOrder(4), new TimeOnly(13, 20))
                    },
                    (new PairOrder(2), new TimeOnly(10, 10))
                },
                {
                    new (PairOrder, TimeOnly)[]
                    {
                        (new PairOrder(1), new TimeOnly(8, 00)),
                        (new PairOrder(2), new TimeOnly(09, 50)),
                        (new PairOrder(3), new TimeOnly(11, 30)),
                        (new PairOrder(4), new TimeOnly(13, 20))
                    },
                    (new PairOrder(1), new TimeOnly(15, 00))
                },
                {
                    new (PairOrder, TimeOnly)[]
                    {
                        (new PairOrder(1), new TimeOnly(8, 00)),
                        (new PairOrder(2), new TimeOnly(09, 50)),
                        (new PairOrder(3), new TimeOnly(11, 30)),
                        (new PairOrder(4), new TimeOnly(13, 20))
                    },
                    (new PairOrder(4), new TimeOnly(08, 00))
                }
        };
}


public class PairsTimetableMemberAutoData : MemberAutoDataAttribute
{
    public PairsTimetableMemberAutoData(string memberName, params object[] parameters) : base(new PairsTimetableAutoData(), memberName, parameters) { }
}

public class PairsTimetableAutoData : AutoDataAttribute
{
    public PairsTimetableAutoData() : base(Create) { }

    private static IFixture Create()
    {
        var fixture = new Fixture();

        var id = new PairsTimetableId(Guid.NewGuid());
        var title = fixture.Create<string>();
        var pairOrder = new PairOrder(3);

        fixture.Inject(pairOrder);
        fixture.Register(() => new PairsTimetable(id, title));

        return fixture;
    }
}

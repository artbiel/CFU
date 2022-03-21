using CFU.Domain.ScheduleContext.PairsTimetableAggregate;
using System.Linq;
using System.Threading.Tasks;

namespace CFU.Domain.UnitTests.ScheduleContext.PairsTimetableAggregate;

public class PairsTimetableFactoryTests
{
    [Fact]
    public void Constructor_ShouldCreatePairsTimetableFactory_WhenAllParametersAreValid()
    {
        // Arrange
        var repository = Substitute.For<IPairsTimetableRepository>();

        // Act
        var factory = new PairsTimetableFactory(repository);

        // Assert
        factory.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_WhenParametersAreDefault()
    {
        // Act
        var act = () => new PairsTimetableFactory(default);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Theory, AutoData]
    public async Task Create_ShouldCreatePairsTimetable_WhenAllParametersAreValid(PairsTimetableId id, string title)
    {
        // Arrange
        var repository = Substitute.For<IPairsTimetableRepository>();
        var factory = new PairsTimetableFactory(repository);
        PairsTimetable? existingTimetable = default;
        repository.GetAsync(Arg.Is(new UniquePairsTimetableSpecification(title))).Returns(existingTimetable);

        // Act
        var timetable = await factory.CreateAsync(id, title);

        // Assert
        timetable.Should().NotBeNull();
        timetable.Id.Should().Be(id);
        timetable.Title.Should().Be(title);
        timetable.PairTimes.Should().BeEmpty();
        timetable.DomainEvents.Should().NotBeEmpty();
        timetable.DomainEvents.First().Should().BeOfType<TimetableCreatedEvent>();
    }

    [Theory]
    [MemberData(nameof(GetPairsTimetableDefaultParameters))]
    public async Task Create_ShouldThrowArgumentException_WhenParametersAreDefault(
        PairsTimetableId id,
        string title)
    {
        var repository = Substitute.For<IPairsTimetableRepository>();
        var factory = new PairsTimetableFactory(repository);

        // Act
        var act = () => factory.CreateAsync(id, title);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    [Theory, AutoData]
    public async Task Create_ShouldThrowPairsTimetableAlreadyExistsException_WhenPairsTimetableAlreadyExists
        (PairsTimetableId id, string title)
    {
        // Arrange
        var existingPairsTimetable = new PairsTimetable(id, title);
        var repository = Substitute.For<IPairsTimetableRepository>();
        var factory = new PairsTimetableFactory(repository);
        repository.GetAsync(Arg.Is(new UniquePairsTimetableSpecification(title))).Returns(existingPairsTimetable);

        // Act
        var act = () => factory.CreateAsync(id, title);

        // Assert
        await act.Should().ThrowAsync<PairsTimetableAlreadyExistsException>();
    }

    public static TheoryData<PairsTimetableId, string> GetPairsTimetableDefaultParameters()
    {
        var id = new PairsTimetableId(Guid.NewGuid());
        var title = "title";

        return new TheoryData<PairsTimetableId, string>
            {
                { default!, title},
                { id, default!}
            };
    }
}

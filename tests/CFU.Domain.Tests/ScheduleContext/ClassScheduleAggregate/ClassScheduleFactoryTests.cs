using CFU.Domain.ScheduleContext.ClassScheduleAggregate;
using System.Linq;
using System.Threading.Tasks;

namespace CFU.Domain.UnitTests.ScheduleContext.ClassScheduleAggregate;

public class ClassScheduleFactoryTests
{
    [Fact]
    public void Constructor_ShouldCreateClassScheduleDomainService_WhenAllParametersAreValid()
    {
        // Arrange
        var repository = Substitute.For<IClassScheduleRepository>();

        // Act
        var factory = new ClassScheduleFactory(repository);

        // Assert
        factory.Should().NotBeNull();
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentException_WhenParametersAreDefault()
    {
        // Act
        var act = () => new ClassScheduleFactory(default);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Theory, AutoData]
    public async Task Create_ShouldCreateClassSchedule_WhenAllParametersAreValid(ClassScheduleId id, GroupId groupId)
    {
        // Arrange
        var repository = Substitute.For<IClassScheduleRepository>();
        var factory = new ClassScheduleFactory(repository);
        ClassSchedule? existingClassSchedule = default;
        repository.GetAsync(Arg.Is(new UniqueClassScheduleSpecification(groupId))).Returns(existingClassSchedule);

        // Act
        var timetable = await factory.CreateAsync(id, groupId);

        // Assert
        timetable.Should().NotBeNull();
        timetable.Id.Should().Be(id);
        timetable.GroupId.Should().Be(groupId);
        timetable.Pairs.Should().BeEmpty();
        timetable.DomainEvents.Should().NotBeEmpty();
        timetable.DomainEvents.First().Should().BeOfType<ClassScheduleCreatedEvent>();
    }

    [Theory]
    [MemberData(nameof(GetClassScheduleDeafultParameters))]
    public async Task Create_ShouldThrowArgumentException_WhenParametersAreDefault(
        ClassScheduleId id,
        GroupId groupId)
    {
        var repository = Substitute.For<IClassScheduleRepository>();
        var factory = new ClassScheduleFactory(repository);

        // Act
        var act = () => factory.CreateAsync(id, groupId);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>();
    }

    [Theory, AutoData]
    public async Task Create_ShouldThrowClassScheduleAlreadyExistsException_WhenClassScheduleAlreadyExists
        (ClassScheduleId id, GroupId groupId)
    {
        // Arrange
        var existingClassSchedule = new ClassSchedule(id, groupId);
        var repository = Substitute.For<IClassScheduleRepository>();
        var factory = new ClassScheduleFactory(repository);
        repository.GetAsync(Arg.Is(new UniqueClassScheduleSpecification(groupId))).Returns(existingClassSchedule);

        // Act
        var act = () => factory.CreateAsync(id, groupId);

        // Assert
        await act.Should().ThrowAsync<ClassScheduleAlreadyExistsException>();
    }

    public static TheoryData<ClassScheduleId, GroupId> GetClassScheduleDeafultParameters()
    {
        var id = new ClassScheduleId(Guid.NewGuid());
        var groupId = new GroupId(Guid.NewGuid());

        return new TheoryData<ClassScheduleId, GroupId>
            {
                { default!, groupId },
                { id, default! }
            };
    }
}
